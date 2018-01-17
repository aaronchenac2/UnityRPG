using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : DialogueTrigger {
    public MenuControls mc;

    bool focused;
    bool framed = false;
    public bool mugged = false;

    GameObject mainCamera;
    GameObject myCameraPos;
    CameraScript cameraScript;

    private void Start()
    {
        bubble = transform.Find("SmallCanvas").Find("SpeechBubble").gameObject;
        speech = bubble.transform.Find("Speech").GetComponent<Text>();
    }

    public override void SayStuff()
    {
        bubble.SetActive(true);
        if (mugged)
        {
            return;
        }
        speech.text = "Hello there! If you have money, come buy some goods!";
    }

    private void OnTriggerStay(Collider other)
    {
        if (mugged)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact") && !framed)
            {
                mainCamera = other.transform.Find("Main Camera").gameObject;
                myCameraPos = transform.Find("CameraPos").gameObject;
                cameraScript = mainCamera.GetComponent<CameraScript>();

                if (!focused)
                {
                    focused = true;
                    cameraScript.myHome = myCameraPos.transform;
                    other.GetComponent<Move>().stuck = true;
                    mc.doranMerchantMenu.SetActive(true);
                }
                else
                {
                    focused = false;
                    cameraScript.myHome = cameraScript.myFirstHome;
                    other.GetComponent<Move>().stuck = false;
                }

                framed = true;
                StartCoroutine(Reenable());
            }
        }
    }

    IEnumerator Reenable()
    {
        yield return null;
        framed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!mugged)
            {
                bubble.SetActive(false);
            }
            mc.doranMerchantMenu.SetActive(false);
            mc.subMerch.SetActive(false);
            if (focused)
            {
                focused = false;
                cameraScript.myHome = cameraScript.myFirstHome;
                other.GetComponent<Move>().stuck = false;
            }
        }
    }
}
