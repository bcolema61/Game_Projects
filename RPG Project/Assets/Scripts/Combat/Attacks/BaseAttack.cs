using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public string attackName; //Name of Attack
    public string attackDescription; //Used for menu interface - not yet implemented
    public float attackDamage; //Base Damage
    public float attackCost; //cost of attack (mana or otherwise)
    public string attackType; //if magic or basic attack

}
