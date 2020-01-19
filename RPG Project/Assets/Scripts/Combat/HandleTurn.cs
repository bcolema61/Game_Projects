using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string Attacker; //name of attacker
    public string Type; //hero or enemy
    public GameObject AttackersGameObject; //GameObject of attacker
    public GameObject AttackersTarget; //Who will be attacked
    
    public BaseAttack chosenAttack; //which attack is performed
}
