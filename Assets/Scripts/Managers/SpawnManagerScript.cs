using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject[] prefabs;
    public int[] spawnRatePer100;

    float tWidth;
    float tLength;

    private void Start()
    {
        float tWidth = Terrain.activeTerrain.terrainData.size.x;
        float tLength = Terrain.activeTerrain.terrainData.size.z;

        for (int j = 0; j < prefabs.Length; j++)
        {
            for (int i = 0; i < tWidth * tLength / 100 / 100 * spawnRatePer100[j]; i++)
            {
                float newX = Random.value * tWidth;
                float newZ = Random.value * tLength;
                float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                GameObject go = Instantiate(prefabs[j], new Vector3(newX, newY, newZ), new Quaternion(0, 0, 0, 0));
                go.name = go.name.Replace("(Clone)", "").Trim();
            }
        }
    }

}
