using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuScrpit : MonoBehaviour {
    public string[] items;
    public SpritesManagerScript sms;
    public CraftingSubMenuScript csms;
    public GameObject[] craftSlotGOs;
    public CraftSlot[] craftSlots;

    private void Start()
    {
        craftSlots = new CraftSlot[craftSlotGOs.Length];
        for (int j = 0; j < craftSlotGOs.Length; j++)
        {
            RegisterCraftSlot(j, craftSlotGOs[j]);
        }
        for (int j = 0; j < items.Length; j++)
        {
            FillCraftSlot(j, items[j]);
        }
    }

    // Registers craftSlots classes
    void RegisterCraftSlot(int index, GameObject go)
    {
        craftSlots[index] = new CraftSlot();
        CraftSlot work = craftSlots[index];
        work.go = go;
        work.image = go.GetComponent<Image>();
        work.name = "";
        work.nameText = go.transform.Find("Name").GetComponent<Text>();
        work.button = go.GetComponent<Button>();
        work.button.onClick.AddListener(delegate { OpenSubMenu(work.name); });
    }

    // Inputs the information of items into craftSlots
    void FillCraftSlot(int index, string name)
    {
        CraftSlot work = craftSlots[index];
        work.name = name;
        work.nameText.text = name;
        work.image.sprite = sms.GetImage(name);
    }

    void OpenSubMenu(string name)
    {
        csms.SetUpSubMenu(name);
    }

    public class CraftSlot
    {
        public GameObject go { get; set; }
        public Image image { get; set; }
        public string name { get; set; }
        public Text nameText { get; set; }
        public Button button { get; set; }
    }

}
