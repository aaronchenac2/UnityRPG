using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public GameObject shield;
    public Image shieldCDImage;
    public Stats stats;
    public bool shielded;

    float time;
    float lastShield;
    float shieldDuration;
    float shieldCD;

    private void Start()
    {
        // Custom set values
        shieldDuration = 3;
        shieldCD = 8;

        // Set all to 0
        time = 0;
        lastShield = -shieldCD;
    }

    private void Update()
    {
        time += Time.deltaTime;

        shieldCDImage.fillAmount = (time - lastShield) / shieldCD;
        if (Input.GetButtonDown("Shield") && lastShield + shieldCD < time)
        {
            shielded = true;
            lastShield = time;
            shield.SetActive(true);
            stats.speed *= 2;
            StartCoroutine(DeactivateShield());
        }
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(shieldDuration);
        shielded = false;
        stats.speed /= 2;
        shield.gameObject.SetActive(false);
    }
}
