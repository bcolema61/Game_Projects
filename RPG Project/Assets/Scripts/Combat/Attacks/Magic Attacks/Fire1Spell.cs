using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire1Spell : BaseAttack
{
    public Fire1Spell()
    {
        attackName = "Fire 1";
        attackDescription = "Basic fireball which burns a lil bit";
        attackDamage = 20f;
        attackCost = 10f;
        attackType = "Magic";
    }
}
