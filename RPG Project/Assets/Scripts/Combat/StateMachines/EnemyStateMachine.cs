using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour //for processing enemy turns
{
    private BattleStateMachine BSM; //global battle state machine
    public BaseEnemy enemy; //contains enemy details

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //for ProgressBar
    private float cur_cooldown = 0f; //starting point for enemy ATB gauge (not fully needed as it is set to random value in Start())
    private float max_cooldown = 10f; //how long it takes for enemy ATB gauge to fill


    //this GameObject
    private Vector2 startPosition; //to store enemy's starting position for movement
    public GameObject Selector; //the selector cursor above the enemy
    //TimeforAction() stuff
    private bool actionStarted = false; //used for knowing whether to execute or exit the ieNumerator
    public GameObject HeroToAttack; //the hero to be attacked by enemy
    private float animSpeed = 10f; //speed at which enemy moves to target for attack animation

    //is enemy alive?
    private bool alive = true;

    //for MP calculations
    private List<BaseAttack> attacksWithinMPThreshold = new List<BaseAttack>();

    void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>(); //sets battle state machine to active battle state machine in BattleManager (in scene)
        startPosition = transform.position; //sets startPosition to the enemy's position at the start of battle
        cur_cooldown = Random.Range(0, 2.5f); //Sets random point for enemy ATB gauge to start
        Selector.SetActive(false); //hides enemy selector cursor
    }


    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                if (BSM.activeATB)
                {
                    UpgradeProgressBar(); //fills enemy ATB gauge
                }
                else
                {
                    if (BSM.pendingTurn == false)
                    {
                        UpgradeProgressBar(); //enemy hero ATB gauge
                    }
                }
            break;

            case (TurnState.CHOOSEACTION):
                ChooseAction(); //enemy chooses random attack from their available attacks (this is where enemy behavior will likely need to go)
                currentState = TurnState.WAITING;
            break;

            case (TurnState.WAITING):
                //idle state
            break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction()); //the actual action being performed (including animation and damage calculation)
            break;

            case (TurnState.DEAD): //run after every time enemy takes damage that brings them to or below 0 hp
                if(!alive) //if alive value is set to false, exits the turn state. this is set to false in below code
                {
                    return;
                } else
                {
                    BSM.expPool += enemy.earnedEXP; //increases enemy's exp to exp pool to take after battle
                    this.gameObject.tag = "DeadEnemy"; //change tag of enemy to DeadEnemy
                    BSM.EnemiesInBattle.Remove(this.gameObject); //Makes this enemy not attackable by heroes
                    Selector.SetActive(false); //disable the selector cursor for the enemy
                    
                    if (BSM.EnemiesInBattle.Count > 0) //remove all enemyAttacks inputs from active perform list if there are still enemies on the field
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++) //go through all actions in perform list
                        {
                            //if (i != 0) //can remove later if enemies can kill themselves. otherwise only checks for items in the perform list after 0 (as 0 would be the hero's action)
                            //{
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject) //if the attacker in the loop is this enemy
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]); //removes this action from the perform list
                                }
                                if (BSM.PerformList[i].AttackersTarget == this.gameObject) //if target in loop in the perform list is the dead enemy
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.EnemiesInBattle[Random.Range(0, BSM.EnemiesInBattle.Count)]; //changes the target from the dead enemy to a random enemy so dead enemy cannot be attacked
                                }
                            //}
                        }
                    }
                    
                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255); //change the color to gray. later play death animation
                    alive = false; //set alive to false to exit out of the turn state
                    BSM.EnemyButtons(); //reset enemy buttons for all alive enemies
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE; //changes battle state to check alive
                }
            break;
        }
    }

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime; //increases enemy ATB gauge over time
        
        if (cur_cooldown >= max_cooldown) //if enemy ATB gauge meets threshold for choosing an action
        {
            BSM.pendingTurn = true;
            currentState = TurnState.CHOOSEACTION; //choose the action
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn(); //new handleturn for the enemy's attack
        myAttack.Attacker = enemy._Name; //enemy's name set for the handleturn's attacker
        myAttack.Type = "Enemy"; //sets handleturn's type to enemy
        myAttack.AttackersGameObject = this.gameObject; //sets the handleturn's attacker game object to this enemy
        myAttack.AttackersTarget = BSM.HeroesInBattle[Random.Range(0, BSM.HeroesInBattle.Count)]; //chooses random hero target --- currently bugged, if all heros die, this line will error as there are no heros left to attack

        //check which attacks are available based on MP cost of all attacks, and enemy's current MP, and adds them to 'attacksWithinMPThreshold' list.
        foreach (BaseAttack atk in enemy.attacks)
        {
            if (atk.attackCost <= enemy.curMP)
            {
                attacksWithinMPThreshold.Add(atk);
            }
        }

        int num = Random.Range(0, attacksWithinMPThreshold.Count); //chooses random enemy attack within MP threshold of enemy's current MP
        myAttack.chosenAttack = attacksWithinMPThreshold[num]; //sets the chosen attack in the enemy's possible attacks to the random value from above

        attacksWithinMPThreshold.Clear(); //clears list for next enemy to use

        BSM.CollectActions(myAttack); //adds chosen attack to the perform list
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break; //breaks from the IEnumerator if we have already gone through it
        }

        actionStarted = true;
        
        //this is where actual attack animation would go
        Vector2 heroPosition = new Vector2(HeroToAttack.transform.position.x-1.5f,HeroToAttack.transform.position.y); //gets hero's position (minus a few pixels on the x axis) to move to for attack animation
        while (MoveToTarget(heroPosition)) {yield return null; } //move towards the target
        
        yield return new WaitForSeconds(0.5f); //wait a bit
        
        DoDamage(); //do damage with calculations (this will change later)

        //animate the enemy back to start position
        Vector2 firstPosition = startPosition; //changes the first position of the animation back to the starting position of the enemy
        while (MoveToTarget(firstPosition)) { yield return null; } //moves back towards the starting position
        
        BSM.PerformList.RemoveAt(0); //removes this performer from the perform list in battle state manager

        BSM.pendingTurn = false;

        BSM.battleStates = BattleStateMachine.PerformAction.WAIT; //reset battle state manager back to wait state
        
        actionStarted = false; //end the coroutine

        cur_cooldown = 0f; //reset the enemy ATB to 0
        currentState = TurnState.PROCESSING; //starts the turn over from waiting for the enemy ATB gauge to fill
    }

    private bool MoveToTarget(Vector3 target) //using Vector2 causes an error, but Vector3 translates without issues
    {
        return target != (transform.position = Vector2.MoveTowards(transform.position, target, animSpeed * Time.deltaTime)); //moves towards the target parameter until position is same as the target position
    }

    void DoDamage() //deals damage to hero
    {
        float calc_damage = enemy.curATK + BSM.PerformList[0].chosenAttack.attackDamage; //calculates damage by enemy's current attack + the attack's damage
        HeroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calc_damage); //processes the damage to the hero from the enemy
        Debug.Log(this.gameObject.name + " has chosen " + BSM.PerformList[0].chosenAttack.attackName + " and does " + calc_damage + " damage to " + HeroToAttack.GetComponent<HeroStateMachine>().hero._Name + "!");

        enemy.curMP -= BSM.PerformList[0].chosenAttack.attackCost; //remove MP from enemy
    }

    public void TakeDamage(float getDamageAmount) //receives damage from hero
    {
        enemy.curHP -= getDamageAmount; //lowers current HP from damageAmount parameter
        if(enemy.curHP <= 0) //checks if enemy is dead
        {
            enemy.curHP = 0; //sets HP to 0 if lower than 0
            currentState = TurnState.DEAD; //changes enemy state to DEAD
        }
    }
}
