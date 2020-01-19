using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass
{
    //all heros and enemies use these values
    public string _Name; //name contains underscore as to not conflict with Unity code using 'name'

    public float baseHP; //max HP
    public float curHP; //current HP

    public float baseMP; //max MP
    public float curMP; //current MP

    //base attack values and current attack values - base is their max value, while current could be modified at any point
    public float baseATK;
    public float curATK;
    public float baseMATK;
    public float curMATK;
    public float baseDEF;
    public float curDEF;
    public float baseMDEF;
    public float curMDEF;

    //enemies need to be implemented for all of these
    public float strength; //for calculating physical attack damage
    public float stamina; //for calculating HP
    public float intelligence; //for calculating magic damage
    public float dexterity; //for calculating ATB gauge speed
    public float agility; //for calculating dodge/crit (not yet implemented)
    public float spirit; //for calculating MP regeneration, magic defense (not yet implemented)

    public List<BaseAttack> attacks = new List<BaseAttack>(); //possible attacks
}
