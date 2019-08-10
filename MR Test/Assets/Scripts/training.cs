using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class training : MonoBehaviour
{
    //int statNum;

    public monsterStats monster;

    time weekCounter = new time();

    public Button strButton, dexButton, intButton, mndButton, defButton, staButton;
    public Button trainButton;

    public GameObject mainCanvas;
    public GameObject trainingCanvas;
    public GameObject monstercanvas;
    public GameObject infoCanvas;

    public GameObject mon;

    int newNum = 0; //temp int
    string numString; //temp string

    public bool isSelected;

    public Button selectedButton;

    float fadeVal;
    float fadeTime = 0.05f;
    bool fadeOutBool;
    bool fadeInBool;

    //public Button theButton;

    void Start()
    {
        strButton.onClick.AddListener(delegate { trainStr(); });
        dexButton.onClick.AddListener(delegate { trainDex(); });
        intButton.onClick.AddListener(delegate { trainInt(); });
        mndButton.onClick.AddListener(delegate { trainMnd(); });
        defButton.onClick.AddListener(delegate { trainDef(); });
        staButton.onClick.AddListener(delegate { trainSta(); });
        trainButton.onClick.AddListener(delegate { runTrain(); });

        fadeVal = GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha;

        monster = GameObject.Find("penguin").GetComponent<monsterStats>();
    }

    void Update()
    {

    }

    void trainStr()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        strButton.Select(); //selects button
        selectedButton = strButton; //sets events selectedButton to this one

        newNum = monster.strength; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        isSelected = true;

    }

    void trainDex()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        dexButton.Select(); //selects button
        selectedButton = dexButton; //sets events selectedButton to this one
        //Debug.Log("selectedButton: " + events.selectedButton);

        newNum = monster.dexterity; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        GameObject.Find("numTxt").GetComponent<Text>().text = numString; //updates numTxt to selected stat value
        isSelected = true;

    }

    void trainInt()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        intButton.Select(); //selects button
        selectedButton = intButton; //sets events selectedButton to this one
        //Debug.Log("selectedButton: " + events.selectedButton);

        newNum = monster.intelligence; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        GameObject.Find("numTxt").GetComponent<Text>().text = numString; //updates numTxt to selected stat value
        isSelected = true;

    }

    void trainMnd()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        mndButton.Select(); //selects button
        selectedButton = mndButton; //sets events selectedButton to this one
        //Debug.Log("selectedButton: " + events.selectedButton);

        newNum = monster.mind; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        GameObject.Find("numTxt").GetComponent<Text>().text = numString; //updates numTxt to selected stat value
        isSelected = true;

    }

    void trainDef()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        defButton.Select(); //selects button
        selectedButton = defButton; //sets events selectedButton to this one
        //Debug.Log("selectedButton: " + events.selectedButton);

        newNum = monster.defense; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        GameObject.Find("numTxt").GetComponent<Text>().text = numString; //updates numTxt to selected stat value
        isSelected = true;

    }

    void trainSta()
    {

        EventSystem.current.SetSelectedGameObject(null); //clears selected
        staButton.Select(); //selects button
        selectedButton = staButton; //sets events selectedButton to this one
        //Debug.Log("selectedButton: " + events.selectedButton);

        newNum = monster.stamina; //sets temp int to monster's selected stat

        numString = newNum.ToString(); //sets temp string to newNum in string format
        GameObject.Find("numTxt").GetComponent<Text>().text = numString; //updates numTxt to selected stat value
        isSelected = true;

    }

    void runTrain()
    {
        if (selectedButton == strButton)
        {
            monster.strength++;
        }
        if (selectedButton == dexButton)
        {
            monster.dexterity++;
        }
        if (selectedButton == intButton)
        {
            monster.intelligence++;
        }
        if (selectedButton == mndButton)
        {
            monster.mind++;
        }
        if (selectedButton == defButton)
        {
            monster.defense++;
        }
        if (selectedButton == staButton)
        {
            monster.stamina++;
        }

        monster.monster.moveEnergy();
        //weekCounter.nextWeek();

        runFade();
        StartCoroutine(testFadeOut());
        
    }

    public IEnumerator testFadeOut()
    {
        
        while (true)
        {
            fadeVal = GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha;

            if (fadeVal < 1.0f)
            {
                GameObject.Find("fadeImage").GetComponent<CanvasGroup>().alpha += .05f;
                yield return new WaitForSeconds(fadeTime);
            } else
            {
                yield return new WaitForSeconds(3.0f);
                StartCoroutine(testFadeIn());
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
                yield return new WaitForSeconds(fadeTime);
            } else
            {
                break;
            }
        }
    }

    void runFade()
    {
        mainCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        mainCanvas.GetComponent<CanvasGroup>().interactable = false;
        monstercanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        monstercanvas.GetComponent<CanvasGroup>().interactable = false;
        trainingCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        trainingCanvas.GetComponent<CanvasGroup>().interactable = false;
        infoCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        infoCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    void runFadeIn()
    {
        mainCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        mainCanvas.GetComponent<CanvasGroup>().interactable = true;
        mainCanvas.SetActive(true);
        monstercanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        monstercanvas.GetComponent<CanvasGroup>().interactable = true;
        trainingCanvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        trainingCanvas.GetComponent<CanvasGroup>().interactable = true;
        infoCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        infoCanvas.GetComponent<CanvasGroup>().interactable = true;
        mon.transform.position = new Vector3(19.22f, 0.1599f, 22.07f);
        mon.transform.rotation = new Quaternion(0.0f, 51.525f, 0.0f, 0.0f);
    }
}
