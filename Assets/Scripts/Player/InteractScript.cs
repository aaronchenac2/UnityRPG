using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {
    bool firstPickUp;
    public Animator myAnim;
    public InventoryScript inventoryScript;
    public ToolsManagerScript tms;
    public Stats stats;
    public Move move;
    public float time;
    Speech speech;

    float cutCD;
    float lastCut;

    bool framed = false;

    private void Start()
    {
        firstPickUp = true;
        speech = FindObjectOfType<Speech>();
        time = 0;
        lastCut = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            MaterialSourceScript treeScript = other.GetComponent<MaterialSourceScript>();
            cutCD = 10 / stats.AD;
            if (Input.GetButtonDown("Fire1") && time > lastCut + cutCD && !tms.HasChildren())
            {
                speech.Say("Wpao!");
                Debug.Log("Animation called");
                myAnim.SetTrigger("AttackTree");
                lastCut = time;
                treeScript.hp -= stats.AD;
            }
        }
        // Collectable tag has to be a child of the actual game object
        if (other.CompareTag("Collectable"))
        {
            if (firstPickUp)
            {
                firstPickUp = false;
                speech.Say("FFrickin pick it up already!!!");
            }
            if (Input.GetButtonDown("Collect"))
            {
                speech.SayCoolDialogue();
                Debug.Log("Collecting: " + other.name);
                inventoryScript.AddItem(other.transform.parent.gameObject);
                Destroy(other.transform.parent.gameObject);
            }
        }

        if (other.CompareTag("Pusheen"))
        {
            if (Input.GetButtonDown("Interact") && !framed)
            {
                framed = true;
                StartCoroutine("Unframe");
                Debug.Log(move.mounted);
                if (move.mounted)
                {
                    move.mounted = false;
                    PusheenControler pc = other.GetComponent<PusheenControler>();
                    pc.player = null;
                }
                else
                {
                    move.mounted = true;
                    PusheenControler pc = other.GetComponent<PusheenControler>();
                    pc.player = transform.parent.parent.gameObject;
                    pc.move = move;
                    pc.stats = stats;
                }
            }
        }
    }

    IEnumerator Unframe()
    {
        yield return new WaitForEndOfFrame();
        framed = false;
    }

}
