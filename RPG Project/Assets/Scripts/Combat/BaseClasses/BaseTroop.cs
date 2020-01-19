using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseTroop
{
    public string _Name;
    public float encounterChance;
    public List<GameObject> enemies = new List<GameObject>();
}
