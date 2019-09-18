using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monsterStats : MonoBehaviour
{
    public monster monster;


    public int strength;
    public int intelligence;
    public int mind;
    public int dexterity;
    public int defense;
    public int stamina;

    string type;

    public float hunger;

    // Start is called before the first frame update
    void Start()
    {
        monster = new monster();

        //change the below when i add a thing in later
        monster.type = "beast";
        type = monster.type;
        giveStats();
        strength = monster.strength;
        intelligence = monster.intelligence;
        mind = monster.mind;
        dexterity = monster.dexterity;
        defense = monster.defense;
        stamina = monster.stamina;
                
        GameObject.Find("statusText").GetComponent<Text>().text = monster.energy[monster.energyIndex];
        GameObject.Find("nameTxt").GetComponent<Text>().text = GameObject.Find("monster").transform.GetChild(0).gameObject.name;

    }

    // Update is called once per frame
    void Update()
    {
        hunger = monster.hungerVal;
    }

    void giveStats()
    {
        if (type == "beast") {
            monster.strength = 75;
            monster.intelligence = 100;
            monster.mind = 125;
            monster.dexterity = 75;
            monster.defense = 175;
            monster.stamina = 155;

            monster.strLevFlex = 1.5f; //average strength scaling
            monster.intLevFlex = 1.1f; //below average int scaling
            monster.mndLevFlex = 1.6f;
            monster.dexLevFlex = 1.0f;
            monster.defLevFlex = 2.0f;
            monster.staLevFlex = 1.8f;
        }
    }

    public void updateStats()
    {

    }





}
