using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

//Contains interface GameObjects for updating UI upon weekly changes
public class moveToNextDay : MonoBehaviour
{
    float fadeVal; //holds value for fadeImage GO alpha
    float fadeTime = 0.05f; //duration of fade in/out

    public GameObject mainCanvas; //Set to main canvas
    public GameObject trainingCanvas; //Set to training canvas
    public GameObject monstercanvas; //Set to monster canvas
    public GameObject infoCanvas; //Set to info canvas
    public GameObject scheduleCanvas; //Set to schedule canvas
    public GameObject progressCanvas; //Set to progress canvas
    public GameObject fadeCanvas; //Set to fade canvas
    public Text txt0, txt1, txt2, txt3, txt4, txt5, txt6, txt7;  //Schedule button text fields

    public GameObject mon, player; //Set to monster and player

    public time time = new time(); //This is set to the Time Tracker Game Object's time script

    string[] movingOnButtons = new string[8]; //Holds values for monthly schedule training buttons
    string[] movingOnButtonsTemp = new string[8]; //Holds the same values temporarily so the weekly training plan is shifted upon new week

    monsterStats monster; //Set to monster Game Object's stats
    GameObject monsterGO; //Set to monster Game Object
    string currentTraining; //The current week's training
    Vector3 strPos, intPos, mndPos, dexPos, defPos, staPos, wanPos, resPos; //monster's position for each training excersize
    Quaternion strRot, intRot, mndRot, dexRot, defRot, staRot, wanRot, resRot; //monster's rotation for each training excersize

    float tempRand1, tempRand2, tempRand3; //Used for calculating randomness in weekly stat gain
    float tempF1, tempF2; //Temporary values for monster's stat flex
    float tempResult; //Holds the result of the above calculations

    public RadarPolygon chart; //Holds the radar chart for weekly progress UI

    public ProgressBar progBar, energyBar, hungerBar; //Holds the progress bars for weekly progress UI

    public GameObject regUp, greatUp; //Images for regular increase vs great increase

    private int currentStat; //Set to whichever stat is being trained
    private int statIncNum; //Set to the value the stat increases by
    private int progVal; //This just keeps the value of the progress bar between 1 and 100 depending on the current value

    

    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.Find("timeTracker").GetComponent<moveToNextDay>().time;

        monsterGO = GameObject.Find("monster").transform.GetChild(0).gameObject;
        monster = GameObject.Find("monster").GetComponent<monsterStats>();

        strPos = new Vector3(19.93f, 30.0f, 53.61f);
        strRot = new Quaternion(0.0f, 185.875f, 0.0f, 0.0f);

        intPos = new Vector3(29.57f, 30.0f, 56.82f);
        intRot = new Quaternion(0.0f, 233.593f, 0.0f, 0.0f);

        mndPos = new Vector3(35.2f, 30.0f, 50.27f);
        mndRot = new Quaternion(0.0f, 221.398f, 0.0f, 0.0f);

        dexPos = new Vector3(44.22f, 30.0f, 49.67f);
        dexRot = new Quaternion(0.0f, 226.688f, 0.0f, 0.0f);

        staPos = new Vector3(55.38f, 30.0f, 34.06f);
        staRot = new Quaternion(0.0f, 250.971f, 0.0f, 0.0f);

        defPos = new Vector3(52.02f, 30.0f, 43.4f);
        defRot = new Quaternion(0.0f, 230.137f, 0.0f, 0.0f);

        wanPos = new Vector3(40.47f, 30.0f, 34.06f);
        wanRot = new Quaternion(0.0f, 55.594f, 0.0f, 0.0f);

        resPos = new Vector3(24.16f, 30.0f, 18.32f);
        resRot = new Quaternion(0.0f, 3.527f, 0.0f, 0.0f);

