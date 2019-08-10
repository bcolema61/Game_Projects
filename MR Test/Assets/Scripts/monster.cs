using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster
{
    public string name;
    public int weeksAge = 1;
    public int monthsAge = 1;
    public int yearsAge = 1;

    public int strength;
    public int dexterity;
    public int intelligence;
    public int mind;
    public int defense;
    public int stamina;

    public string type;
    
    public int energyIndex;
    public string[] energy = new string[] { "Energetic", "Lively", "Okay", "Tired", "Exhausted", "Frustrated" };

    public int energyVal = 100;

    public float strLevFlex, dexLevFlex, intLevFlex, mndLevFlex, defLevFlex, staLevFlex;

    // Start is called before the first frame update
    void Start()
    {
        energyIndex = 0;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveEnergy()
    {
        energyVal = energyVal - 15;

        if (energyVal <= 0)
        {
            energyIndex = 5;
        }
        if (energyVal >= 1 && energyVal <= 9) {
            energyIndex = 5;
        }
        if (energyVal >= 10 && energyVal <= 19)
        {
            energyIndex = 5;
        }
        if (energyVal >= 20 && energyVal <= 29)
        {
            energyIndex = 4;
        }
        if (energyVal >= 30 && energyVal <= 39)
        {
            energyIndex = 3;
        }
        if (energyVal >= 40 && energyVal <= 49)
        {
            energyIndex = 3;
        }
        if (energyVal >= 50 && energyVal <= 59)
        {
            energyIndex = 2;
        }
        if (energyVal >= 60 && energyVal <= 69)
        {
            energyIndex = 2;
        }
        if (energyVal >= 70 && energyVal <= 79)
        {
            energyIndex = 1;
        }
        if (energyVal >= 80 && energyVal <= 89)
        {
            energyIndex = 1;
        }
        if (energyVal >= 90 && energyVal <= 100)
        {
            energyIndex = 0;
        }

        if (energyVal < 0)
        {
            energyVal = 0;
        }

        GameObject.Find("statusText").GetComponent<Text>().text = energy[energyIndex];
    }

    public void rest()
    {
        energyVal = energyVal + 50;
        if (energyVal <= 0)
        {
            energyIndex = 5;
        }
        if (energyVal <= 10 && energyVal >= 19)
        {
            energyIndex = 5;
        }
        if (energyVal <= 20 && energyVal >= 29)
        {
            energyIndex = 4;
        }
        if (energyVal <= 30 && energyVal >= 39)
        {
            energyIndex = 3;
        }
        if (energyVal <= 40 && energyVal >= 49)
        {
            energyIndex = 3;
        }
        if (energyVal <= 50 && energyVal >= 59)
        {
            energyIndex = 2;
        }
        if (energyVal <= 60 && energyVal >= 69)
        {
            energyIndex = 2;
        }
        if (energyVal <= 70 && energyVal >= 79)
        {
            energyIndex = 1;
        }
        if (energyVal <= 80 && energyVal >= 89)
        {
            energyIndex = 1;
        }
        if (energyVal <= 90 && energyVal >= 100)
        {
            energyIndex = 0;
        }

        GameObject.Find("statusText").GetComponent<Text>().text = energy[energyIndex];
    }

    int getEnergy()
    {

        return 0;
    }


}
