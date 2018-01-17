using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : DialogueTrigger {
    public GameObject ornnCraftingMenu;
    public GameObject craftingSubMenu;

    private void Start()
    {
        bubble = transform.Find("SmallCanvas").Find("SpeechBubble").gameObject;
        speech = bubble.transform.Find("Speech").GetComponent<Text>();
    }

    public override void SayStuff()
    {
        bubble.SetActive(true);
        speech.text = "Stand near me. CCraft weapons and armor.";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bubble.SetActive(false);
            ornnCraftingMenu.SetActive(false);
            craftingSubMenu.SetActive(false);
        }
    }
}
