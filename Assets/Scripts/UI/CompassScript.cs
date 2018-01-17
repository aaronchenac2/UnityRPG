using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour {
    public GameObject player;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, player.transform.eulerAngles.y);
	}
}
