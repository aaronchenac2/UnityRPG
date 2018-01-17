using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {
    public GameObject[] slotGOs;
    public Slot[] slots;
    public SpritesManagerScript sms;
    public SubInventoryScript sis;
    public PrefabsManagerScript pms;

    private void Start()
    {
        slots = new Slot[slotGOs.Length];
        for (int j = 0; j < slotGOs.Length; j++)
        {
            RegisterSlot(j, slotGOs[j]);
        }
        for (int j = 0; j < 4; j++)
        {
            AddItem(pms.GetPrefab("Rock"));
        }
        if (Hacks.hacks)
        {
            for (int j = 0; j < 99; j++)
            {
                AddItem(pms.GetPrefab("Log"));
                AddItem(pms.GetPrefab("Diamond"));
                AddItem(pms.GetPrefab("Mallet"));
                AddItem("Coin");
            }
        }
    }

    void RegisterSlot(int index, GameObject go)
    {
        slots[index] = new Slot();
        Slot work = slots[index];
        work.go = go;
        work.image = go.GetComponent<Image>();
        work.button = go.GetComponent<Button>();
        work.button.onClick.AddListener(delegate { OpenSubMenu(work.name); });
        work.name = "";
        work.countText = go.transform.Find("Count").GetComponent<Text>();
        work.count = 0;
        work.occupied = false;
    }

    public void AddItem(GameObject go)
    {
        Debug.Log("Adding " + go.name + " to inventory...");
        // Search for item in slots
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(true);

        // If found item, add 1 to count and returns
        for (int j = 0; j < slots.Length; j++)
        {
            if (go.name.Equals(slots[j].name))
            {
                slots[j].count++;
                slots[j].countText.text = "" + slots[j].count;
                gameObject.SetActive(currentState);
                return;
            }
        }
        Debug.Log(go.name + " is not found in the current inventory... Adding to history...");

        // At this point, item is not in inventory
        for (int j = 0; j < slots.Length; j++)
        {
            // Finds the first unoccupied slot
            if (!slots[j].occupied)
            {
                Slot work = slots[j];
                work.image.sprite = sms.GetImage(go.name);
                work.name = go.name;
                work.count = 1;
                work.countText.text = "1";
                work.occupied = true;
                gameObject.SetActive(currentState);
                Debug.Log("Slot: " + j + " has been allocated for " + go.name);
                return;
            }
        }

        Debug.Log("ERROR: INVENTORY SCRIPT ADD ITEM");
    }

    public void AddItem(string s)
    {
        GameObject go = pms.GetPrefab(s);
        Debug.Log("Adding " + go.name + " to inventory...");
        // Search for item in slots
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(true);

        // If found item, add 1 to count and returns
        for (int j = 0; j < slots.Length; j++)
        {
            if (go.name.Equals(slots[j].name))
            {
                slots[j].count++;
                slots[j].countText.text = "" + slots[j].count;
                gameObject.SetActive(currentState);
                return;
            }
        }
        Debug.Log(go.name + " is not found in the current inventory... Adding to history...");

        // At this point, item is not in inventory
        for (int j = 0; j < slots.Length; j++)
        {
            // Finds the first unoccupied slot
            if (!slots[j].occupied)
            {
                Slot work = slots[j];
                work.image.sprite = sms.GetImage(go.name);
                work.name = go.name;
                work.count = 1;
                work.countText.text = "1";
                work.occupied = true;
                gameObject.SetActive(currentState);
                Debug.Log("Slot: " + j + " has been allocated for " + go.name);
                return;
            }
        }
        Debug.Log("ERROR: INVENTORY SCRIPT ADD ITEM");
    }

    public void SubtractItem(string name, int num)
    {
        GameObject go = pms.GetPrefab(name);

        // Search for item in slots
        bool currentState = gameObject.activeSelf;
        gameObject.SetActive(true);

        Debug.Log(name + " " + num);

        // If found item, subtracts num to count and returns
        for (int j = 0; j < slots.Length; j++)
        {
            Debug.Log(go);
            Debug.Log(slots[j]);
            if (go.name.Equals(slots[j].name))
            {
                slots[j].count -= num;
                slots[j].countText.text = "" + slots[j].count;
                gameObject.SetActive(currentState);

                if (slots[j].count == 0)
                {
                    slots[j].image.sprite = null;
                    slots[j].name = "";
                    slots[j].occupied = false;
                }
                return;
            }
        }
        Debug.Log("ERROR: INVENTORY SCRIPT SUBTRACT ITEM");
    }

    // Returns slot with item name
    public Slot GetItem(string s)
    {
        for (int j = 0; j < slots.Length; j++)
        {
            if (slots[j].name.Equals(s))
            {
                return slots[j];
            }
        }
        return null;
    }

    void OpenSubMenu(string name)
    {
        sis.DisplayInfo(name);
    }
}

public class Slot
{
    public GameObject go { get; set; }
    public Image image { get; set; }
    public Button button { get; set; }
    public string name { get; set; }
    public int count { get; set; }
    public Text countText { get; set; }
    public bool occupied { get; set; }
}