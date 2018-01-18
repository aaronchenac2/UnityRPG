using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {
    public Enemy enemy;
    public bool damaged;
    float attackCD;

    private void Start()
    {
        attackCD = 1.5f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !damaged)
        {
            damaged = true;
            Stats playerStats = other.GetComponent<Stats>();
            //                 percent basehp + level              armor becomes weaker as hp decreases                                               // deals more dmg if enemy is higher lvl              
            playerStats.HP -= (playerStats.baseHP / 10 + enemy.level) * (1.2f - (playerStats.Armor / playerStats.level * playerStats.hp / playerStats.maxHp)) * (enemy.level / playerStats.level);
            StartCoroutine(Restance());
        }
    }

    IEnumerator Restance()
    {
        yield return new WaitForSeconds(attackCD);
        damaged = false;
    }
}
