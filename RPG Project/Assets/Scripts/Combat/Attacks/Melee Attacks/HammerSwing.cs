using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : BaseAttack
{
    public HammerSwing()
    {
        attackName = "Hammer Swing";
        attackDescription = "powerful hammer swing";
        attackDamage = 20f;
        attackCost = 0f;
        attackType = "Physical";
    }
}
