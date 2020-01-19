using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bio1Spell : BaseAttack
{
    public Bio1Spell()
    {
        attackName = "Bio 1";
        attackDescription = "Basic Poison attack";
        attackDamage = 5f;
        attackCost = 5f;
        attackType = "Magic";
    }
}