        currentTraining = txt0.text;
        doTrainingForWeek();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(runNextday());
        }
    }

    public void OnTriggerStay(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    private void fillRadarStats()
    {
        chart.value[0] = (float)monster.dexterity * .001f;
        chart.value[1] = (float)monster.strength * .001f;
        chart.value[2] = (float)monster.intelligence * .001f;
        chart.value[3] = (float)monster.mind * .001f;
        chart.value[4] = (float)monster.stamina * .001f;
        chart.value[5] = (float)monster.defense * .001f;
    }

    private void setProgTrain()
    {
        if (currentTraining == "Strength")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Strength";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.strength.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.strength).ToString();
        }
        if (currentTraining == "Dexterity")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Dexterity";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.dexterity.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.dexterity).ToString();
        }
        if (currentTraining == "Intelligence")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Intelligence";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.intelligence.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.intelligence).ToString();
        }
        if (currentTraining == "Mind")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Mind";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.mind.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.mind).ToString();
        }
        if (currentTraining == "Defense")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Defense";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.defense.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.defense).ToString();
        }
        if (currentTraining == "Stamina")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Stamina";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.stamina.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.stamina).ToString();
        }
        if (currentTraining == "Rest")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = "";
            GameObject.Find("pLevelNum").GetComponent<Text>().text = "";
        }
        if (currentTraining == "Wander")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Stamina";
            GameObject.Find("pNextLevelStat").GetComponent<Text>().text = monster.stamina.ToString();
            GameObject.Find("pLevelNum").GetComponent<Text>().text = getTrainLevel(monster.stamina).ToString();
        }

        if (currentTraining == "Rest")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Rest";
            GameObject.Find("pStatIncNum").GetComponent<Text>().text = "";
            GameObject.Find("pLevelStat").GetComponent<Text>().text = "";
            progBar.BarValue = 0;
        } else if (currentTraining == "Wander")
        {
            GameObject.Find("pTrainingText").GetComponent<Text>().text = "Wander";
            GameObject.Find("pStatIncNum").GetComponent<Text>().text = "";
            GameObject.Find("pLevelStat").GetComponent<Text>().text = "";
        } else
        {
            GameObject.Find("pStatIncNum").GetComponent<Text>().text = statIncNum.ToString();
            GameObject.Find("pLevelStat").GetComponent<Text>().text = currentStat.ToString();
            progBar.BarValue = progVal * 2; //next step is to lerp this so it gradually fills
        }
        
        fillRadarStats();
    }

    private int getTrainLevel(int stat)
    {
        if (stat <= 49)
        {
            progVal = stat;
            return 1;
        }
        if (stat >=50 && stat <= 99)
        {
            progVal = stat - 50;
            return 2;
        }
        if (stat >=100 && stat <= 149)
        {
            progVal = stat - 100;
            return 3;
        }
        if (stat >= 150 && stat <= 99)
        {
            progVal = stat - 150;
            return 4;
        }
        if (stat >= 200 && stat <= 149)
        {
            progVal = stat - 200;
            return 5;
        }
        if (stat >= 250 && stat <= 99)
        {
            progVal = stat - 250;
            return 6;
        }
        if (stat >= 300 && stat <= 149)
        {
            progVal = stat - 300;
            return 7;
        }
        if (stat >= 350 && stat <= 99)
        {
            progVal = stat - 350;
            return 8;
        }
        if (stat >= 400 && stat <= 149)
        {
            progVal = stat - 400;
            return 9;
        }
        if (stat >= 450 && stat <= 99)
        {
            progVal = stat - 450;
            return 10;
        }
        if (stat >= 500 && stat <= 149)
        {
            progVal = stat - 500;
            return 11;
        }
        if (stat >= 550 && stat <= 99)
        {
            progVal = stat - 550;
            return 12;
        }
        if (stat >= 600 && stat <= 149)
        {
            progVal = stat - 600;
            return 13;
        }
        if (stat >= 650 && stat <= 99)
        {
            progVal = stat - 650;
            return 14;
        }
        if (stat >= 700 && stat <= 149)
        {
            progVal = stat - 700;
            return 15;
        }
        if (stat >= 750 && stat <= 99)
        {
            progVal = stat - 750;
            return 16;
        }
        if (stat >= 800 && stat <= 149)
        {
            progVal = stat - 800;
            return 17;
        }
        if (stat >= 850 && stat <= 99)
        {
            progVal = stat - 8500;
            return 18;
        }
        if (stat >= 900 && stat <= 149)
        {
            progVal = stat - 900;
            return 19;
        }
        if (stat >= 950)
        {
            progVal = stat - 950;
            return 20;
        }

        else
        {
            return 0;
        }
        
    }

    private void setProgEnergy()
    {
        energyBar.BarValue = monster.monster.energyVal;
    }

    private void setProgHunger()
    {
        hungerBar.BarValue = monster.monster.hungerVal;
    }

    private void setRegTrain()
    {
        regUp.SetActive(true);
        greatUp.SetActive(false);
    }

    private void setGreatTrain()
    {
        regUp.SetActive(false);
        greatUp.SetActive(true);
    }

    private void resetTrain()
    {
        regUp.SetActive(false);
        greatUp.SetActive(false);
    }

    IEnumerator runNextday()
    {
        player.GetComponent<PlayerMotor>().stopMovement();
        player.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(testFadeOut());

        yield return new WaitForSeconds(2.0f);
        displayProg();
        DoLast();
                
        yield return new WaitForSeconds(10.0f);
               
        StartCoroutine(testFadeIn());
        hideProg();
        resetTrain();
        yield return null;
    }

    void DoLast()
    {
        //insert stuff here to happen before screen is faded back in
        player.transform.position = new Vector3(20.23f, 30.0f, 29.64f);
        mon.transform.position = new Vector3(13.06f, 30.0f, 53.32f);
        player.GetComponent<PlayerController>().enabled = true;

        mon.GetComponent<monsterStats>().monster.moveEnergy();

        mon.GetComponent<monsterStats>().monster.moveHunger();

        time.nextWeek();

        shiftSchedule();

        doTrainingForWeek();

        upMonsterAge(monster.monster);

        //update Progress UI
        fillRadarStats();
        setProgTrain();
        setProgEnergy();
        setProgHunger();

        GameObject.Find("pDateTxt").GetComponent<Text>().text = GameObject.Find("dateText").GetComponent<Text>().text.ToString();

        //and done        
    }

    void upMonsterAge(monster mon)
    {
        mon.weeksAge++;

        if (mon.weeksAge == 5)
        {
            mon.monthsAge++;
            mon.weeksAge = 1;
        }

        if (mon.monthsAge == 13)
        {
            mon.yearsAge++;
            mon.monthsAge = 1;
        }
    }

    void shiftSchedule()
    {
        buildArray();
        shiftFromArray();
        currentTraining = movingOnButtons[0];
    }

    void buildArray()
    {
        movingOnButtons[0] = txt0.text;
        movingOnButtons[1] = txt1.text;
        movingOnButtons[2] = txt2.text;
        movingOnButtons[3] = txt3.text;
        movingOnButtons[4] = txt4.text;
        movingOnButtons[5] = txt5.text;
        movingOnButtons[6] = txt6.text;
        movingOnButtons[7] = txt7.text;

        movingOnButtonsTemp[0] = movingOnButtons[1];
        movingOnButtonsTemp[1] = movingOnButtons[2];
        movingOnButtonsTemp[2] = movingOnButtons[3];
        movingOnButtonsTemp[3] = movingOnButtons[4];
        movingOnButtonsTemp[4] = movingOnButtons[5];
        movingOnButtonsTemp[5] = movingOnButtons[6];
        movingOnButtonsTemp[6] = movingOnButtons[7];
        movingOnButtonsTemp[7] = movingOnButtons[0];

        movingOnButtons[0] = movingOnButtonsTemp[0];
        movingOnButtons[1] = movingOnButtonsTemp[1];
        movingOnButtons[2] = movingOnButtonsTemp[2];
        movingOnButtons[3] = movingOnButtonsTemp[3];
        movingOnButtons[4] = movingOnButtonsTemp[4];
        movingOnButtons[5] = movingOnButtonsTemp[5];
        movingOnButtons[6] = movingOnButtonsTemp[6];
        movingOnButtons[7] = movingOnButtonsTemp[7];
    }

    void shiftFromArray()
    {
        txt0.text = movingOnButtons[0];
        txt1.text = movingOnButtons[1];
        txt2.text = movingOnButtons[2];
        txt3.text = movingOnButtons[3];
        txt4.text = movingOnButtons[4];
        txt5.text = movingOnButtons[5];
        txt6.text = movingOnButtons[6];
        txt7.text = movingOnButtons[7];
    }

    void doTrainingForWeek()
    {
        turnOffTraining();

        if (currentTraining == "Strength")
        {
            monsterGO.transform.position = strPos;
            monsterGO.transform.rotation = strRot;
            trainStr();
        }

        if (currentTraining == "Intelligence")
        {
            monsterGO.transform.position = intPos;
            monsterGO.transform.rotation = intRot;
            trainInt();
        }

        if (currentTraining == "Mind")
        {
            monsterGO.transform.position = mndPos;
            monsterGO.transform.rotation = mndRot;
            trainMnd();
        }

        if (currentTraining == "Dexterity")
        {
            monsterGO.transform.position = dexPos;
            monsterGO.transform.rotation = dexRot;
            trainDex();
        }

        if (currentTraining == "Stamina")
        {
            monsterGO.transform.position = staPos;
            monsterGO.transform.rotation = staRot;
            trainSta();
        }

        if (currentTraining == "Defense")
        {
            monsterGO.transform.position = defPos;
            monsterGO.transform.rotation = defRot;
            trainDef();
        }

        if (currentTraining == "Wander")
        {
            monsterGO.transform.position = wanPos;
            monsterGO.transform.rotation = wanRot;
            trainWander();
        }

        if (currentTraining == "Rest")
        {
            monsterGO.transform.position = resPos;
            monsterGO.transform.rotation = resRot;
            trainRest();
        }

        if (tempRand1 >= 1.0f && tempRand1 <= 3.0f)
        {
            setRegTrain();
        }
        else
        {
            setGreatTrain();
        }
    }

    float getRandFromEnergy()
    {
        if (monster.monster.energyIndex == 0)
        {
            return Random.Range(3.0f, 6.0f);
        }
        if (monster.monster.energyIndex == 1)
        {
            return Random.Range(3.0f, 5.0f);
        }
        if (monster.monster.energyIndex == 2)
        {
            return Random.Range(2.0f, 5.0f);
        }
        if (monster.monster.energyIndex == 3)
        {
            return Random.Range(2.0f, 4.0f);
        }
        if (monster.monster.energyIndex == 4)
        {
            return Random.Range(1.0f, 3.0f);
        }
        if (monster.monster.energyIndex == 5)
        {
            return Random.Range(1.0f, 2.0f);
        }
        else
        {
            return 0.0f;
        }
    }

    float getValFromAge()
    {
        return monster.monster.yearsAge * 0.75f;
    }

    void trainStr()
    {
        currentStat = monster.strength;
        tempRand1 = Random.Range(1.0f, 5.0f);
        tempF1 = monster.monster.strLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.strength = monster.strength + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Strength increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainInt()
    {
        currentStat = monster.intelligence;
        tempRand1 = Random.Range(2.0f, 5.0f);
        tempF1 = monster.monster.intLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.intelligence = monster.intelligence + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Intelligence increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainMnd()
    {
        currentStat = monster.mind;
        tempRand1 = Random.Range(2.0f, 5.0f);
        tempF1 = monster.monster.mndLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.mind = monster.mind + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Mind increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainDex()
    {
        currentStat = monster.dexterity;
        tempRand1 = Random.Range(2.0f, 5.0f);
        tempF1 = monster.monster.dexLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.dexterity = monster.dexterity + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Dexterity increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainDef()
    {
        currentStat = monster.defense;
        tempRand1 = Random.Range(2.0f, 5.0f);
        tempF1 = monster.monster.defLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.defense = monster.defense + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Defense increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainSta()
    {
        currentStat = monster.stamina;
        tempRand1 = Random.Range(2.0f, 5.0f);
        tempF1 = monster.monster.staLevFlex;
        tempResult = ((tempRand1 * tempF1) - 1.5f * (getValFromAge())) + (getRandFromEnergy() - 3.0f);
        tempResult = Mathf.Round(tempResult);
        statIncNum = Mathf.RoundToInt(tempResult);

        monster.stamina = monster.stamina + Mathf.RoundToInt(tempResult);
        Debug.Log(monster.name + " - Stamina increased by " + Mathf.RoundToInt(tempResult));
    }

    void trainRest()
    {
        monster.monster.rest();
    }

    void trainWander()
    {
        monsterGO.GetComponent<wander>().enabled = true;
    }

    void turnOffTraining()
    {
        monsterGO.GetComponent<wander>().enabled = false;
    }

    void displayProg()
    {
        progressCanvas.gameObject.SetActive(true);
    }

    void hideProg()
    {
        progressCanvas.gameObject.SetActive(false);
    }

    public IEnumerator testFadeOut()
    {
        while (true)
        {
            fadeCanvas.SetActive(true);
            fadeVal = GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha;

            if (fadeVal < 1.0f)
            {
                GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha += .05f;
                mainCanvas.GetComponent<CanvasGroup>().alpha -= .05f;
                infoCanvas.GetComponent<CanvasGroup>().alpha -= .05f;
                yield return new WaitForSeconds(fadeTime);
            }
            else
            {
                yield return new WaitForSeconds(3.0f);
                break;
            }
        }
    }

    public IEnumerator testFadeIn()
    {
        runFadeIn();
        while (true)
        {
            fadeVal = GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha;

            if (fadeVal > 0.0f)
            {
                GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha -= .05f;
                mainCanvas.GetComponent<CanvasGroup>().alpha += .05f;
                infoCanvas.GetComponent<CanvasGroup>().alpha += .05f;
                yield return new WaitForSeconds(fadeTime);
            }
            else
            {
                fadeCanvas.SetActive(false);
                break;
            }
        }
    }

    void runFade()
    {
        //mainCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //mainCanvas.GetComponent<CanvasGroup>().interactable = false;
        //monstercanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //monstercanvas.GetComponent<CanvasGroup>().interactable = false;
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = false;
        //infoCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //infoCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    void runFadeIn()
    {
        //mainCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //mainCanvas.GetComponent<CanvasGroup>().interactable = true;
        mainCanvas.SetActive(true);
        //monstercanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //monstercanvas.GetComponent<CanvasGroup>().interactable = true;
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;
        //infoCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //infoCanvas.GetComponent<CanvasGroup>().interactable = true;
    }


}
