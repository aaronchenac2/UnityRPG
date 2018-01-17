using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour
{
    public GameObject speechBubble;
    public Text speech;
    string[] coolDialogues = { "Sweet!!", "Awesome!!", "Coolio!!", "Breathtaking!!", "Magnificient!!",
        "Wonderful!!", "Amazing!!", "Stunning!!", "Impressive!!", "Astonishing!!", "Beautiful!!" };

    public void Say(string dialogue)
    {
        speech.text = dialogue;
        speechBubble.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ShutUp());
    }

    public void SayCoolDialogue()
    {
        speech.text = coolDialogues[(int) (Random.value * coolDialogues.Length)];
        speechBubble.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ShutUp());
    }

    IEnumerator ShutUp()
    {
        yield return new WaitForSeconds(3);
        speechBubble.SetActive(false);
    }
}