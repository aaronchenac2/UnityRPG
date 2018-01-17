using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            levelText.text = "" + level;
            MaxSouls = Mathf.Pow(level, 1.5f) * 1.2f + 10;
            Souls = maxSouls;
        }
    }

    public float souls;
    public float Souls
    {
        get
        {
            return souls;
        }
        set
        {
            souls = value;
            soulSlider.value = souls;
            if (souls <= 0)
            {
                SpawnItems();
                levelText.transform.parent.gameObject.SetActive(false);
                soulSlider.gameObject.SetActive(false);
                anim.SetTrigger("Die");
                target = transform;
                Destroy(gameObject, 3);
                //enabled = false;
            }
        }
    }

    public float maxSouls;
    public float MaxSouls

    {
        get
        {
            return maxSouls;
        }
        set
        {
            maxSouls = value;
            soulSlider.maxValue = maxSouls;
        }
    }

    public char type; // A B C
    public GameObject[] lootPrefabs;
    public Loot[] loot;

    public Transform target;
    public NavMeshAgent agent;
    public Animator anim;

    public Text levelText;
    public Slider soulSlider;

    private void Start()
    {
        Level = level;
        MaxSouls = maxSouls;
        Souls = souls;

        agent = GetComponent<NavMeshAgent>();

        // Declares the loot class array and registers the prefabs into the class
        loot = new Loot[lootPrefabs.Length];
        for (int j = 0; j < lootPrefabs.Length; j++)
        {
            RegisterLoot(j, lootPrefabs[j]);
        }
    }

    public abstract void Attack(Transform player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            Attack(other.transform);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }

    // Spawns the loot items
    void SpawnItems()
    {
        if (loot == null)
        {
            // Declares the loot class array and registers the prefabs into the class
            loot = new Loot[lootPrefabs.Length];
            for (int j = 0; j < lootPrefabs.Length; j++)
            {
                RegisterLoot(j, lootPrefabs[j]);
            }
        }
        // For every loot item
        for (int j = 0; j < loot.Length; j++)
        {
            int iterations = -1;
            switch(type)
            {
                case 'A':
                    iterations = 1;
                    break;
                case 'B':
                    iterations = 2;
                    break;
                case 'C':
                    iterations = 4;
                    break;
            }
            // For level
            for (int i = 0; i < level * iterations; i++)
            {
                // Rarity
                if (Random.value < loot[j].rarity)
                {
                    GameObject go = Instantiate(loot[j].go, new Vector3(transform.position.x + Random.value * 2, Random.value * level,
    transform.position.z + Random.value * 2), new Quaternion(0, 0, 0, 0));
                    go.name = go.name.Replace("(Clone)", "").Trim();
                }
            }
        }
    }

    void RegisterLoot(int index, GameObject go)
    {
        loot[index] = new Loot();
        loot[index].go = go;
        string lootName = go.name;
        loot[index].name = lootName;
        loot[index].rarity = 1;
        Debug.Log(loot[index].go);
    }
}
