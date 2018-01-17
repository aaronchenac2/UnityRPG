using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneIntro : MonoBehaviour {
    public GameObject attacker;
    public Animator attackerAnim;
    public GameObject aBubble;
    public Text aDiag;
    public GameObject victim;
    public Animator victimAnim;
    public GameObject vBubble;
    public Text vDiag;
    public Image black;

    GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.gameObject.SetActive(false);
            transform.Find("ActivateOnTrigger").gameObject.SetActive(true);
            StartCoroutine(FadeIn());
            StartCoroutine(Huzzah());
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = black.color;
            c.a = f;
            black.color = c;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 0f; f < 1; f += 0.1f)
        {
            Color c = black.color;
            c.a = f;
            black.color = c;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }
    }

    IEnumerator Huzzah()
    {
        yield return new WaitForSeconds(.3f);
        aBubble.SetActive(true);
        vBubble.SetActive(false);
        aDiag.text = "Hoo hooo look at that pretty lady :)....";
        yield return new WaitForSeconds(3);
        vBubble.SetActive(true);
        vDiag.text = "I'm feeling a malicious aura around me...";
        aDiag.text = "Hehehhee Mr. Box isn't here... Now is my chance!!";
        yield return new WaitForSeconds(4);
        aDiag.text = "Huzzah!!";
        vDiag.text = "AHHHHHHHH!!!";
        attackerAnim.SetTrigger("Jump");
        yield return new WaitForSeconds(1);
        attackerAnim.SetTrigger("Run");
        victimAnim.SetTrigger("Run");
        vDiag.text = "Save me Mr. Box!!!";
        victimAnim.enabled = false;
        victim.transform.parent = attacker.transform;
        victim.transform.localPosition = new Vector3(0, 1, 0);
        yield return new WaitForSeconds(2.3f);
        victim.SetActive(false);
        attacker.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
        gameObject.SetActive(false);
    }
}
