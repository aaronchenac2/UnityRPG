using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    public GameObject bullet;
    public GameObject gunPoint;
    GameObject player;
    bool aggroed = false;

    bool loaded;
    float fireCD;


    public override void Attack(Transform player)
    {
        // Only trigger once;
        if (aggroed)
        {
            return;
        }
        this.player = player.gameObject;
        aggroed = true;
        loaded = true;
        Debug.Log(level);
        fireCD = .5f * (20.4f / (level + 4) - .1f);
        Debug.Log(fireCD);
    }

    private void Update()
    {
        // Don't want to do excessive calculations if not aggroed
        if (!aggroed)
        {
            return;
        }

        // If player is not within twice the range of aggro collider, the bot will follow the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius * 3);
        if (!ContainsPlayer(colliders))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
        }

        // Always look at the player
        transform.LookAt(player.transform);

        // If gun is loaded, fire
        if (loaded)
        {
            GameObject b = Instantiate(bullet, gunPoint.transform.position, new Quaternion(0, 0, 0, 0));

            RaycastHit hit;
            Vector3 heading = (player.transform.position - gunPoint.transform.position);
            if (Physics.Raycast(gunPoint.transform.position, heading / heading.magnitude, out hit, 1000))
            {
                Bullet bScript = b.GetComponent<Bullet>();
                bScript.target = hit.point;
                bScript.source = gameObject;
            }
            loaded = false;
            Invoke("Reload", fireCD);
        }
    }

    bool ContainsPlayer(Collider[] colliders)
    {
        for (int j = 0; j < colliders.Length; j++)
        {
            if (colliders[j].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void Reload()
    {
        loaded = true;
    }


}
