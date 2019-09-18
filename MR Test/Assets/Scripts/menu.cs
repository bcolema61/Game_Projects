using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class menu : MonoBehaviour //This operates the menu navigation and displays some data
{ 
    public GameObject mainCanvas; //Set to main canvas
    public GameObject monstercanvas; //Set to monster canvas
    public GameObject scheduleCanvas; //Set to schedule canvas
    public GameObject dataCanvas; //Set to data canvas

    public Button monsterMenu, dataMenu; //Set to monster and data buttons
    public Button closeBut, sCloseBut, dCloseBut; //Set to close buttons
    public Button monRenameBut, saveRenameBut; //Set to rename buttons

    public Button scheduleBut; //Set to schedule button
    
    monsterStats monster; //Set to current monster

    public time time;  //Set to time tracker
    
    public GameObject iField; //Set to monster rename text input field

    ProgressBar energyBar, hungerBar; //Energy and Hunger Progress Bars
    RadarPolygon chart; //Monster stats radar chart

    // Start is called before the first frame update
    void Start()
    {
        closeBut.onClick.AddListener(mainMenu); //Runs mainMenu when clicking on 'Close' in Monster menu
        monsterMenu.onClick.AddListener(openMonster); //Runs openMonster when clicking on monster menu button
        monRenameBut.onClick.AddListener(renameMonster); //Runs renameMonster when clicking on rename monster button
        saveRenameBut.onClick.AddListener(saveRenameMonster); //Runs saveRenameMonster when clicking on save button when renaming monster
        scheduleBut.onClick.AddListener(openSchedule); //Runs openSchedule when clicking on schedule button
        sCloseBut.onClick.AddListener(mainMenu); //Runs mainMenu when clicking on 'close' in schedule menu
        dataMenu.onClick.AddListener(openData); //Runs openData when clicking on data button
        dCloseBut.onClick.AddListener(mainMenu); //Runs mainMenu when clicking on close in data menu
        //time.month = 12; //**for debugging** - sets current month to whenever
        time = GameObject.Find("timeTracker").GetComponent<moveToNextDay>().time; //Sets time to current time tracked in the game - this is set on 'timeTracker' object
        GameObject.Find("dateText").GetComponent<Text>().text = time.getMonthString(time.month) + ". Week " + time.week.ToString() + ", " + time.year.ToString(); //Sets date on the info panel to the current time

        monster = GameObject.Find("monster").GetComponent<monsterStats>();//Sets monster to current monster in game

        dataCanvas.SetActive(true); //Enables data canvas to set progress bars
        hungerBar = GameObject.Find("dHungerProgBar").GetComponent<ProgressBar>(); //Sets hunger bar to data canvas hunger bar
        energyBar = GameObject.Find("dEnergyProgBar").GetComponent<ProgressBar>(); //Sets energy bar to data canvas energy bar
        chart = GameObject.Find("dRadarPolygonData").GetComponent<RadarPolygon>(); //Sets chart to data canvas chart
        dataCanvas.SetActive(false); //Disables data canvas as progress bars are ready
    }

    void openData()
    {
        //Hides menu canvas and displays Data canvas
        mainCanvas.SetActive(false);
        dataCanvas.SetActive(true);
        
        SetDEnergyProgBar(); //Sets energy bar to value of monster's energy
        SetDHungerProgBar(); //Sets hunger bar to value of monster's hunger
        SetDChart(); //Sets each value of monster's parameters in the radar chart
    }

    public void SetDEnergyProgBar()
    {
        energyBar.BarValue = monster.monster.energyVal; //Sets energy bar to value of monster's energy
    }

    public void SetDHungerProgBar()
    {
        hungerBar.BarValue = monster.monster.hungerVal; //Sets hunger bar to value of monster's hunger
    }

    public void SetDChart()  //Sets each value of monster's parameters in the radar chart
    {
        chart.value[0] = (float)monster.dexterity * .001f;
        chart.value[1] = (float)monster.strength * .001f;
        chart.value[2] = (float)monster.intelligence * .001f;
        chart.value[3] = (float)monster.mind * .001f;
        chart.value[4] = (float)monster.stamina * .001f;
        chart.value[5] = (float)monster.defense * .001f;
    }


    void mainMenu()
    {
        //Hides all canvases and displays main menu canvas
        mainCanvas.SetActive(true);
        monstercanvas.SetActive(false);
        scheduleCanvas.SetActive(false);
        dataCanvas.SetActive(false);
    }

    void openMonster()
    {
        //Hides menu canvas and displays monster canvas
        mainCanvas.SetActive(false);
        monstercanvas.SetActive(true);
    }

    void openSchedule()
    {
        //Hides menu canvas and displays schedule canvas
        mainCanvas.SetActive(false);
        scheduleCanvas.SetActive(true);
    }

    void renameMonster()
    {
        iField.SetActive(true); //Displays rename monster text input field
    }

    void saveRenameMonster()
    {
        monster.name = iField.GetComponent<InputField>().text; //Sets monster's name to value in monster rename field
        GameObject.Find("nameTxt").GetComponent<Text>().text = monster.name; //Changes name in info panel to new monster's name
        iField.SetActive(false); //Hides the rename monster text input
    }
}
