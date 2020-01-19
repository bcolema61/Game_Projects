using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour //for processing phases of battle between enemies and heroes
{
    public enum PerformAction //phases of battle
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }
    public PerformAction battleStates;
    
    public enum HeroGUI //phases of a hero selecting input
    {
        ACTIVATE,
        WAITING,
        DONE
    }
    public HeroGUI HeroInput;

    public List<HandleTurn> PerformList = new List<HandleTurn>(); //to store the turns that have been chosen between enemies and heros
    public List<GameObject> HeroesInBattle = new List<GameObject>(); //to store the gameobjects for all living heroes in battle
    public List<GameObject> AllHeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>(); //to store the gameobjects for all enemies in battle

    public List<GameObject> HeroesToManage = new List<GameObject>(); //to store the gameobjects for all heros available to make a selection
    private HandleTurn HeroChoice; //the variable to store current hero's selection

    public GameObject EnemyButton; //button to click to use attack on enemy
    public Transform EnemySelectSpacer; //for enemy select panel vertical layout group

    public GameObject actionPanel; //panel that displays attack, magic, etc.
    public GameObject EnemySelectPanel; //panel that displays list of enemy targets
    public GameObject MagicPanel; //panel that lists magic attacks

    //hero attacks
    public Transform actionSpacer; //to be assigned to the action panel's spacer
    public Transform magicSpacer; //to be assigned to the magic panel's spacer
    public GameObject actionButton; //to be assigned to the attack button in action panel
    public GameObject magicButton; //to be assigned to the magic button in action panel

    private List<GameObject> atkBtns = new List<GameObject>();

    //enemy buttons
    private List<GameObject> enemyBtns = new List<GameObject>();

    //SPAWN POINTS
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public List<Transform> heroSpawnPoints = new List<Transform>();

    //for adding EXP after battle
    public int expPool;

    //for Active/Wait Time Battle
    public bool activeATB = false; //change for active or wait ATB;
    public bool pendingTurn = false;

    //for moving 'back' in menus with Cancel button (escape by default)
    bool enemySelectAfterAttackCancel = false;
    bool magicAttackCancel = false;
    bool enemySelectAfterMagicCancel = false;

    private void Awake()
    {
        for (int i = 0; i < GameManager.instance.activeHeroes.Count; i++)
        {
            GameObject NewHero = Instantiate(GameManager.instance.heroesToBattle[i], heroSpawnPoints[i].position, Quaternion.identity) as GameObject; //uses enemy prefabs in Encounter region list and creates them as gameobjects
        }
        for(int i=0; i < GameManager.instance.enemyAmount; i++)
        {
            GameObject NewEnemy = Instantiate(GameManager.instance.enemiesToBattle[i], enemySpawnPoints[i].position, Quaternion.identity) as GameObject; //uses enemy prefabs in Encounter region list and creates them as gameobjects
            if (i == 0)
            {
                NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().enemy._Name; //sets the created enemy's name based on prefab enemy's name
            } else
            {
                NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().enemy._Name + " " + (i + 1); //if there are more than 1 of the enemy, add a number to it.  This will need to be updated as separate enemies will not be taken into account
            }
            NewEnemy.GetComponent<EnemyStateMachine>().enemy._Name = NewEnemy.name; //sets the created enemy's name in the state machine
            EnemiesInBattle.Add(NewEnemy); //adds the created enemy to enemies in battle list
        }
    }

    void Start()
    {
        battleStates = PerformAction.WAIT; //battle starts in Battle State "WAIT"
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero")); //adds all heros with Hero tag to Heroes in Battle list
        for (int i = 0; i < HeroesInBattle.Count; i++) //copies all available heroes in battle to 'AllHeroesInBattle' so both lists can be used differently
        {
            AllHeroesInBattle.Add(HeroesInBattle[i]);
        }
        //insert here to tie heros in battle to new hero manager stuff to maintain exp, hp, mp, etc. can loop through heroes in battle to assign.
        HeroInput = HeroGUI.ACTIVATE; //battle starts with hero interface in state "ACTIVATE"

        actionPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        MagicPanel.SetActive(false);

        EnemyButtons(); //creates enemy buttons from EnemiesInBattle list
    }

    
    void Update()
    {
        switch(battleStates) //phases of battle
        {
            case (PerformAction.WAIT): //checks if actions are to be taken
                if (PerformList.Count > 0) //if there are actions to be taken (from enemy or hero)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
            break;

            case (PerformAction.TAKEACTION): //checks for hero/enemy and processes action
                GameObject performer = GameObject.Find(PerformList[0].Attacker); //creates game object = enemy or hero that is attacking as "performer" which is used as current attacker (hero or enemy)
                if (PerformList[0].Type == "Enemy") //if attacker is an enemy
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>(); //gets enemy's state machine
                    for (int i=0; i < HeroesInBattle.Count; i++) //for each hero in the battle
                    {
                        if(PerformList[0].AttackersTarget == HeroesInBattle[i]) //if the enemy's target = the hero in the loop index of all heroes currently in battle
                        {
                            ESM.HeroToAttack = PerformList[0].AttackersTarget; //changes enemy state machine hero to attack to the enemy's target
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        { //if the enemy's target != the hero in the loop index of heroes currently in battle (ie. enemy has already chosen which hero to attack, but this hero died in the last turn, so enemy unable to attack that hero)
                            PerformList[0].AttackersTarget = HeroesInBattle[Random.Range(0, HeroesInBattle.Count)]; //change enemy's target to random hero in battle
                            ESM.HeroToAttack = PerformList[0].AttackersTarget; //change enemy state machine hero to attack to enemy's new target
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                    }
                }
                if (PerformList[0].Type == "Hero") //if attacker is a hero
                {
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>(); //gets hero's state machine
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget; //changes hero state machine enemy to attack to the hero's target
                    HSM.currentState = HeroStateMachine.TurnState.ACTION; //tells hero state machine to start ACTION phase
                }
                battleStates = PerformAction.PERFORMACTION; //changes battle state to PERFORMACTION
            break;

            case (PerformAction.PERFORMACTION): //not sure yet what this does exactly

            break;

            case (PerformAction.CHECKALIVE): //checks when hero or enemy dies if win or loss conditions have been met
                if (HeroesInBattle.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                    //lose game
                } else if (EnemiesInBattle.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                    //win the battle
                } else
                {
                    clearAttackPanel(); //resets/clears enemySelect, actionPanel, magicPanel, and attack buttons
                    HeroInput = HeroGUI.ACTIVATE;
                }
            break;

            case (PerformAction.LOSE): //lose battle
                Debug.Log("Game lost"); //things to go here later - retry battle, go back to world map, load from save
            break;

            case (PerformAction.WIN): //win battle
                Debug.Log("You win!");
                for (int i = 0; i < HeroesInBattle.Count; i++) //for each hero in battle
                {
                    GameManager.instance.activeHeroes[i].currentExp += expPool; //adds the expPool to each active hero's current exp 
                    HeroesInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING; //set each hero to WAITING state
                }

                expPool = 0; //reset exp pool
                UpdateActiveHeroes(); //keeps hero's parameters (HP, MP, EXP, and any persistent buffs/debuffs) consistent through each battle
                GameManager.instance.ProcessExp();
                GameManager.instance.heroesToBattle.Clear(); //clears heroesToBattle list to be generated again next battle

                GameManager.instance.LoadSceneAfterBattle(); //load scene from before battle
                GameManager.instance.gameState = GameManager.GameStates.HOSTILE_STATE; //puts game manager back to hostile state
                GameManager.instance.enemiesToBattle.Clear(); //clears enemies to battle list to be used from scratch on next battle
            break;


        }

        switch (HeroInput) //phases of hero inputs when hero's turn is available
        {
            case (HeroGUI.ACTIVATE): //hero's turn is available
                if (HeroesToManage.Count > 0) //if there is a hero's turn available (ATB gauge filled up and pending input)
                {
                    HeroesToManage[0].transform.Find("Selector").gameObject.SetActive(true); //Show hero's selector cursor
                    HeroChoice = new HandleTurn(); //new handle turn instance as HeroChoice
                    actionPanel.SetActive(true); //enables actionPanel (attack, magic, etc)
                    CreateActionButtons(); //populate action buttons
                    SetCancelButton(0); //Cancel button does nothing
                    HeroInput = HeroGUI.WAITING;
                }
            break;

            case (HeroGUI.WAITING):
                //idle state
            break;

            case (HeroGUI.DONE):
                HeroInputDone();
            break;

        }

        GetCancelButton();
    }

    void UpdateActiveHeroes() //updates game manager hero parameters upon winning the battle
    {
        for (int i = 0; i < GameManager.instance.heroAmount; i++)
        {
            BaseHero heroToUpdate = GameManager.instance.activeHeroes[i];
            BaseHero fromHero = AllHeroesInBattle[i].GetComponent<HeroStateMachine>().hero;
            heroToUpdate.curHP = fromHero.curHP;
            heroToUpdate.curMP = fromHero.curMP;
            //add exp here
        }
    }
    
    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input); //used by enemy state machine to add enemy's chosen attack to the perform list
    }

    public void EnemyButtons()
    {
        //clean up
        foreach(GameObject enemyBtn in enemyBtns) //for each enemy button already added
        {
            Destroy(enemyBtn); //destroy the game object so it doesn't appear as potential enemy to attack
        }
        enemyBtns.Clear(); //clears the enemy buttons list

        //create buttons
        foreach(GameObject enemy in EnemiesInBattle) //for each enemy in the enemies in battle list
        {
            GameObject newButton = Instantiate(EnemyButton) as GameObject; //creates new enemy button (to select as a target for attack) for each enemy still in battle
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>(); //initialize enemy select button variable for the current enemy in the loop

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>(); //initialize enemy state machine for current enemy to be manipulated below

            Text buttonText = newButton.GetComponentInChildren<Text>(); //initialize text as the button's text object
            buttonText.text = cur_enemy.enemy._Name; //changes the button's text to the current enemy's name

            button.EnemyPrefab = enemy; //sets the enemy select button's attached enemy to the current enemy

            newButton.transform.SetParent(EnemySelectSpacer,false);  //sets the parent of the new enemy button to the spacer for the enemy button panel
            enemyBtns.Add(newButton); //adds the enemy select variable to the enemy buttons list
        }
    }

    public void AttackInput() //when clicking 'attack' in actionPanel
    {
        HeroChoice.Attacker = HeroesToManage[0].name; //sets heroChoice attacker to current hero making selection
        HeroChoice.AttackersGameObject = HeroesToManage[0]; //sets heroChoice attacker's game object to the current hero making selection's game object
        HeroChoice.Type = "Hero"; //as HeroChoice is of class HandleTurn, sets type to Hero
        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<HeroStateMachine>().hero.attacks[0]; //sets heroChoice chosen attack to the current hero's hero state machine to the attack at top of their attack list (likely change later)
        actionPanel.SetActive(false); //hides attack panel as action has been chosen
        EnemySelectPanel.SetActive(true); //displays enemy select panel to process chosen attack to
        SetCancelButton(1);
    }

    public void EnemySelection(GameObject chosenEnemy) //for selecting enemy
    {
        HeroChoice.AttackersTarget = chosenEnemy; //sets the current hero making selection's target to the chosen enemy after clicking on it from enemySelectPanel
        HeroInput = HeroGUI.DONE;
    }

    void HeroInputDone() 
    {
        PerformList.Add(HeroChoice); //adds the details of the current hero making selection's choice to the perform list
        
        //here we can possibly check for the selected attack's MP and reduce it from the selected hero's MP.  might be a different location.
        
        clearAttackPanel(); //cleans the attackpanel

        HeroesToManage[0].transform.Find("Selector").gameObject.SetActive(false); //hides the current hero making selection's selector cursor
        HeroesToManage.RemoveAt(0); //removes the hero making selection from the heroesToManage list
        HeroInput = HeroGUI.ACTIVATE; //resets the HeroGUI switch back to the beginning to await the next hero's choice
    }

    void clearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        actionPanel.SetActive(false);
        MagicPanel.SetActive(false);

        foreach (GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();
    }

    //create actionbuttons (attack, magic, etc)
    void CreateActionButtons()
    {
        GameObject ActionButton = Instantiate(actionButton) as GameObject; //instantiates prefab assigned to actionButton as a gameobject for attack command
        Text ActionButtonText = ActionButton.transform.Find("Text").gameObject.GetComponent<Text>(); //initializes text object as the text attached to above actionButton
        ActionButtonText.text = "Attack"; //changes the text of the actionButton to 'Attack'
        ActionButton.GetComponent<Button>().onClick.AddListener(() => AttackInput()); //assigns action when clicking the button to AttackInput() function
        ActionButton.transform.SetParent(actionSpacer, false); //sets the parent of this button to the action panel spacer
        atkBtns.Add(ActionButton); //adds the action button to atkBtns list for organizational purposes

        GameObject MagicActionButton = Instantiate(actionButton) as GameObject; //instantiates prefab assigned to actionButton as a gameobject for magic command
        Text MagicActionButtonText = MagicActionButton.transform.Find("Text").gameObject.GetComponent<Text>(); //initializes text object as the text attached to above actionButton
        MagicActionButtonText.text = "Magic"; //changes the text of the actionButton to 'Magic'
        MagicActionButton.GetComponent<Button>().onClick.AddListener(() => MagicInput()); //assigns action when clicking the button to MagicInput() function
        MagicActionButton.transform.SetParent(actionSpacer, false); //sets the parent of this button to the action panel spacer
        atkBtns.Add(MagicActionButton); //adds the magic button to atkBtns list for organizational purposes

        if (HeroesToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks.Count > 0) //if the current hero has any magic attacks
        {
            foreach(BaseAttack magicAtk in HeroesToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks) //for each magic attack
            {
                GameObject MagicButton = Instantiate(magicButton) as GameObject; //instantiate prefab assigned to magicButton as a gameobject for selecting which magic to use after clicking magic in action panel
                Text MagicButtonText = MagicButton.transform.Find("Text").gameObject.GetComponent<Text>(); //initializes text object as the text attached to above magicButton
                MagicButtonText.text = magicAtk.attackName; //changes the text of the magicButton to the current magic attack in the loop
                MagicAttackButton MATB = MagicButton.GetComponent<MagicAttackButton>(); //initializes Magic Attack Button script from the Magic Attack Button assigned to the magic button prefab
                MATB.magicAttackToPerform = magicAtk; //sets the magic attack button's attack to perform to the current magic attack in the loop
                MagicButton.transform.SetParent(magicSpacer, false); //sets the parent of this button to the magic panel spacer
                if (magicAtk.attackCost > HeroesToManage[0].GetComponent<HeroStateMachine>().hero.curMP)
                {
                    MagicButton.GetComponent<Button>().interactable = false;
                } else
                {
                    MagicButton.GetComponent<Button>().interactable = true;
                }
                atkBtns.Add(MagicButton); //adds the magic button to atkBtns list for organizational purposes
            }
        } else //if the current hero does not have any magic attacks
        {
            MagicActionButton.GetComponent<Button>().interactable = false; //keeps the magic attack button from being available. could also hide it. will likely change this in the future
        }
    }

    public void SetChosenMagic(BaseAttack chosenMagic) //called after choosing a magic attack
    {
        HeroChoice.Attacker = HeroesToManage[0].name; //sets the hero choice attacker to the current hero who selected magic
        HeroChoice.AttackersGameObject = HeroesToManage[0]; //sets the hero choice attacker's game object to the current hero
        HeroChoice.Type = "Hero"; //as hero choice is a HandleTurn, sets the type to Hero

        HeroChoice.chosenAttack = chosenMagic; //sets the hero choice's chosen attack to the chosenMagic in the parameter
        MagicPanel.SetActive(false); //hides the magic panel
        EnemySelectPanel.SetActive(true); //opens the enemy select panel
        SetCancelButton(3);
        
    }

    public void MagicInput() //after clicking 'Magic' on the action panel
    {
        actionPanel.SetActive(false); //hides the action panel
        MagicPanel.SetActive(true); //displays the magic panel showing current heros magic attacks
        SetCancelButton(2);
    }

    void SetCancelButton(int option) //setting the bools for hitting the cancel button so the functionality changes depending on which menu the player is in
    {
        if (option == 0) //in action menu
        {
            enemySelectAfterAttackCancel = false;
            magicAttackCancel = false;
            enemySelectAfterMagicCancel = false;
        }

        if (option == 1) //in enemy select menu after choosing attack
        {
            enemySelectAfterAttackCancel = true;
            magicAttackCancel = false;
            enemySelectAfterMagicCancel = false;
        }

        if (option == 2) //in magic select menu
        {
            enemySelectAfterAttackCancel = false;
            magicAttackCancel = true;
            enemySelectAfterMagicCancel = false;
        }

        if (option == 3) //in enemy select menu after choosing magic
        {
            enemySelectAfterAttackCancel = false;
            magicAttackCancel = false;
            enemySelectAfterMagicCancel = true;
        }
    }

    void GetCancelButton() //detects if Cancel button is hit, and moves 'back' in the menus
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (enemySelectAfterAttackCancel)
            {
                actionPanel.SetActive(true); //shows action panel
                EnemySelectPanel.SetActive(false); //hides enemy select panel
                SetCancelButton(0);
            }
            if (magicAttackCancel)
            {
                actionPanel.SetActive(true); //shows the action panel
                MagicPanel.SetActive(false); //hides the magic panel showing current heros magic attacks
                SetCancelButton(0);
            }
            if (enemySelectAfterMagicCancel)
            {
                MagicPanel.SetActive(true); //shows the magic panel
                EnemySelectPanel.SetActive(false); //hides the enemy select panel
                SetCancelButton(2);
            }
        }
    }
}
