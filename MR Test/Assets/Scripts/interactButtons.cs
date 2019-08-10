using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class interactButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text statName, statVal;

    bool buttonSelected;

    Button selectedButton;

    training training;

    void Start()
    {
        training = GameObject.Find("Training Canvas").GetComponent<training>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
        //Use this to tell when the user right-clicks on the Button
        /*if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
            Debug.Log(name + " Game Object Right Clicked!");
        }*/

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (name == "strBut")
            {
                statName.text = "Strength";
                statVal.text = training.monster.strength.ToString();
            }
            if (name == "dexBut")
            {
                statName.text = "Dexterity";
                statVal.text = training.monster.dexterity.ToString();
            }
            if (name == "intBut")
            {
                statName.text = "Intelligence";
                statVal.text = training.monster.intelligence.ToString();
            }
            if (name == "mndBut")
            {
                statName.text = "Mind";
                statVal.text = training.monster.mind.ToString();
            }
            if (name == "defBut")
            {
                statName.text = "Defense";
                statVal.text = training.monster.defense.ToString();
            }
            if (name == "staBut")
            {
                statName.text = "Stamina";
                statVal.text = training.monster.stamina.ToString();
            }
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        //Debug.Log("Cursor Entering " + name + " GameObject");
        //Debug.Log("onpointerenter: " + name);

        

        buttonSelected = GameObject.Find("Training Canvas").GetComponent<training>().isSelected;
        selectedButton = GameObject.Find("Training Canvas").GetComponent<training>().selectedButton;

        if (buttonSelected == false)
        {
            if (name == "strBut")
            {
                statName.text = "Strength";
                statVal.text = training.monster.strength.ToString();
            }
            if (name == "dexBut")
            {
                statName.text = "Dexterity";
                statVal.text = training.monster.dexterity.ToString();
            }
            if (name == "intBut")
            {
                statName.text = "Intelligence";
                statVal.text = training.monster.intelligence.ToString();
            }
            if (name == "mndBut")
            {
                statName.text = "Mind";
                statVal.text = training.monster.mind.ToString();
            }
            if (name == "defBut")
            {
                statName.text = "Defense";
                statVal.text = training.monster.defense.ToString();
            }
            if (name == "staBut")
            {
                statName.text = "Stamina";
                statVal.text = training.monster.stamina.ToString();
            }
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        //Debug.Log("Cursor Exiting " + name + " GameObject");
    }

    void strength()
    {

    }
    void dexterity()
    {

    }
    void mind()
    {

    }
    void intelligence()
    {

    }
    void defense()
    {

    }
    void stamina()
    {

    }


}