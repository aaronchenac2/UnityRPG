using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform myHome;
    public Transform myFirstHome;

    private void Start()
    {
        myFirstHome = transform.parent.Find("CameraInitPos");
        myHome = myFirstHome;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myHome.position, 5f * Time.deltaTime);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, myHome.rotation.eulerAngles, 5f * Time.deltaTime);
    }
}
