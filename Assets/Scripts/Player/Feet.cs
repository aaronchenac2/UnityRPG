using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {
    Move move;
    Speech speech;

    private void Start()
    {
        move = FindObjectOfType<Move>();
        speech = FindObjectOfType<Speech>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("World"))
        {
            speech.Say("Oof!");
            move.inAir = false;
            move.anim.SetBool("Jump", false);
            move.anim.SetBool("SpecialSwordSpin", false);
            move.stuck = false;
            move.spin = false;
            move.rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("World"))
        {
            speech.Say("WHEEEEEEEE");
            move.inAir = true;
            move.anim.SetBool("Jump", true);
        }
    }
}
