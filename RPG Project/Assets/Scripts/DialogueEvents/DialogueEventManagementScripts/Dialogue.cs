using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public List<BaseDialogueEvent> newEventOrDialogue = new List<BaseDialogueEvent>();

    GameObject playerGO;
    float interactableDistance = 0.4f;

    float textSpeed = 0.030225f;

    Text messageText;
    bool messageFinished = false;
    bool dialogueStarted = false;
    bool playerLocked = false;

    bool buttonPressed = false;

    object[] attachedScripts;
    BaseScriptedEvent theScript;
    List<BaseScriptedEvent> dialogueEvents = new List<BaseScriptedEvent>();
    [System.NonSerialized] public int currentDialogue;

    public bool startAutomatically;
    bool runOnce = true;

    Collider2D thisCollider;
    Collider2D playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find("Player");
        messageText = GameManager.instance.DialogueCanvas.GetComponentInChildren<Text>();

        attachedScripts = FindObjectsOfType(typeof(BaseScriptedEvent));
        System.Array.Reverse(attachedScripts);

        for (int i = 0; i < attachedScripts.Length; i++)
        {
            theScript = attachedScripts[i] as BaseScriptedEvent;
            if (this.gameObject.name == theScript.name)
            {
                dialogueEvents.Add(theScript);
            }
        }
        thisCollider = this.gameObject.GetComponent<Collider2D>();
        playerCollider = playerGO.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Confirm") && dialogueStarted == false && buttonPressed == false)
        {
            if ((this.transform.position.x - playerGO.transform.position.x <= interactableDistance && this.transform.position.x - playerGO.transform.position.x >= -interactableDistance) &&
                (this.transform.position.y - playerGO.transform.position.y <= interactableDistance && this.transform.position.y - playerGO.transform.position.y >= -interactableDistance))
            {
                //Debug.Log(this.transform.position - playerGO.transform.position);
                
                CheckConfirmButtonStatus();
                StartCoroutine(ShowMessage());
            }
        }

        if (newEventOrDialogue[currentDialogue].triggerAction == BaseDialogueEvent.triggerActions.ONTOUCH && runOnce && thisCollider.IsTouching(playerCollider))
        {
            runOnce = false;
            StartCoroutine(ShowMessage());
        }

        if (newEventOrDialogue[currentDialogue].triggerAction == BaseDialogueEvent.triggerActions.AUTOSTART)
        {
            startAutomatically = true;
        } else
        {
            startAutomatically = false;
        } 

        if (startAutomatically && runOnce)
        {
            runOnce = false;
            startAutomatically = false;
            StartCoroutine(ShowMessage());
        }

        if (newEventOrDialogue[currentDialogue].triggerAction == BaseDialogueEvent.triggerActions.PARALLEL)
        {
            StartCoroutine(ShowMessage());
        }

        if (playerLocked)
        {
            playerGO.GetComponent<PlayerMovement>().enabled = false;
        } else
        {
            playerGO.GetComponent<PlayerMovement>().enabled = true;
            dialogueStarted = false;
        }

        CheckConfirmButtonStatus();

    }

    IEnumerator ShowMessage()
    {

        dialogueStarted = true;
        playerLocked = true;
        
        for (int d = 0; d < newEventOrDialogue.Count; d++)//each separate dialog on NPC
        {
            BaseDialogueEvent dialogue = newEventOrDialogue[d];
            if (dialogue.dialogText.Length > 0)
            {
                GameManager.instance.DisplayPanel(true);
            } else
            {
                GameManager.instance.DisplayPanel(false);
            }

            bool processMessage = true;
            
            if (dialogue.processOptions == BaseDialogueEvent.processIfTrueOptions.ANY)
            {
                if (dialogue.processIfTrue.Count > 0)
                {
                    for (int i = 0; i < dialogue.processIfTrue.Count; i++)
                    {
                        int whichBool = dialogue.processIfTrue[i];
                        //Debug.Log("Check do process: " + i + " - " + whichBool);
                        if (GameManager.instance.globalBools[whichBool])
                        {
                            //Debug.Log("Found do process: " + whichBool
                            processMessage = true;
                        }
                    }
                }
            }
            if (dialogue.processOptions == BaseDialogueEvent.processIfTrueOptions.ALL)
            {
                if (dialogue.processIfTrue.Count > 0)
                {
                    for (int i = 0; i < dialogue.processIfTrue.Count; i++)
                    {
                        int whichBool = dialogue.processIfTrue[i];
                        //Debug.Log("Check do process: " + i + " - " + whichBool);
                        if (GameManager.instance.globalBools[whichBool])
                        {
                            processMessage = true;
                            continue;
                        } else
                        {
                            processMessage = false;
                            break;
                        }
                    }
                }
            }
            if (dialogue.dontProcessOptions == BaseDialogueEvent.dontProcessIfTrueOptions.ANY)
            {
                if (dialogue.dontProcessIfTrue.Count > 0)
                {

                    for (int i = 0; i < dialogue.dontProcessIfTrue.Count; i++)
                    {
                        int whichBool = dialogue.dontProcessIfTrue[i];
                        //Debug.Log("Check dont process: " + i + " - " + whichBool);
                        if (GameManager.instance.globalBools[whichBool])
                        {
                            //Debug.Log("Found dont process: " + whichBool);
                            processMessage = false;
                        }
                    }
                }
            }
            if (dialogue.dontProcessOptions == BaseDialogueEvent.dontProcessIfTrueOptions.ALL)
            {
                if (dialogue.dontProcessIfTrue.Count > 0)
                {

                    for (int i = 0; i < dialogue.dontProcessIfTrue.Count; i++)
                    {
                        int whichBool = dialogue.dontProcessIfTrue[i];
                        //Debug.Log("Check dont process: " + i + " - " + whichBool);
                        if (GameManager.instance.globalBools[whichBool])
                        {
                            //Debug.Log("Found dont process: " + whichBool);
                            processMessage = false;
                            continue;
                        } else
                        {
                            processMessage = true;
                            break;
                        }
                    }
                }
            }

            /*if (dialogue.processIfTrue.Count > 0)
            {
                for (int i = 0; i < dialogue.processIfTrue.Count; i++)
                {
                    int whichBool = dialogue.processIfTrue[i];
                    //Debug.Log("Check do process: " + i + " - " + whichBool);
                    if (GameManager.instance.globalBools[whichBool])
                    {
                        //Debug.Log("Found do process: " + whichBool
                        processMessage = true;
                    }
                }
            }
            if (dialogue.dontProcessIfTrue.Count > 0)
            {

                for (int i = 0; i < dialogue.dontProcessIfTrue.Count; i++)
                {
                    int whichBool = dialogue.dontProcessIfTrue[i];
                    //Debug.Log("Check dont process: " + i + " - " + whichBool);
                    if (GameManager.instance.globalBools[whichBool])
                    {
                        //Debug.Log("Found dont process: " + whichBool);
                        processMessage = false;
                    }
                }
            }*/


            //Debug.Log("processMessage: " + processMessage);
            if (processMessage)
            {
                //Debug.Log(d);
                if (dialogue.eventsBefore.Count > 0)
                {
                    foreach (BaseEvent runEvent in dialogue.eventsBefore)
                    {
                        if (runEvent.method.Length > 0)
                        {
                            dialogueEvents[runEvent.index].Invoke(runEvent.method, runEvent.waitTime);
                        }
                    }
                }
                for (int j = 0; j < dialogue.dialogText.Length; j++) //actual dialog texts
                {
                    messageFinished = false;
                    string text = dialogue.dialogText[j];
                    string fullText = "";
                    for (int i = 0; i < text.Length; i++)
                    {
                        CheckConfirmButtonStatus();
                        fullText += text[i];
                        messageText.text = fullText;
                        yield return new WaitForSecondsRealtime(textSpeed);
                    }

                    if (fullText == text)
                    {
                        messageFinished = true;
                    }

                    if (messageFinished)
                    {
                        yield return new WaitUntil(() => Input.GetButtonDown("Confirm"));
                    }
                }
                if (currentDialogue < (newEventOrDialogue.Count-1))
                {
                    currentDialogue = d+1;
                }
            }
            else
            {
                //Debug.Log("Skipping - " + d);
                continue;
            }

            if (dialogue.markAsTrue.Count > 0)
            {
                foreach (int boolToChange in dialogue.markAsTrue)
                {
                    Debug.Log("Dialogue - Marking global bool " + boolToChange + " as true");
                    GameManager.instance.globalBools[boolToChange] = true;
                }
            }

            if (dialogue.markAsFalse.Count > 0)
            {
                foreach (int boolToChange in dialogue.markAsFalse)
                {
                    Debug.Log("Dialogue - Marking global bool " + boolToChange + " as false");
                    GameManager.instance.globalBools[boolToChange] = false;
                }
            }

            if (messageFinished)
            {
                if (dialogue.eventsAfter.Count > 0)
                {
                    foreach (BaseEvent runEvent in dialogue.eventsAfter)
                    {
                        if (runEvent.method.Length > 0)
                        {
                            dialogueEvents[runEvent.index].Invoke(runEvent.method, runEvent.waitTime);
                        }
                    }
                }
                //Debug.Log("messageFinished: " + d);
                break;
            }
        }
        //Debug.Log("End of messages");
        runOnce = true;
        playerLocked = false;
        messageText.text = "";
        GameManager.instance.DisplayPanel(false);
    }
    

    void CheckConfirmButtonStatus()
    {
        if (Input.GetButtonDown("Confirm"))
        {
            //Debug.Log("buttonPressed");
            buttonPressed = true;
        }
        if (buttonPressed)
        {
            if (Input.GetButtonUp("Confirm"))
            {
                //Debug.Log("button released");
                buttonPressed = false;
            }
        }
    }

}
