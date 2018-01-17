using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGivers : DialogueTrigger {
    InventoryScript inventoryScript;
    public string[] reqNames;
    public int[] reqCounts;
    Requirement[] reqs;

    public string meetSpeech;
    public string passSpeech;
    public GameObject[] objectsToBeUnlocked;

    bool completed;
    bool focused;
    bool framed = false;

    GameObject mainCamera;
    GameObject myCameraPos;
    CameraScript cameraScript;


    private void Start()
    {
        completed = false;
        focused = false;

        bubble = transform.Find("SmallCanvas").Find("SpeechBubble").gameObject;
        speech = bubble.transform.Find("Speech").GetComponent<Text>();
        inventoryScript = FindObjectOfType<InventoryScript>();

        reqs = new Requirement[reqNames.Length];
        for (int j = 0; j < reqNames.Length; j++)
        {
            reqs[j] = new Requirement(reqNames[j], reqCounts[j]);
        }
    }

    public override void SayStuff()
    {
        bubble.SetActive(true);
        speech.text = meetSpeech;
    }

    private void OnTriggerStay(Collider other)
    {
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
                }
                else
                {
                    focused = false;
                    cameraScript.myHome = cameraScript.myFirstHome;
                    other.GetComponent<Move>().stuck = false;
                }

                if (!completed && CheckTransaction())
                {
                    completed = true;
                    speech.text = passSpeech;
                    meetSpeech = passSpeech;
                    for (int j = 0; j < objectsToBeUnlocked.Length; j++)
                    {
                        objectsToBeUnlocked[j].SetActive(false);
                    }
                }
                framed = true;
                StartCoroutine(Reenable());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bubble.SetActive(false);
            if (focused)
            {
                focused = false;
                cameraScript.myHome = cameraScript.myFirstHome;
                other.GetComponent<Move>().stuck = false;
            }
        }
    }

    IEnumerator Reenable()
    {
        yield return null;
        framed = false;
    }

    bool CheckTransaction()
    {
        bool allClear = true;
        string errorMessage = "";
        for (int j = 0; j < reqs.Length; j++)
        {
            string reqName = reqs[j].name;
            int reqCount = reqs[j].count;
            if (reqName != "")
            {
                Slot myItem = inventoryScript.GetItem(reqName);
                if (myItem == null)
                {
                    errorMessage += "You don't have any " + reqName + "s!!\n";
                    allClear = false;
                }
                else if (myItem.count < reqCount)
                {
                    int lack = reqCount - myItem.count;
                    errorMessage += "You need " + lack + " more " + myItem.name;
                    if (lack > 1)
                    {
                        errorMessage += "s!!\n";
                    }
                    else
                    {
                        errorMessage += "!!\n";
                    }
                    allClear = false;
                }
            }
        }
        if (allClear)
        {
            for (int j = 0; j < reqs.Length; j++)
            {
                string reqName = reqs[j].name;
                int reqCount = reqs[j].count;
                if (reqName != "")
                {
                    inventoryScript.SubtractItem(reqName, reqCount);
                }
            }
            return true;
        }
        else
        {
            speech.text = errorMessage;
            return false;
        }
    }
}
