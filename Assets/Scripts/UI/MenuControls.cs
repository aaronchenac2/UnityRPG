using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public GameObject inventory;
    public GameObject ornnCraftingMenu;
    public GameObject doranMerchantMenu;
    public GameObject craftingMenu;
    public GameObject subInv;
    public GameObject subCraft;
    public GameObject subMerch;

    bool inventoryOpen;
    bool craftingMenuOpen;

    private void Start()
    {
        inventoryOpen = false;
        craftingMenuOpen = false;

        subInv.SetActive(false);
        subCraft.SetActive(false);
        subMerch.SetActive(false);
        inventory.SetActive(false);
        ornnCraftingMenu.SetActive(false);
        doranMerchantMenu.SetActive(false);
        craftingMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            inventory.SetActive(false);
            craftingMenu.SetActive(false);
            subInv.SetActive(false);
            subCraft.SetActive(false);
        }
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryOpen = !inventoryOpen;
            inventory.SetActive(inventoryOpen);
        }
        if (Input.GetButtonDown("CraftingMenu"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
            for (int j = 0; j < colliders.Length; j++)
            {
                if (colliders[j].CompareTag("Blacksmith"))
                {
                    ornnCraftingMenu.SetActive(true);
                    return;
                }
            }
            craftingMenuOpen = !craftingMenuOpen;
            craftingMenu.SetActive(craftingMenuOpen);
            if (craftingMenuOpen)
            {
                inventoryOpen = true;
                inventory.SetActive(inventoryOpen);
            }
        }
    }
}
