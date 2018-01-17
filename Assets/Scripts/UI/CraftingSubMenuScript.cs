using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSubMenuScript : MonoBehaviour {
    public GameObject[] requirementGOs;
    public SpritesManagerScript sms;
    public InventoryScript inventoryScript;
    public PrefabsManagerScript pms;
    public Text itemName;
    public Button craftButton;
    RequirementSlot[] reqSlots;

    public Speech speech;

    // Returns string of requirements given name of item.
    // Input: name of craft item
    // Output: array of requirements
    Requirement[] GetRequirements(string name)
    {
        Requirement[] answer = new Requirement[4];
        // Regular Crafting Menu
        if (name.Equals("Pickaxe"))
        {
            answer[0] = new Requirement("Log", 5);
            answer[1] = new Requirement("Rock", 2);
            answer[2] = new Requirement("", 0);
            answer[3] = new Requirement("", 0);
        }
        else if (name.Equals("Axe"))
        {
            answer[0] = new Requirement("Log", 5);
            answer[1] = new Requirement("Rock", 2);
            answer[2] = new Requirement("", 0);
            answer[3] = new Requirement("", 0);
        }
        else if (name.Equals("Mallet"))
        {
            answer[0] = new Requirement("Log", 7);
            answer[1] = new Requirement("", 0);
            answer[2] = new Requirement("", 0);
            answer[3] = new Requirement("", 0);
        }

        // Ornn
        else if (name.Equals("Sword"))
        {
            answer[0] = new Requirement("Log", 5);
            answer[1] = new Requirement("Rock", 2);
            answer[2] = new Requirement("Diamond", 1);
            answer[3] = new Requirement("Mallet", 2);
        }
        else if (name.Equals("Wand"))
        {
            answer[0] = new Requirement("Demon Head", 1);
            answer[1] = new Requirement("Diamond", 2);
            answer[2] = new Requirement("Mallet", 2);
            answer[3] = new Requirement("Rock", 20);
        }
        return answer;
    }

    private void Start()
    {
        craftButton.onClick.AddListener(delegate { CheckTransaction(itemName.text); });
        reqSlots = new RequirementSlot[requirementGOs.Length];
        for (int j = 0; j < requirementGOs.Length; j++)
        {
            RegisterReqSlot(j, requirementGOs[j]);
        }
    }

    void CheckTransaction(string name)
    {
        Requirement[] reqs = GetRequirements(name);
        bool allClear = true;
        string errorMessage = "";
        for (int j = 0; j < reqs.Length; j++)
        {
            string reqName = reqs[j].name;
            int reqCount = reqs[j].count;
            if (reqName != "")
            {
                Slot myItem = inventoryScript.GetItem(reqName);
                if (myItem == null)
                {
                    errorMessage += "Mr.Box don't have any " + reqName + "s!!\n";
                    allClear = false;
                }
                else if (myItem.count < reqCount)
                {
                    int lack = reqCount - myItem.count;
                    errorMessage += "Mr. Box need " + lack + " more " + myItem.name;
                    if (lack > 1)
                    {
                        errorMessage += "s!!\n";
                    }
                    else
                    {
                        errorMessage += "!!\n";
                    }
                    allClear = false;
                }
            }
        }
        if (allClear)
        {
            for (int j = 0; j < reqs.Length; j++)
            {
                string reqName = reqs[j].name;
                int reqCount = reqs[j].count;
                if (reqName != "")
                {
                    inventoryScript.SubtractItem(reqName, reqCount);
                }
            }
            inventoryScript.AddItem(itemName.text);
            char firstChar = itemName.text.ToCharArray()[0];
            if (firstChar.Equals('a') || firstChar.Equals('e') || firstChar.Equals('i') || firstChar.Equals('o') || firstChar.Equals('u') )
            {
                speech.Say("Wow I just crafted an " + itemName.text );
            }
            else
            {
                speech.Say("Wow I just crafted a " + itemName.text);
            }
        }
        if (errorMessage != "")
        {
            FindObjectOfType<Speech>().Say(errorMessage);
        }
    }

    void RegisterReqSlot(int index, GameObject go)
    {
        reqSlots[index] = new RequirementSlot();
        RequirementSlot work = reqSlots[index];
        work.go = go;
        work.image = go.GetComponent<Image>();
        work.name = "";
        work.nameText = go.transform.Find("Name").GetComponent<Text>();
        work.countText = go.transform.Find("Count").GetComponent<Text>();
    }

    void FillReqSlot(int index, string name, int count)
    {
        reqSlots[index].image.sprite = sms.GetImage(name);
        reqSlots[index].name = name;
        reqSlots[index].nameText.text = name;
        reqSlots[index].countText.text = "" + count;
    }

    public void SetUpSubMenu(string name)
    {
        gameObject.SetActive(true);
        itemName.text = name;
        Requirement[] reqs = GetRequirements(name);
        for (int j = 0; j < reqs.Length; j++)
        {
            FillReqSlot(j, reqs[j].name, reqs[j].count);
        }
    }

    public class RequirementSlot
    {
        public GameObject go { get; set; }
        public Image image { get; set; }
        public string name { get; set; }
        public Text nameText { get; set; }
        public Text countText { get; set; }
    }
}

public class Requirement
{
    public string name;
    public int count;

    public Requirement(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
}