using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseScriptedEvent : MonoBehaviour
{
    public string method;

    [System.NonSerialized] public float baseMoveSpeed = 0.5f;

    [System.NonSerialized] public Transform thisTransform;
    [System.NonSerialized] public GameObject thisGameObject;
    [System.NonSerialized] public Transform playerTransform;
    [System.NonSerialized] public GameObject playerGameObject;
    [System.NonSerialized] public GameManager gameManager;

    private void Start()
    {
        thisGameObject = this.gameObject;
        thisTransform = this.gameObject.transform;
        playerGameObject = GameObject.Find("Player");
        playerTransform = playerGameObject.transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //----MOVEMENT----
    public IEnumerator MoveLeft(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x - spacesToMove, currentPos.y);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveRight(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x + spacesToMove, currentPos.y);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveUp(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x, currentPos.y + spacesToMove);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveDown(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
            Vector2 position = new Vector2(currentPos.x, currentPos.y - spacesToMove);
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, position, t);
                yield return null;
            }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveLeftUp(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x - spacesToMove, currentPos.y + spacesToMove);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveRightUp(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x + spacesToMove, currentPos.y + spacesToMove);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveLeftDown(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x - spacesToMove, currentPos.y - spacesToMove);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveRightDown(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        Vector2 position = new Vector2(currentPos.x + spacesToMove, currentPos.y - spacesToMove);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveToTarget(Transform transform, Vector2 target, float timeToMove)
    {
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, target, t);
            yield return null;
        }
        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    public IEnumerator MoveRandom(Transform transform, float timeToMove, int spacesToMove)
    {
        if (timeToMove == baseMoveSpeed)
        {
            timeToMove = getBaseMoveSpeed(spacesToMove);
        }
        if (transform == playerTransform)
        {
            DisablePlayerMovement();
        }
        Vector2 currentPos = transform.position;
        int randomDirection = Random.Range(0, 4);
        Vector2 position = new Vector2();
        if (randomDirection == 0)
        {
            position = new Vector2(currentPos.x - spacesToMove, currentPos.y); //left
        }
        if (randomDirection == 1)
        {
            position = new Vector2(currentPos.x + spacesToMove, currentPos.y); //right
        }
        if (randomDirection == 2)
        {
            
            position = new Vector2(currentPos.x, currentPos.y - spacesToMove); //down
        }
        if (randomDirection == 3)
        {
            position = new Vector2(currentPos.x, currentPos.y + spacesToMove); //up
        }

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        if (transform == playerTransform)
        {
            EnablePlayerMovement();
        }
    }

    //ienumerator MoveTowardPlayer

    //ienumerator MoveAwayFromPlayer

    //ienumerator StepForward

    //ienumerator StepBackward

    //ienumerator TurnDown

    //ienumerator TurnLeft

    //ienumerator TurnRight

    //ienumerator TurnUp

    //ienumerator Turn90Right

    //ienumerator Turn90Left

    //ienumerator Turn180

    //ienumerator Turn90RightOrLeft

    //ienumerator TurnRandom

    //ienumerator TurnTowardPlayer

    //ienumerator TurnAwayFromPlayer

    //void ChangeDefaultMoveSpeed

    //void EnableWalkingAnimation

    //void DisableWalkingAnimation

    //void EnableForceDirection

    //void DisableForceDirection

    public void DisablePlayerMovement()
    {
        playerGameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    public void EnablePlayerMovement()
    {
        playerGameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public void SavePosition(GameObject objectToSave)
    {
        BasePositionSave thePosition = new BasePositionSave();
        thePosition._Name = objectToSave.name;
        thePosition.newPosition = gameObject.transform.position;
        thePosition.newDirection = "Test";
        thePosition.Scene = SceneManager.GetActiveScene().name;
        GameManager.instance.positionSaves.Add(thePosition);
        Debug.Log("Position saved: " + thePosition._Name);
    }


    //----------------


    //----BATTLE MANAGEMENT----

    public void CallBattle(int troopIndex, string scene)
    {
        GameManager.instance.getBattleFromScript(troopIndex, scene);
    }

    //void ChangeEncounterFrequency

    //----------------


    //----SCENE MANAGEMENT----

    //void TransitionToScene

    //void ShowShop

    //void OpenMenu

    //void OpenSave

    //void GameOver

    //void ReturnToTitle

    //----------------


    //----GAME MANAGEMENT----
    
    public void ChangeSwitch(int whichEvent, int whichSwitch, bool whichBool)
    {
        BaseDialogueEvent e = thisGameObject.GetComponent<Dialogue>().newEventOrDialogue[whichEvent];

        if (whichSwitch == 1)
        {
            e.switch1 = whichBool;
        } else if (whichSwitch == 2)
        {
            e.switch2 = whichBool;
        }
    }

    public bool GetSwitchBool(int whichEvent, int whichSwitch)
    {
        BaseDialogueEvent e = this.gameObject.GetComponent<Dialogue>().newEventOrDialogue[whichEvent];

        if (whichSwitch == 1)
        {
            return e.switch1;
        } else if (whichSwitch == 2)
        {
            return e.switch2;
        } else
        {
            Debug.Log("GetSwitchBool - invalid switch: " + whichSwitch);
            return false;
        }
    }

    public void ChangeGlobalBool(int index, bool boolean)
    {
        GameManager.instance.globalBools[index] = boolean;
    }

    //----------------


    //----MUSIC/SOUNDS----

    //void PlaySE

    //void PlayBGM

    //ienumerator FadeOutBGM

    //ienumerator FadeInBGM

    //void PlayBGS

    //ienumerator FadeOutBGS

    //void StopBGM

    //void StopSE

    //----------------


    //----TIMING----

    public IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    //----------------


    //----DIALOGUE----

    //void ShowMessage

    //void ShowChoices

    //void InputNumber

    //----------------


    //----SYSTEM SETTINGS----

    //void ChangeBattleBGM

    //void ChangeSaveAccess

    //void ChangeMenuAccess

    //----------------


    //----SPRITES----

    //void ChangeGraphic

    //void ChangeOpacity

    //void AddSprite

    //void RemoveSprite

    //----------------


    //----ACTORS----

    //void ChangeHP

    //void ChangeMP

    //void ChangeEXP

    //void ChangeLevel

    //void ChangeParameter

    //void AddSkill

    //void RemoveSkill

    //void ChangeEquipment

    //void ChangeName

    //void InputName

    //----------------


    //----PARTY----

    //void ChangeGold

    //void AddItem

    //void RemoveItem

    //void AddWeapon

    //void RemoveWeapon

    //void AddArmor

    //void RemoveArmor

    //void ChangePartyMember

    //----------------


    //----IMAGES----

    //void ShowPicture

    //void MovePicture

    //void RotatePicture

    //void TintPicture

    //void RemovePicture

    //----------------


    //----SCREEN EFFECTS/WEATHER----

    //void FadeInScreen

    //void FadeOutScreen

    //void TintScreen

    //void FlashScreen

    //void ShakeScreen

    //----------------

    //----FOR EVENTS----
    float getBaseMoveSpeed(int spaces)
    {
        float tempMoveSpeed = baseMoveSpeed * spaces;
        return tempMoveSpeed;
    }
}


