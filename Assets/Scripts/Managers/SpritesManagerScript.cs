using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesManagerScript : MonoBehaviour {
    public Sprite[] sprites;

    // Returns sprite of given name
    public Sprite GetImage(string name)
    {
        for (int j = 0; j < sprites.Length; j++)
        {
            if (sprites[j].name.Equals(name))
            {
                return sprites[j];
            }
        }
        return null;
    }
}
