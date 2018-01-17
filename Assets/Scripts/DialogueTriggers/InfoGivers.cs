using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoGivers : DialogueTrigger {
    public string dialogue;


    public override void SayStuff()
    {
        bubble.SetActive(true);
        speech.text = dialogue;
    }
}
