using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public Stats stats;
    public Animator anim;
    Move move;
    Animator myAnim;

    Vector3 initPos;
    Quaternion initRot;
    float time;
    float lastSwing;
    float swingCD;

    private void Start()
    {
        initPos = transform.localPosition;
        initRot = transform.localRotation;
        myAnim = GetComponent<Animator>();
        move = transform.parent.parent.parent.parent.GetComponent<Move>();
        time = 0;

        RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
            if (ac.animationClips[i].name.Equals("Swing" + name))       //If it has the same name as your clip
            {
                swingCD = ac.animationClips[i].length;
                Debug.Log(name + " " + swingCD);
            }
        }

        lastSwing = -swingCD;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && time > lastSwing + swingCD)
        {
            lastSwing = time;
            transform.localPosition = initPos;
            transform.localRotation = initRot;
            anim.SetTrigger("Swing" + name);
            if (myAnim != null)
            {
                myAnim.enabled = false;
                StopAllCoroutines();
                StartCoroutine(RestartAnim());
            }
            move.stuck = true;
            StartCoroutine(Unstuck());
        }
    }

    IEnumerator Unstuck()
    {
        yield return new WaitForSeconds(.5f);
        move.stuck = false;
    }

    IEnumerator RestartAnim()
    {
        yield return new WaitForSeconds(swingCD + .1f);
        myAnim.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Tree") && name.Equals("Axe"))
            || (other.name.Equals("BigRock") && name.Equals("Pickaxe")))
        {
            other.GetComponent<MaterialSourceScript>().hp -= stats.AD * 2;
        }
        if (other.CompareTag("Enemy") && name.Equals("Sword"))
        {
            Enemy es = other.transform.parent.GetComponent<Enemy>();

            float baseDamage = es.maxSouls / 10;
                          //percent hp  // deals more dmg if has higher hp          // deals more dmg if is higher lvl              
            float damage = baseDamage + .2f * baseDamage * stats.hp / stats.maxHp + baseDamage * stats.AD / stats.level * stats.level / es.level;
            es.Souls -= damage;
            stats.Souls += damage * .5f;
        }
    }


}
