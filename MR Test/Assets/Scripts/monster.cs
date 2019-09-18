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

    public int hungerVal = 100;
    public int hungerIndex;
    public string[] hunger = new string[] { "Full", "Satiated", "Indifferent", "Hungry", "Starving", "Famished" };

    public float strLevFlex, dexLevFlex, intLevFlex, mndLevFlex, defLevFlex, staLevFlex;

    //Configure how quickly energy and hunger diminishes each week
    int energyRate = 15;
    int hungerRate = 10;



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
        energyVal = energyVal - energyRate;

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

    public void moveHunger()
    {
        Debug.Log("moving hunger: " + hungerVal);
        hungerVal = hungerVal - hungerRate;

        if (hungerVal <= 0)
        {
            hungerIndex = 5;
        }
        if (hungerVal >= 1 && hungerVal <= 9)
        {
            hungerIndex = 5;
        }
        if (hungerVal >= 10 && hungerVal <= 19)
        {
            hungerIndex = 5;
        }
        if (hungerVal >= 20 && hungerVal <= 29)
        {
            hungerIndex = 4;
        }
        if (hungerVal >= 30 && hungerVal <= 39)
        {
            hungerIndex = 3;
        }
        if (hungerVal >= 40 && hungerVal <= 49)
        {
            hungerIndex = 3;
        }
        if (hungerVal >= 50 && hungerVal <= 59)
        {
            hungerIndex = 2;
        }
        if (hungerVal >= 60 && hungerVal <= 69)
        {
            hungerIndex = 2;
        }
        if (hungerVal >= 70 && hungerVal <= 79)
        {
            hungerIndex = 1;
        }
        if (hungerVal >= 80 && hungerVal <= 89)
        {
            hungerIndex = 1;
        }
        if (hungerVal >= 90 && hungerVal <= 100)
        {
            hungerIndex = 0;
        }

        if (hungerVal < 0)
        {
            hungerVal = 0;
        }

        Debug.Log("done moving hunger: " + hungerVal);
    }


}
