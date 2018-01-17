using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogueTrigger : MonoBehaviour
{
    public GameObject bubble;
    public Text speech;

    private void Start()
    {
        bubble = transform.Find("SmallCanvas").Find("SpeechBubble").gameObject;
        speech = bubble.transform.Find("Speech").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.Find("SmallCanvas").Find("SpeechBubble").gameObject.SetActive(false);
            SayStuff();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bubble.SetActive(false);
        }
    }

    public abstract void SayStuff();
}
