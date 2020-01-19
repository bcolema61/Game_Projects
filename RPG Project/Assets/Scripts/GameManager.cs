using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TOOLS FOR DEBUGGING
    bool canBattle = true; //for debugging purposes, change to true to allow battles

    //TOOLS FOR DEBUGGING THAT WILL STILL BE USED
    int battleChance = 10; //lower number is lower chance battle will occur.  set higher for battle debugging purposes so battle is encountered instantly - up to maxBattleChance
    int maxBattleChance = 10; //higher number is lower chance the battle will occur depending on battleChance, default is 1000

    //DEBUGGING NOTES
    //Each hero needs it's own prefab

    //-----------------------------------
    public static GameManager instance;

    public RegionData curRegion;

    [HideInInspector] public GameObject DialogueCanvas;

    //SPAWN POINTS
    [HideInInspector] public string nextSpawnPoint;

    //HERO
    public GameObject heroCharacter;

    //POSITIONS
    public Vector2 nextHeroPosition; //is set for loading player after battle
    public Vector2 lastHeroPosition; //is set for loading player after battle

    //SCENES
    public string sceneToLoad; //to load on collisions
    public string lastScene; //to load after battle

    //BOOLS
    public bool isWalking = false; //if player is currently walking
    public bool canGetEncounter = false; //if player is able to encounter enemies
    [HideInInspector] public bool gotAttacked = false; //if player actually enters combat
    bool receivedAllExp = false;

    //ACTIVE HEROES
    public List<BaseHero> activeHeroes = new List<BaseHero>();

    //LEVELING BASES
    public int[] toLevelUp;

    //Global Game Bools
    public List<bool> globalBools = new List<bool>();

    //POSITION SAVES
    [HideInInspector] public List<BasePositionSave> positionSaves = new List<BasePositionSave>();

    //TROOPS
    public List<BaseTroop> troops = new List<BaseTroop>();

    //FROM SCRIPT STUFF
    [HideInInspector] public string battleSceneFromScript;

    

    //ENUM
    public enum GameStates
    {
        HOSTILE_STATE,
        PEACEFUL_STATE,
        BATTLE_STATE,
        IDLE
    }
    public GameStates gameState;

    
    [HideInInspector] public List<GameObject> enemiesToBattle = new List<GameObject>(); //for adding enemies in encounter to the battle
    [HideInInspector] public List<GameObject> heroesToBattle = new List<GameObject>();
    [HideInInspector] public int enemyAmount; //for how many enemies can be encountered in one battle
    [HideInInspector] public int heroAmount;

    [HideInInspector] public bool startBattleFromScript;
    

    void Awake()
    {
        GameObject.Find("DebugCamera").SetActive(false); //Disable debugging camera so main camera attached to player can be used
        DialogueCanvas = GameObject.Find("DialogueCanvas");
        DialogueCanvas.GetComponent<CanvasGroup>().alpha = 0;
        
        
        if (instance == null) //check if instance exists
        {
            instance = this; //if not set the instance to this
        }
        else if (instance != this) //if it exists but is not this instance
        {
            Destroy(gameObject); //destroy it
        }
        DontDestroyOnLoad(gameObject); //set this to be persistable across scenes

        if (!GameObject.Find("Player")) //if player gameobject doesnt exist
        {
            GameObject Hero = Instantiate(heroCharacter, nextHeroPosition, Quaternion.identity) as GameObject; //create player gameobject from prefab
            Hero.name = "Player"; //name player gameobject to "Player"
        }

        LoadPositionSaves();

    }

    void LoadPositionSaves()
    {
        if (instance.positionSaves.Count > 0)
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            for (int j = 0; j < allObjects.Length; j++)
            {
                for (int i = 0; i < instance.positionSaves.Count; i++)
                {
                    if (instance.positionSaves[i]._Name == allObjects[j].name && instance.positionSaves[i].Scene == SceneManager.GetActiveScene().name)
                    {
                        allObjects[j].transform.position = instance.positionSaves[i].newPosition;
                        break;
                    }
                }
            }
        }
    }
    

    void StartBattle(bool fromRegion)
    {
        //enemyAmount = Random.Range(1, curRegion.maxAmountEnemies + 1); //amount of enemies to be encountered
        //enemyAmount = 1; //enable for debugging purposes

        //for (int i = 0; i < enemyAmount; i++) //which enemies to add to the fight (could change this later possibly for pre-designed groups)
        //{
        //    enemiesToBattle.Add(curRegion.possibleEnemies[Random.Range(0, curRegion.possibleEnemies.Count)]); //adds random enemy from current region to enemiesToBattle list
        //}

        if (fromRegion)
        {
            getTroopsFromRegion();
        }
        
        heroAmount = activeHeroes.Count; //number of active heroes in the game manager
        

        for (int i = 0; i < heroAmount; i++) //loop through all active heroes in the game manager
        {
            heroesToBattle.Add(activeHeroes[i].heroPrefab); //spawn them in battle
        }

        GenerateHeroesToBattle(); //sets all parameters for the hero combatants to the values from their corresponding values in active hero list in game manager

        //HERO
        lastHeroPosition = GameObject.Find("Player").gameObject.transform.position; //sets player's gameObject in the world position to lastHeroPosition
        nextHeroPosition = lastHeroPosition; //next hero position should be the last hero position, to spawn back into the world after battle
        lastScene = SceneManager.GetActiveScene().name; //sets last scene to the scene before battle
        //LOAD LEVEL
        if (fromRegion)
        {
            SceneManager.LoadScene(curRegion.BattleScene); //loads battle scene
        } else
        {
            SceneManager.LoadScene(battleSceneFromScript); //loads battle scene from script
        }
        
        //RESET HERO ENCOUNTER BOOLS
        isWalking = false;
        gotAttacked = false;
        canGetEncounter = false;
    }

    private void Update()
    {
        switch(gameState)
        {
            case (GameStates.HOSTILE_STATE): //in the world, dungeon, etc. encounter possible
                if(isWalking) //if player is walking, encounter may be possible
                {
                    RandomEncounter(); //check for random encounter
                }
                if(gotAttacked) //if player is attacked
                {
                    gameState = GameStates.BATTLE_STATE; //transition to battle state
                }
            break;

            case (GameStates.PEACEFUL_STATE): //in town or area with no encounters

            break;

            case (GameStates.BATTLE_STATE):
                if (gotAttacked)
                {
                    StartBattle(true); //Loads battle scene
                } else
                {
                    StartBattle(false); //Loads battle scene
                }
                
                gameState = GameStates.IDLE;
            break;

            case (GameStates.IDLE):

            break;

        }
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad); //loads scene from collisions
    }

    public void LoadSceneAfterBattle()
    {
        SceneManager.LoadScene(lastScene); //loads last scene saved from before battle
    }

    void RandomEncounter()
    {
        if(isWalking && canGetEncounter && canBattle) //if player is walking in an encounterable zone
        {
            int battleValue = Random.Range(0, maxBattleChance); //get random value between 0 and maxBattleChance
            if(battleValue < battleChance)
            {
                Debug.Log("GameManager - RandomEncounter called");
                Debug.Log("isWalking: " + isWalking + ", canGetEncounter: " + canGetEncounter);
                Debug.Log("BattleChance: " + battleChance + " out of " + maxBattleChance + ", battleValue: " + battleValue);
                gotAttacked = true; //allows for battle to start. if this turns true during 'HOSTILE_STATE', battle is started
            }
        }
    }

    void GenerateHeroesToBattle() //when heros are instantiated in battle, the stats from game manager heros are copied over to the combatants
    {
        for (int i = 0; i < heroAmount; i++)
        {
            BaseHero heroToAdd = heroesToBattle[i].GetComponent<HeroStateMachine>().hero;
            BaseHero fromHero = activeHeroes[i];
            heroToAdd._Name = fromHero._Name;
            heroToAdd.currentLevel = fromHero.currentLevel;
            heroToAdd.currentExp = fromHero.currentExp;
            heroToAdd.baseHP = fromHero.baseHP;
            heroToAdd.curHP = fromHero.curHP;
            heroToAdd.baseMP = fromHero.baseMP;
            heroToAdd.curMP = fromHero.curMP;
            heroToAdd.baseATK = fromHero.baseATK;
            heroToAdd.curATK = fromHero.curATK;
            heroToAdd.baseMATK = fromHero.baseMATK;
            heroToAdd.curMATK = fromHero.curMATK;
            heroToAdd.baseDEF = fromHero.baseDEF;
            heroToAdd.curDEF = fromHero.curDEF;
            heroToAdd.strength = fromHero.strength;
            heroToAdd.stamina = fromHero.stamina;
            heroToAdd.intelligence = fromHero.intelligence;
            heroToAdd.dexterity = fromHero.dexterity;
            heroToAdd.agility = fromHero.agility;
            heroToAdd.spirit = fromHero.spirit;
            heroToAdd.strengthModifier = fromHero.strengthModifier;
            heroToAdd.staminaModifier = fromHero.staminaModifier;
            heroToAdd.intelligenceModifier = fromHero.intelligenceModifier;
            heroToAdd.dexterityModifier = fromHero.dexterityModifier;
            heroToAdd.agilityModifier = fromHero.agilityModifier;
            heroToAdd.attacks = fromHero.attacks;
            heroToAdd.MagicAttacks = fromHero.MagicAttacks;
        }
    }

    public void ProcessExp()
    {
        foreach (BaseHero hero in activeHeroes)
        {
            Debug.Log("currentExp: " + hero.currentExp + ", currentLevel: " + hero.currentLevel);
            while(receivedAllExp == false)
            {
                if (hero.currentExp >= toLevelUp[(hero.currentLevel)])
                {
                    hero.levelBeforeExp = hero.currentLevel; //wrong place for this, will update later
                    hero.LevelUp();
                } else
                {
                    receivedAllExp = true;
                }
            }
            receivedAllExp = false;
        }
    }

    public void DisplayPanel(bool display)
    {
        if (display)
        {
            DialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
            //Debug.Log("displaying canvas");
        }
        else
        {
            DialogueCanvas.GetComponent<CanvasGroup>().alpha = 0;
            //Debug.Log("hiding canvas");
        }
    }

    void getTroopsFromRegion()
    {
        int whichTroop = Random.Range(0, curRegion.possibleTroops.Count);
        enemyAmount = troops[whichTroop].enemies.Count;
        Debug.Log(whichTroop);
        Debug.Log(troops[whichTroop].enemies.Count);
        for (int i = 0; i < enemyAmount; i++)
        {
            Debug.Log("In the loop: " + troops[whichTroop].enemies[i].name);
            enemiesToBattle.Add(troops[whichTroop].enemies[i]);
        }
    }

    public void getBattleFromScript(int troopIndex, string scene)
    {
        for (int i = 0; i < troops[troopIndex].enemies.Count; i++)
        {
            enemiesToBattle.Add(troops[troopIndex].enemies[i]);
        }
        battleSceneFromScript = scene;
        enemyAmount = troops[troopIndex].enemies.Count;
        gameState = GameManager.GameStates.BATTLE_STATE;
    }

}
