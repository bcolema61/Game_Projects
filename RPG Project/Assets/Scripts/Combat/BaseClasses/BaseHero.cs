using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHero : BaseClass
{
    public List<BaseAttack> MagicAttacks = new List<BaseAttack>(); //unit's magic attacks

    public GameObject heroPrefab; //each hero needs its own prefab
    //for calculating HP, ATB gauge fill speed, etc. not really used yet
    
    //modifiers for leveling purposes.  The higher the modifier, the more effect they are at gaining that particular stat
    public float strengthModifier;
    public float staminaModifier;
    public float intelligenceModifier;
    public float dexterityModifier;
    public float agilityModifier;
    public float spiritModifier;

    public int currentLevel = 1;
    [HideInInspector] public int levelBeforeExp;
    public int currentExp;
    [HideInInspector] public int expBeforeAddingExp;

    //public MultiDimensionalInt[] testArray;
    public void LevelUp()
    {
        currentLevel++;
        Debug.Log(_Name + " has leveled up from " + levelBeforeExp + " to " + currentLevel);
        processStatLevelUps();
    }

    public void processStatLevelUps()
    {
        //Debug.Log("Strength: " + strength + ", strengthModifier: " + strengthModifier);
        strength = strength + Mathf.Round(strength * strengthModifier);
        //Debug.Log("New strength: " + strength);

        //Debug.Log("Stamina: " + stamina + ", staminaModifier: " + staminaModifier);
        stamina = stamina + Mathf.Round(stamina * staminaModifier);
        //Debug.Log("New stamina: " + stamina);

        //Debug.Log("Intelligence: " + intelligence + ", intelligenceModifer: " + intelligenceModifier);
        intelligence = intelligence + Mathf.Round(intelligence * intelligenceModifier);
        //Debug.Log("New intelligence: " + intelligence);

        //Debug.Log("Spirit: " + spirit + ", spiritModifier: " + spiritModifier);
        spirit = spirit + Mathf.Round(spirit * spiritModifier);
        //Debug.Log("New spirit: " + spirit);

        //Debug.Log("Dexterity: " + dexterity + ", dexterityModifier: " + dexterityModifier);
        dexterity = dexterity + Mathf.Round(dexterity * dexterityModifier);
        //Debug.Log("New dexterity: " + dexterity);

        //Debug.Log("Agility: " + agility + ", agilityModifier: " + agilityModifier);
        agility = agility + Mathf.Round(agility * agilityModifier);
        //Debug.Log("New agility: " + agility);

        updateBaseStats();

        learnNewAttacks();
    }

    //stats are affected by parameters here when leveling up
    void updateBaseStats()
    {
        baseATK = Mathf.Round(baseATK + (strength * .5f));
        curATK = baseATK;

        baseMATK = Mathf.Round(baseMATK + intelligence * .5f);
        curMATK = baseMATK;

        baseDEF = Mathf.Round(baseDEF + (stamina * .6f));
        curDEF = baseDEF;

        baseMDEF = Mathf.Round(baseDEF + (stamina * .5f));
        curMDEF = baseMDEF;

        baseHP = Mathf.Round(baseHP + (stamina * .75f));
        baseMP = Mathf.Round(baseMP + (intelligence * .5f));

        curHP = baseHP; //if full heal should occur on levelup, using for debugging purposes for now
        curMP = baseMP; //if MP should be restored on levelup, using for debugging purposes for now
    }

    void learnNewAttacks()
    {
        if (_Name == "Test dude 1")
        {
            if (currentLevel == 4)
            {
                //Debug.Log("Learned the thing!");
            }
        }

        if (_Name == "Test dude 2")
        {
            if (currentLevel == 2)
            {
                //Debug.Log("Learned the other thing!");
            }
        }
    }
}
