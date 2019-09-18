using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scheduleMenu : MonoBehaviour
{
    public Button but0, but1, but2, but3, but4, but5, but6, but7;

    public Text but0Txt, but1Txt, but2Txt, but3Txt, but4Txt, but5Txt, but6Txt, but7Txt;

    public Button strBut, mndBut, dexBut, defBut, intBut, staBut, restBut, wanderBut;

    public Button saveBut, closeBut, tCloseBut;

    public GameObject weeklyCanvas, trainingCanvas;

    Button updateBut;

    Text butText;

    public Text dateText, descText, statTxt, statValTxt;

    bool buttonClicked;
    string trainingMod, trainingStat;

    public time time = new time();
    int week;
    int month;
    int year;
    int tempWeek;
    int tempMonth;
    int tempYear;
    int pWeek = 1;
    int pMonth = 1;
    int pYear = 2000;
    string weekString;

    Button selBut, selTBut;

    Button selAtr;

    monsterStats monster;

    // Start is called before the first frame update
    void Start()
    {
        //but0.onClick.AddListener(delegate { openSchedule(); });
        but1.onClick.AddListener(delegate { openSchedule(); });
        but2.onClick.AddListener(delegate { openSchedule(); });
        but3.onClick.AddListener(delegate { openSchedule(); });
        but4.onClick.AddListener(delegate { openSchedule(); });
        but5.onClick.AddListener(delegate { openSchedule(); });
        but6.onClick.AddListener(delegate { openSchedule(); });
        but7.onClick.AddListener(delegate { openSchedule(); });

        strBut.onClick.AddListener(delegate { onStrButClick(); });
        mndBut.onClick.AddListener(delegate { onMndButClick(); });
        dexBut.onClick.AddListener(delegate { onDexButClick(); });
        defBut.onClick.AddListener(delegate { onDefButClick(); });
        intBut.onClick.AddListener(delegate { onIntButClick(); });
        staBut.onClick.AddListener(delegate { onStaButClick(); });

        restBut.onClick.AddListener(delegate { onRestButClick(); });
        wanderBut.onClick.AddListener(delegate { onWanderButClick(); });

        saveBut.onClick.AddListener(delegate { onSaveButClick(); });
        tCloseBut.onClick.AddListener(delegate { closeSchedule(); });


        weeklyCanvas.SetActive(true); //might need to fix this later
        dateText.text = GameObject.Find("dateText").GetComponent<Text>().text; //this too for sure

        time = GameObject.Find("timeTracker").GetComponent<moveToNextDay>().time;
        week = time.week;
        month = time.month;
        year = time.year;


        monster = GameObject.Find("monster").GetComponent<monsterStats>();

        buttonClicked = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void placeHold()
    {
        //Debug.Log("penis");

        if (selBut == but0)
        {
            //Debug.Log("Penis");
        }


    }

    void onStrButClick()
    {
        buttonClicked = true;
        selTBut = strBut;
        selAtr = strBut;

        trainingStat = monster.strength.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onMndButClick()
    {
        buttonClicked = true;
        selTBut = mndBut;
        selAtr = mndBut;

        trainingStat = monster.mind.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onDexButClick()
    {
        buttonClicked = true;
        selTBut = dexBut;
        selAtr = dexBut;

        trainingStat = monster.dexterity.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onStaButClick()
    {
        buttonClicked = true;
        selTBut = staBut;
        selAtr = staBut;

        trainingStat = monster.stamina.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onIntButClick()
    {
        buttonClicked = true;
        selTBut = intBut;
        selAtr = intBut;

        trainingStat = monster.intelligence.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onDefButClick()
    {
        buttonClicked = true;
        selTBut = defBut;
        selAtr = defBut;

        trainingStat = monster.defense.ToString();
        trainingMod = "+";
        statTxt.text = trainingStat;
        statValTxt.text = trainingMod;
    }
    void onRestButClick()
    {
        buttonClicked = true;
        selTBut = restBut;
        selAtr = restBut;

        //trainingStat = monster.strength.ToString();
        //trainingMod = "+";
        //statTxt.text = trainingStat;
        //statValTxt.text = trainingMod;
    }
    void onWanderButClick()
    {
        selTBut = wanderBut;
        selAtr = wanderBut;
    }
    void onSaveButClick()
    { 
        if (selBut == but0)
        {
            if (selAtr == strBut)
            {
                but0Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but0Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but0Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but0Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but0Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but0Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but0Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but0Txt.text = "Wander";
            }
        }
        if (selBut == but1)
        {
            if (selAtr == strBut)
            {
                but1Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but1Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but1Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but1Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but1Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but1Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but1Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but1Txt.text = "Wander";
            }
        }
        if (selBut == but2)
        {
            if (selAtr == strBut)
            {
                but2Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but2Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but2Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but2Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but2Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but2Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but2Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but2Txt.text = "Wander";
            }
        }
        if (selBut == but3)
        {
            if (selAtr == strBut)
            {
                but3Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but3Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but3Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but3Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but3Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but3Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but3Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but3Txt.text = "Wander";
            }
        }
        if (selBut == but4)
        {
            if (selAtr == strBut)
            {
                but4Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but4Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but4Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but4Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but4Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but4Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but4Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but4Txt.text = "Wander";
            }
        }
        if (selBut == but5)
        {
            if (selAtr == strBut)
            {
                but5Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but5Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but5Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but5Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but5Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but5Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but5Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but5Txt.text = "Wander";
            }
        }
        if (selBut == but6)
        {
            if (selAtr == strBut)
            {
                but6Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but6Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but6Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but6Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but6Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but6Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but6Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but6Txt.text = "Wander";
            }
        }
        if (selBut == but7)
        {
            if (selAtr == strBut)
            {
                but7Txt.text = "Strength";
            }
            if (selAtr == dexBut)
            {
                but7Txt.text = "Dexterity";
            }
            if (selAtr == intBut)
            {
                but7Txt.text = "Intelligence";
            }
            if (selAtr == mndBut)
            {
                but7Txt.text = "Mind";
            }
            if (selAtr == defBut)
            {
                but7Txt.text = "Defense";
            }
            if (selAtr == staBut)
            {
                but7Txt.text = "Stamina";
            }
            if (selAtr == restBut)
            {
                but7Txt.text = "Rest";
            }
            if (selAtr == wanderBut)
            {
                but7Txt.text = "Wander";
            }
        }
        selTBut = null;
        closeSchedule();
        buttonClicked = false;
    }

    void openSchedule()
    {
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;
        
        weeklyCanvas.SetActive(false);
        trainingCanvas.SetActive(true);
    }

    void closeSchedule()
    {
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;

        weeklyCanvas.SetActive(true);
        trainingCanvas.SetActive(false);
        buttonClicked = false;
    }
    
    string getDateForText(Button but)
    {
        string temp;
        week = time.week;
        month = time.month;
        year = time.year;

        tempWeek = week;
        tempMonth = month;
        tempYear = year;

        pMonth = month - 1;

        // dateText.text = time.getMonthString(month) + ", " + "Week " + week.ToString() + "," + year.ToString();

        // if (pWeek == 0)
        //  {
        //      tempMonth = month - 1;
        //        monthString = time.getMonthString(tempMonth);
        //     }

        //----------

        if (but == but0)
        {
            tempWeek = week;

        }

        if (but == but1)
        {
            tempWeek = week + 1;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
        }

        if (but == but2)
        {
            tempWeek = week + 2;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
        }

        if (but == but3)
        {
            tempWeek = week + 3;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
            if (tempWeek == 7)
            {
                tempWeek = 3;
                tempMonth = month + 1;
            }
        }

        if (but == but4)
        {
            tempWeek = week + 4;
            
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
            if (tempWeek == 7)
            {
                tempWeek = 3;
                tempMonth = month + 1;
            }
            if (tempWeek == 8)
            {
                tempWeek = 4;
                tempMonth = month + 1;
            }
        }

        if (but == but5)
        {
            tempWeek = week + 5;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
            if (tempWeek == 7)
            {
                tempWeek = 3;
                tempMonth = month + 1;
            }
            if (tempWeek == 8)
            {
                tempWeek = 4;
                tempMonth = month + 1;
            }
            if (tempWeek == 9)
            {
                tempWeek = 1;
                tempMonth = month + 2;
            }
        }

        if (but == but6)
        {
            tempWeek = week + 6;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
            if (tempWeek == 7)
            {
                tempWeek = 3;
                tempMonth = month + 1;
            }
            if (tempWeek == 8)
            {
                tempWeek = 4;
                tempMonth = month + 1;
            }
            if (tempWeek == 9)
            {
                tempWeek = 1;
                tempMonth = month + 2;
            }
            if (tempWeek == 10)
            {
                tempWeek = 2;
                tempMonth = month + 2;
            }
        }

        if (but == but7)
        {
            tempWeek = week + 7;
            if (tempWeek == 5)
            {
                tempWeek = 1;
                tempMonth = month + 1;
            }
            if (tempWeek == 6)
            {
                tempWeek = 2;
                tempMonth = month + 1;
            }
            if (tempWeek == 7)
            {
                tempWeek = 3;
                tempMonth = month + 1;
            }
            if (tempWeek == 8)
            {
                tempWeek = 4;
                tempMonth = month + 1;
            }
            if (tempWeek == 9)
            {
                tempWeek = 1;
                tempMonth = month + 2;
            }
            if (tempWeek == 10)
            {
                tempWeek = 2;
                tempMonth = month + 2;
            }
            if (tempWeek == 11)
            {
                tempWeek = 3;
                tempMonth = month + 2;
            }

        }

        //Debug.Log(tempMonth.ToString());

        //if (pMonth == 0 && year > 2000)
        //{
        //    tempYear = year - 1;
        //}

        if (tempMonth == 13 || tempMonth == 14)
        {
            tempYear++;
        }


   //     pWeek = week - 1;

        temp = time.getMonthString(tempMonth) + ". " + "Week " + tempWeek.ToString() + ", " + tempYear.ToString();

        return temp;
    }



    public void changeDescOnHover(Button but)
    {
        //Debug.Log(but);

        //if but == but0
            if (butText.text == "Wander")
            {
                descText.text = ":)";
            }
            if (butText.text == "Strength")
            {
                descText.text = monster.strength.ToString();
            }
            if (butText.text == "Intelligence")
            {
                descText.text = monster.intelligence.ToString();
            }
            if (butText.text == "Mind")
            {
                descText.text = monster.mind.ToString();
            }
            if (butText.text == "Dexterity")
            {
                descText.text = monster.dexterity.ToString();
            }
            if (butText.text == "Defense")
            {
                descText.text = monster.defense.ToString();
            }
            if (butText.text == "Stamina")
            {
                descText.text = monster.stamina.ToString();
            }
            if (butText.text == "Rest")
            {
                descText.text = monster.monster.energy[monster.monster.energyIndex];
            }
    }

    
    public void ChangeSched0OnHover()
    {
        butText = GameObject.Find("sBut0Txt").GetComponent<Text>();
        changeDescOnHover(but0);
        dateText.text = getDateForText(but0);
        selBut = but0;
    }
    public void ChangeSched1OnHover()
    {
        butText = GameObject.Find("sBut1Txt").GetComponent<Text>();
        changeDescOnHover(but1);
        dateText.text = getDateForText(but1);
        selBut = but1;
    }
    public void ChangeSched2OnHover()
    {
        butText = GameObject.Find("sBut2Txt").GetComponent<Text>();
        changeDescOnHover(but2);
        dateText.text = getDateForText(but2);
        selBut = but2;
    }
    public void ChangeSched3OnHover()
    {
        butText = GameObject.Find("sBut3Txt").GetComponent<Text>();
        changeDescOnHover(but3);
        dateText.text = getDateForText(but3);
        selBut = but3;
    }
    public void ChangeSched4OnHover()
    {
        butText = GameObject.Find("sBut4Txt").GetComponent<Text>();
        changeDescOnHover(but4);
        dateText.text = getDateForText(but4);
        selBut = but4;
    }
    public void ChangeSched5OnHover()
    {
        butText = GameObject.Find("sBut5Txt").GetComponent<Text>();
        changeDescOnHover(but5);
        dateText.text = getDateForText(but5);
        selBut = but5;
    }
    public void ChangeSched6OnHover()
    {
        butText = GameObject.Find("sBut6Txt").GetComponent<Text>();
        changeDescOnHover(but6);
        dateText.text = getDateForText(but6);
        selBut = but6;
    }
    public void ChangeSched7OnHover()
    {
        butText = GameObject.Find("sBut7Txt").GetComponent<Text>();
        changeDescOnHover(but7);
        dateText.text = getDateForText(but7);
        selBut = but7;
    }

    public void changeTrainingOnHover(Button but)
    {
        //Debug.Log(but);

        //if but == but0
        if (buttonClicked == false)
        { 
            if (but == strBut)
            {
                trainingStat = monster.strength.ToString();
                trainingMod = "+";
            }
            if (but == dexBut)
            {
                trainingStat = monster.dexterity.ToString();
                trainingMod = "+";
            }
            if (but == intBut)
            {
                trainingStat = monster.intelligence.ToString();
                trainingMod = "+";
            }
            if (but == mndBut)
            {
                trainingStat = monster.mind.ToString();
                trainingMod = "+";
            }
            if (but == defBut)
            {
                trainingStat = monster.defense.ToString();
                trainingMod = "+";
            }
            if (but == staBut)
            {
                trainingStat = monster.stamina.ToString();
                trainingMod = "+";
            }
            if (but == restBut)
            {
                //statTxt.text = monster.rest.ToString();
            }
            if (but == wanderBut)
            {
                //statTxt.text = monster.strength.ToString();
            }
            statTxt.text = trainingStat;
            statValTxt.text = trainingMod;
        }
        //Debug.Log(but);
    }


    public void ChangeStrOnHover()
    {
        changeTrainingOnHover(strBut);
        //Debug.Log("ChangeStrOnHover");
    }
    public void ChangeDexOnHover()
    {
        changeTrainingOnHover(dexBut);
        //Debug.Log("ChangeDexOnHover");
    }
    public void ChangeIntOnHover()
    {
        changeTrainingOnHover(intBut);
        //Debug.Log("ChangeIntOnHover");
    }
    public void ChangeMndOnHover()
    {
        changeTrainingOnHover(mndBut);
        //Debug.Log("ChangeMndOnHover");
    }
    public void ChangeDefOnHover()
    {
        changeTrainingOnHover(defBut);
        //Debug.Log("ChangeDefOnHover");
    }
    public void ChangeStaOnHover()
    {
        changeTrainingOnHover(staBut);
        //Debug.Log("ChangeStaOnHover");
    }
    public void ChangeRestOnHover()
    {
        changeTrainingOnHover(restBut);
        //Debug.Log("ChangeRestOnHover");
    }
    public void ChangeWanderOnHover()
    {
        changeTrainingOnHover(wanderBut);
        //Debug.Log("ChangeWanderOnHover");
    }


}
