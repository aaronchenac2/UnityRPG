using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public CameraScript cs;
    public GameObject myCamera;
    public GameObject cameraResidue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject camRes = Instantiate(cameraResidue, transform.position, new Quaternion(0, 0, 0, 0));
            myCamera.transform.parent = camRes.transform;
            Invoke("ReturnToPlayer", .01f);
            transform.parent.parent.position = other.GetComponent<Bullet>().source.transform.position + new Vector3(0, 20, 0);
            gameObject.SetActive(false);
        }
    }

    void ReturnToPlayer()
    {
        myCamera.transform.parent = gameObject.transform.parent.parent;
        cs.myHome = cs.myFirstHome;
    }
}
