using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack
{
    public Slash()
    {
        attackName = "Slash";
        attackDescription = "standard sword slash";
        attackDamage = 10f;
        attackCost = 0f;
        attackType = "Physical";
    }
}
