using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    [MenuItem("Tools/Display Dialogue Canvas")]
    public static void DisplayDialogueCanvas()
    {
        GameObject.Find("DialogueCanvas").GetComponent<CanvasGroup>().alpha = 1;
    }

    [MenuItem("Tools/Hide Dialogue Canvas")]
    public static void HideDialogueCanvas()
    {
        GameObject.Find("DialogueCanvas").GetComponent<CanvasGroup>().alpha = 0;
    }

    [MenuItem("Tools/List Troops")]
    public static void ListTroops()
    {
        List<BaseTroop> troops = GameObject.Find("GameManager").GetComponent<GameManager>().troops;
        Debug.Log("-----GameManager Troops-----");
        for (int i = 0; i < troops.Count; i++)
        {
            Debug.Log(i + ") " + troops[i]._Name);
        }
        Debug.Log("-----End GameManager Troops-----");
    }
}
