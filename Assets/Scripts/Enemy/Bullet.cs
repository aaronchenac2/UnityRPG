using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    public GameObject source;
    public float maxSpeed;

    private void Start()
    {
        maxSpeed = 50;
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if (transform.position.Equals(target))
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, maxSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Stats playerStats = other.GetComponent<Stats>();
            Enemy enemy = source.GetComponent<Enemy>();
            playerStats.HP -= (playerStats.baseHP / 20 + enemy.level) * (1.2f - (playerStats.Armor / playerStats.level * playerStats.hp / playerStats.maxHp)) * (enemy.level / playerStats.level);
        }
    }
}
