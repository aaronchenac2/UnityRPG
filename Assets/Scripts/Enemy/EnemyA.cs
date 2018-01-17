using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy {
    public override void Attack(Transform player)
    {
        // Maybe add force on boss delayed
        //player.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 3);
        anim.SetTrigger("GroundPound");
    }
}
