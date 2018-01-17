using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MerchantSubMenuScript : MonoBehaviour
{
    public Button buyButton;
    public Text itemName;
    public Text itemCost;

    public Rigidbody myRB;
    public InventoryScript inventoryScript;

    int GetCost(string name)
    {
        if (name.Equals("Compass"))
        {
            return 3;
        }
        if (name.Equals("Health Potion"))
        {
            return 1;
        }
        return -1;
    }

    private void Start()
    {
        buyButton.onClick.AddListener(delegate { CheckTransaction(itemName.text); });
    }

    public void SetUpSubMenu(string name)
    {
        gameObject.SetActive(true);
        itemName.text = name;
        itemCost.text = "" + GetCost(name);
    }

    void CheckTransaction(string name)
    {
        GameObject merchant = null;
        Merchant ms = null;
        Collider[] others = Physics.OverlapSphere(myRB.transform.position, 5);
        for (int j = 0; j < others.Length; j++)
        {
            Debug.Log(others[j].name);
            if (others[j].CompareTag("Merchant"))
            {
                merchant = others[j].gameObject;
                ms = merchant.GetComponent<Merchant>();
                break;
            }
        }

        Slot tokens = inventoryScript.GetItem("Coin");
        if (tokens == null || tokens.count < GetCost(itemName.text))
        {
            inventoryScript.AddItem(itemName.text);
            ms.bubble.SetActive(true);
            ms.speech.text = "THIEF!!!!";
            ms.mugged = true;
            myRB.AddExplosionForce(1000, merchant.transform.position, 5);
        }
        else 
        {
            tokens.count -= GetCost(itemName.text);
            tokens.countText.text = "" + tokens.count;
            inventoryScript.AddItem(itemName.text);
            ms.bubble.SetActive(true);
            ms.speech.text = "Here's your " + itemName.text;
        }

    }
}
