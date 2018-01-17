using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : MonoBehaviour {
    float spawnCD;
    bool campSleep;

    public GameObject EnemyPrefab;
    public GameObject[] spawnPoints;
    public int level;


    private void Start()
    {
        spawnCD = 10;
        campSleep = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !campSleep)
        {
            campSleep = true;
            StartCoroutine(WakeCamp());
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                GameObject enemy = Instantiate(EnemyPrefab, spawnPoints[j].transform.position, new Quaternion(0, 0, 0, 0));
                enemy.GetComponent<Enemy>().Level = level;

                if (EnemyPrefab.name.Equals("EnemyA"))
                {
                    enemy.GetComponent<EnemyA>().target = other.transform;
                }
            }
        }
    }

    IEnumerator WakeCamp()
    {
        yield return new WaitForSeconds(spawnCD);
        campSleep = false;
    }
}
