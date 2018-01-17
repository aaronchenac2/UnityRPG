using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManagerScript : MonoBehaviour {
    public GameObject[] prefabs;

    // Returns prefab of given name
    public GameObject GetPrefab(string name)
    {
        for (int j = 0; j < prefabs.Length; j++)
        {
            if (prefabs[j].name.Equals(name))
            {
                return prefabs[j];
            }
        }
        return null;
    }
}
