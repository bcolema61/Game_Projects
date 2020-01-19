using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM; //global battle state machine
    public BaseHero hero; //this hero

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //for ProgressBar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    private Image ProgressBar;
    public GameObject Selector;
    //IeNumerator
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    private Vector2 startPosition;
    private float animSpeed = 10f;
    //dead
    private bool alive = true;

    //heroPanel
    private HeroPanelStats stats;
    public GameObject HeroPanel;
    private Transform HeroPanelSpacer; //for the spacer on the Hero Panel (for Vertical Layout Group)

    void Start()
    {
        HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").transform.Find("HeroPanelSpacer"); //find spacer and make connection
                
        CreateHeroPanel(); //create panel and fill in info

        if (hero.curHP == 0) //if hero starts battle at 0 HP
        {
            ProgressBar.transform.localScale = new Vector2(0, ProgressBar.transform.localScale.y); //reduces ATB gauge to 0
            cur_cooldown = 0; //Sets ATB gauge to 0
            currentState = HeroStateMachine.TurnState.DEAD; //sets hero in dead state
        } else //if hero is alive
        {
            cur_cooldown = Random.Range(0, 2.5f); //Sets random point for ATB gauge to start
            currentState = TurnState.PROCESSING; //begins hero processing phase
        }

        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>(); //make connection to the global battle state manager
        Selector.SetActive(false); //hide hero selector cursor
        startPosition = transform.position; //sets start position to hero's position for animation purposes
    }

    void Update()
    {
        //Debug.Log("HeroStateMachine - currentState: " + currentState);
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                if (BSM.activeATB)
                {
                    UpgradeProgressBar(); //fills hero ATB gauge
                } else
                {
                    if (BSM.pendingTurn == false)
                    {
                        UpgradeProgressBar(); //fills hero ATB gauge
                    }
                }
                
            break;

            case (TurnState.ADDTOLIST):
                BSM.HeroesToManage.Add(this.gameObject); //adds hero to heros who can make selection
                currentState = TurnState.WAITING;
            break;

            case (TurnState.WAITING):
                //idle
            break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction()); //processes hero action
            break;

            case (TurnState.DEAD): //run after every time enemy takes damage that brings them to or below 0 hp
                if (!alive) //if alive value is set to false, exits the turn state. this is set to false in below code
                {
                    return;
                } else
                {
                    this.gameObject.tag = "DeadHero"; //change tag of hero to DeadHero
                    BSM.HeroesInBattle.Remove(this.gameObject); //not hero attackable by enemy
                    BSM.HeroesToManage.Remove(this.gameObject); //not able to manage hero with player
                    
                    Selector.SetActive(false); //hide the selector cursor for the hero
                    //reset GUI
                    BSM.actionPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    //remove hero's handleturn from performlist (if there was one)
                    if (BSM.HeroesInBattle.Count > 0)
                    {
                        
                        for (int i = 0; i < BSM.PerformList.Count; i++) //go through all actions in perform list
                        {
                            if (i != 0) //can remove later if heros can kill themselves.  otherwise only checks for items in the perform list after 0 (as 0 would be the enemy's action)
                            {
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject) //if the attacker in the loop is this hero
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]); //removes this action from the perform list
                                }

                                if (BSM.PerformList[i].AttackersTarget == this.gameObject) //if target in loop in the perform list is the dead hero
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.HeroesInBattle[Random.Range(0, BSM.HeroesInBattle.Count)];//changes the target from the dead hero to a random hero so dead hero cannot be attacked
                                }
                            }
                        }
                    }
                    
                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255); //change color/ play animation
                    Debug.Log(hero._Name + " - DEAD");
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;

                    alive = false;
                }
            break;
        }
    }

    //create hero panel
    void CreateHeroPanel()
    {
        HeroPanel = Instantiate(HeroPanel) as GameObject; //creates gameobject of heroPanel prefab (display in BattleCanvas which shows ATB gauge and HP, MP, etc)
        stats = HeroPanel.GetComponent<HeroPanelStats>(); //gets the hero panel's stats script
        stats.HeroName.text = hero._Name; //sets hero name in the hero panel to the current hero's name
        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP; //sets HP in the hero panel to the current hero's HP
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP; //sets MP in the hero panel to the current hero's MP
        ProgressBar = stats.ProgressBar; //sets ATB gauge in the hero panel to the hero's ATB
        HeroPanel.transform.SetParent(HeroPanelSpacer, false); //sets the hero panel to the hero panel's spacer for vertical layout group
    }

    void UpgradeProgressBar()
    {
        cur_cooldown = (cur_cooldown + (Time.deltaTime / 1f)) + (hero.dexterity * .000055955f); //increases ATB gauge, first float dictates how slowly gauge fills (default 1.5f), while second float dictates how effective dexterity is
        float calc_cooldown = cur_cooldown / max_cooldown; //does math of % of ATB gauge to be filled each frame
        ProgressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y); //shows graphic of ATB gauge increasing
        if (cur_cooldown >= max_cooldown) //if hero turn is ready
        {
            BSM.pendingTurn = true;
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator TimeForAction()
    {     
        if (actionStarted)
        {
            yield break; //breaks from the IEnumerator if we have gone through it already
        }

        actionStarted = true;

        //animate the hero to the enemy (this is where attack animations will go)
        Vector2 enemyPosition = new Vector2(EnemyToAttack.transform.position.x + 1.5f, EnemyToAttack.transform.position.y); //sets enemyPosition to the chosen enemy's position + a few pixels on the x axis
        while (MoveToTarget(enemyPosition)) { yield return null; } //moves the hero to the calculated position above
        
        yield return new WaitForSeconds(0.5f); //wait a bit
        
        DoDamage(); //do damage with calculations (this will change later)
        ReduceMP(); //reduce MP by attack cost

        //animate the enemy back to start position
        Vector2 firstPosition = startPosition; //changes the hero's position back to the starting position
        while (MoveToTarget(firstPosition)) { yield return null; } //move the hero back to the starting position        
        
        BSM.PerformList.RemoveAt(0); //remove this performer from the list in BSM

        RecoverMPAfterTurn(); //slowly recover MP based on spirit value

        BSM.pendingTurn = false;
        
        if (BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE) //if the battle is still going (didn't win or lose)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT; //sets battle state machine back to WAIT
            
            cur_cooldown = 0f; //reset the hero's ATB gauge to 0
            currentState = TurnState.PROCESSING; //starts the turn over back to filling up the ATB gauge
        } else
        {
            currentState = TurnState.WAITING; //if the battle is in win or lose state, turns the hero back to WAITING (idle) state
        }

        //end coroutine
        actionStarted = false;
    }

    private bool MoveToTarget(Vector3 target)
    {
        return target != (transform.position = Vector2.MoveTowards(transform.position, target, animSpeed * Time.deltaTime)); //moves toward target until position is same as target position
    }

    public void TakeDamage(float getDamageAmount) //receives damage from enemy
    {
        hero.curHP -= getDamageAmount; //subtracts hero's current HP from getDamageAmount parameter
        if (hero.curHP <= 0)
        {
            hero.curHP = 0; //sets hero's HP to 0 if it is 0 or below
            currentState = TurnState.DEAD; //changes hero's state to DEAD
        }
        UpdateHeroStats(); //updates UI to show current HP and MP
    }
    
    void DoDamage() //deals damage to enemy
    {
        float calc_damage;
        if (BSM.PerformList[0].chosenAttack.attackType == "Physical")
        {
            calc_damage = hero.curATK + BSM.PerformList[0].chosenAttack.attackDamage; //calculates damage by hero's attack + the chosen attack's damage
            EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage); //processes enemy take damage by above value
            Debug.Log(hero._Name + " has chosen " + BSM.PerformList[0].chosenAttack.attackName + " and does " + calc_damage + " damage to " + EnemyToAttack.GetComponent<EnemyStateMachine>().enemy._Name + "!");
            Debug.Log(BSM.PerformList[0].chosenAttack.attackName + " calc_damage - physical - hero's ATK: " + hero.curATK + " + chosenAttack's damage: " + BSM.PerformList[0].chosenAttack.attackDamage + " = " + calc_damage);
        }
        else if (BSM.PerformList[0].chosenAttack.attackType == "Magic")
        {
            //can check if magic attack should have a flat value, ie gravity spell
            calc_damage = hero.curMATK + BSM.PerformList[0].chosenAttack.attackDamage; //calculates damage by hero's magic attack + the chosen attack's damage
            EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage); //processes enemy take damage by above value
            Debug.Log(hero._Name + " has chosen " + BSM.PerformList[0].chosenAttack.attackName + " and does " + calc_damage + " damage to " + EnemyToAttack.GetComponent<EnemyStateMachine>().enemy._Name + "!");
            Debug.Log(BSM.PerformList[0].chosenAttack.attackName + " calc_damage - magic - hero's MATK: " + hero.curMATK + " + chosenAttack's damage: " + BSM.PerformList[0].chosenAttack.attackDamage + " = " + calc_damage);
        }
        else //if attack type not found
        {
            calc_damage = hero.curATK + BSM.PerformList[0].chosenAttack.attackDamage; //calculates damage by hero's attack + the chosen attack's damage
            EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage); //processes enemy take damage by above value
            Debug.Log(hero._Name + " has chosen " + BSM.PerformList[0].chosenAttack.attackName + " and does " + calc_damage + " damage to " + EnemyToAttack.GetComponent<EnemyStateMachine>().enemy._Name + "! -- NOTE: ATTACK TYPE NOT FOUND: " + BSM.PerformList[0].chosenAttack.attackType);
        }
        
    }

    void ReduceMP() //after casting magic
    {
        hero.curMP -= BSM.PerformList[0].chosenAttack.attackCost; //remove MP of chosen attack from hero
        UpdateHeroStats(); //update the UI to show missing MP
    }

    //Update stats on damage / heal
    void UpdateHeroStats() //can maybe make this public to process when losing MP
    {
        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP;
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP;

        for (int i=0; i < GameManager.instance.activeHeroes.Count; i++)
        {
            if (GameManager.instance.activeHeroes[i]._Name == hero._Name) {
                GameManager.instance.activeHeroes[i].curHP = hero.curHP;
                GameManager.instance.activeHeroes[i].curMP = hero.curMP;
            }
        }
    }

    void RecoverMPAfterTurn()
    {
        if (hero.curMP < hero.baseMP)
        {
           hero.curMP += Mathf.Ceil(hero.spirit * .15f);
           Debug.Log(hero._Name + " recovering " + Mathf.Ceil(hero.spirit * .15f) + " MP");
        }

        if (hero.curMP > hero.baseMP)
        {
            hero.curMP = hero.baseMP;
        }
        UpdateHeroStats();
    }
}
