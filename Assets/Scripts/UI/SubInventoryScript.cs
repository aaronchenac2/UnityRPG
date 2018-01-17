using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubInventoryScript : MonoBehaviour {
    public Text itemName;
    public Text itemDescription;
    public PrefabsManagerScript pms;
    public ToolsManagerScript tms;
    public InventoryScript inventoryScript;
    public Button use;
    public Stats stats;
    GameObject go;

    string GetDescription(string name)
    {
        if (name.Equals("Log"))
        {
            return "I want to smack someone with this...";
        }
        else if (name.Equals("Rock"))
        {
            return "That looks very hard...";
        }
        else if (name.Equals("Pickaxe"))
        {
            return "Maybe I can smash something with this...";
        }
        else if (name.Equals("Sword"))
        {
            return "I need a sword master to teach me how to use this...";
        }
        else if (name.Equals("Coin"))
        {
            return "My precious :D.......................";
        }
        else if (name.Equals("Compass"))
        {
            return "Wow I feel like an adventurer already!!";
        }
        else if (name.Equals("Health Potion"))
        {
            return "Looks like someone's blood...";
        }
        return "";
    }

    void UseItem()
    {
        if (go.CompareTag("Tool"))
        {
            if (!go.name.Equals("Sword"))
            {
                tms.ChangeTool(go.name);
            }
            else
            {
                if(!transform.parent.transform.parent.Find("Locks").Find("SwordLock").gameObject.activeSelf)
                {
                    tms.ChangeTool(go.name);
                }
            }
        }
        else if (go.name.Equals("Compass"))
        {
            transform.parent.Find("Compass").gameObject.SetActive(true);
        }
        else if (go.name.Equals("Health Potion"))
        {
            stats.HP += 10;
        }
        else
        {
            return;
        }
        inventoryScript.SubtractItem(go.name, 1);
    }

    private void Start()
    {
        use.onClick.AddListener(UseItem);
    }

    public void DisplayInfo(string name)
    {
        gameObject.SetActive(true);
        go = pms.GetPrefab(name);
        itemName.text = "Item Name: " + name;
        itemDescription.text = "Item Description: " + GetDescription(name);
        if (name.Equals(""))
        {
            itemName.text = "Nothing here :)";
            itemDescription.text = "Nothing here too :)";
        }
    }
}
