using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().EnemySelection(EnemyPrefab); //Save input of enemy selection to enemy prefab
    }

    public void HideSelector() //hides selector cursor over enemy
    {
            EnemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }

    public void ShowSelector() //shows selector cursor over enemy
    {
            EnemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
    }
}
