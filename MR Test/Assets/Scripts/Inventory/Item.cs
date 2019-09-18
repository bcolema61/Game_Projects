using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    private string theItem;

    public enum itemType
    {
        food,
        equipment,
        misc
    }

    public itemType type;

    public float fillHunger;

    monster monster;


    void Start()
    {

    }

    public virtual void Use ()
    {
        Debug.Log("using " + name);
        theItem = name.ToString();


        doAction(theItem);
    }

    void doAction(string item)
    {
        Debug.Log("doAction: " + item);
        if (type.ToString() == "food")
        {
            runFood(item);
        } else if (type.ToString() == "equipment")
        {
            Debug.Log("can't use equipment yet");
        }
        
    }



    void runFood(string item)
    {
        monster = GameObject.Find("monster").GetComponent<monsterStats>().monster;

        //check to make sure monster is being interacted with.. if so:

        Debug.Log("monster's hunger before: " + monster.hungerVal);

        monster.hungerVal = monster.hungerVal + Mathf.RoundToInt(fillHunger);

        Debug.Log("monster's hunger after: " + monster.hungerVal);

        if (monster.hungerVal > 100)
        {
            monster.hungerVal = 100;
        }

        Debug.Log("monster's hunger after (corrected): " + monster.hungerVal);

        Inventory.instance.Remove(this);
    }


}
