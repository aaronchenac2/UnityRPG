using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSourceScript : MonoBehaviour
{
    float initHp;
    public float hp { get; set; }
    public GameObject[] lootPrefabs;
    public Loot[] loot;

    // Assigns go, name, and rarity
    void RegisterLoot(int index, GameObject go)
    {
        loot[index] = new Loot();
        loot[index].go = go;
        string lootName = go.name;
        loot[index].name = lootName;
        if (lootName.Equals("Log") || lootName.Equals("Rock"))
        {
            loot[index].rarity = .6f;
            if (Hacks.hacks)
            {
                loot[index].rarity = 1;
            }
        }
        else if (lootName.Equals("Diamond"))
        {
            loot[index].rarity = .2f;
        }
    }

    private void Start()
    {
        int cutOff = name.IndexOf('(');
        if (cutOff != -1)
        {
            name = name.Substring(0, cutOff).Trim();
        }

        // hp of tree = height
        if (CompareTag("Tree"))
        {
            hp = GetComponent<CapsuleCollider>().height * 2;
        }

        if (name.Equals("BigRock"))
        {
            hp = GetComponent<SphereCollider>().radius * 2;
        }

        initHp = hp;

        // Declares the loot class array and registers the prefabs into the class
        loot = new Loot[lootPrefabs.Length];
        for (int j = 0; j < lootPrefabs.Length; j++)
        {
            RegisterLoot(j, lootPrefabs[j]);
        }
    }

    // Spawns items if thing dies
    private void Update()
    {
        if (hp <= 0)
        {
            SpawnItems();
            Destroy(gameObject);
        }
    }

    // Spawns the loot items
    void SpawnItems()
    {
        // For every loot item
        for (int j = 0; j < loot.Length; j++)
        {
            // For size of thing / 2
            for (int i = 0; i < (int)initHp / 2; i++)
            {
                // Rarity
                if (Random.value < loot[j].rarity)
                {
                    GameObject go = Instantiate(loot[j].go, new Vector3(transform.position.x + Random.value * 2, Random.value * initHp / 2,
    transform.position.z + Random.value * 2), new Quaternion(0, 0, 0, 0));
                    go.name = go.name.Replace("(Clone)", "").Trim();
                }
            }
        }
    }
}

// Loot class to store information
public class Loot
{
    public GameObject go { get; set; }
    public string name { get; set; }
    public float rarity { get; set; }
}