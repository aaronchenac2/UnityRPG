using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    public GameObject cameraToLookAt;

    private void Start()
    {
        cameraToLookAt = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
