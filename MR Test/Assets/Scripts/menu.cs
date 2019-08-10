using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menu : MonoBehaviour
{ 
    public GameObject mainCanvas;
    public GameObject trainingCanvas;
    public GameObject monstercanvas;
    public GameObject scheduleCanvas;

    public Button monsterMenu;
    public Button closeBut, sCloseBut;
    public Button monRenameBut, saveRenameBut;

    public Button scheduleBut;
    
    monster monsterTest = new monster();

    public time time;
    
    public GameObject iField;

    // Start is called before the first frame update
    void Start()
    {
        //trainMenu.onClick.AddListener(openTraining);
        //trainBut.onClick.AddListener(train);
        closeBut.onClick.AddListener(mainMenu);
        monsterMenu.onClick.AddListener(openMonster);
        monRenameBut.onClick.AddListener(renameMonster);
        saveRenameBut.onClick.AddListener(saveRenameMonster);
        scheduleBut.onClick.AddListener(openSchedule);
        sCloseBut.onClick.AddListener(mainMenu);
        iField.SetActive(false);
        //time.month = 12; //for debugging - erase this later
        time = GameObject.Find("timeTracker").GetComponent<moveToNextDay>().time;
        GameObject.Find("dateText").GetComponent<Text>().text = time.getMonthString(time.month) + ". Week " + time.week.ToString() + ", " + time.year.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void openTraining()
    {
        trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        trainingCanvas.GetComponent<CanvasGroup>().interactable = true;

        mainCanvas.SetActive(false);
        monstercanvas.SetActive(false);
        trainingCanvas.SetActive(true);
        scheduleCanvas.SetActive(false);
    }

    void mainMenu()
    {
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;

        mainCanvas.SetActive(true);
        monstercanvas.SetActive(false);
        trainingCanvas.SetActive(false);
        scheduleCanvas.SetActive(false);
    }

    void openMonster()
    {
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;
        mainCanvas.SetActive(false);
        monstercanvas.SetActive(true);
        trainingCanvas.SetActive(false);
        scheduleCanvas.SetActive(false);
    }

    void openSchedule()
    {
        //trainingCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        //trainingCanvas.GetComponent<CanvasGroup>().interactable = true;
        mainCanvas.SetActive(false);
        monstercanvas.SetActive(false);
        trainingCanvas.SetActive(false);
        scheduleCanvas.SetActive(true);
    }

    void train()
    {
        time.nextWeek();
        GameObject.Find("dateText").GetComponent<Text>().text = time.getMonthString(time.month) + ", Week " + time.week.ToString() + ", " + time.year.ToString();
    }

    void renameMonster()
    {
        iField.SetActive(true);
    }

    void saveRenameMonster()
    {
        monsterTest.name = iField.GetComponent<InputField>().text;
        GameObject.Find("nameTxt").GetComponent<Text>().text = monsterTest.name;
        iField.SetActive(false);
    }
}
