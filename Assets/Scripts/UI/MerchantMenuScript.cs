using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantMenuScript : MonoBehaviour
{
    public string[] items;
    public SpritesManagerScript sms;
    public MerchantSubMenuScript msms;
    public GameObject[] itemSlotGOs;
    public ItemSlot[] itemSlots;

    private void Start()
    {
        itemSlots = new ItemSlot[itemSlotGOs.Length];
        for (int j = 0; j < itemSlotGOs.Length; j++)
        {
            RegisterItemSlot(j, itemSlotGOs[j]);
        }
        for (int j = 0; j < items.Length; j++)
        {
            FillItemSlot(j, items[j]);
        }
    }

    // Registers craftSlots classes
    void RegisterItemSlot(int index, GameObject go)
    {
        itemSlots[index] = new ItemSlot();
        ItemSlot work = itemSlots[index];
        work.go = go;
        work.image = go.GetComponent<Image>();
        work.name = "";
        work.nameText = go.transform.Find("Name").GetComponent<Text>();
        work.button = go.GetComponent<Button>();
        work.button.onClick.AddListener(delegate { OpenSubMenu(work.name); });
    }

    // Inputs the information of items into craftSlots
    void FillItemSlot(int index, string name)
    {
        ItemSlot work = itemSlots[index];
        work.name = name;
        work.nameText.text = name;
        work.image.sprite = sms.GetImage(name);
    }

    void OpenSubMenu(string name)
    {
        msms.SetUpSubMenu(name);
    }

    public class ItemSlot
    {
        public GameObject go { get; set; }
        public Image image { get; set; }
        public string name { get; set; }
        public Text nameText { get; set; }
        public Button button { get; set; }
    }
}
