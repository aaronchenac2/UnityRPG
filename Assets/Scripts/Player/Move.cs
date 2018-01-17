using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public Stats stats;
    public Animator anim;
    public ToolsManagerScript tms;
    public CameraScript cs;
    Speech speech;
    public Rigidbody rb;
    public bool inAir;
    public bool stuck;
    public bool spin;
    public bool spinDown;
    public bool mounted;

    public GameObject sword;

    float width;
    float height;
    float panSpeed;

    private void Start()
    {
        speech = FindObjectOfType<Speech>();
        inAir = false;
        stuck = false;
        spin = false;
        spinDown = false;
        mounted = false;

        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
        panSpeed = 100f;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Tool"))
        {
            if (Input.GetButton("Secondary"))
            {
                speech.Say("Next!!!");
                tms.NextTool();
            }
            else
            {
                speech.Say("Bye tool!!");
                tms.ChangeTool("Toss");
            }
        }

        if (Input.GetMouseButton(2))
        {
            cs.enabled = false;
            /* Click camera function
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000, 9))
            {
                cs.gameObject.transform.LookAt(hit.point);
            }
            */

            // Panning function
            float inputX = Input.mousePosition.x;
            float inputY = Input.mousePosition.y;

            if (inputY > .75f * height)
            {
                cs.gameObject.transform.eulerAngles += new Vector3(-panSpeed * Time.deltaTime, 0, 0);
            }
            else if (inputY < .25f * height)
            {
                cs.gameObject.transform.eulerAngles += new Vector3(panSpeed * Time.deltaTime, 0, 0);
            }
            else if (inputX > .75f * width)
            {
                cs.gameObject.transform.eulerAngles += new Vector3(0, panSpeed * Time.deltaTime, 0);
            }
            else if (inputX < .25f * width)
            {
                cs.gameObject.transform.eulerAngles += new Vector3(0, -panSpeed * Time.deltaTime, 0);
            }

            if (Input.GetButton("Secondary"))
            {
                cs.enabled = true;
                cs.gameObject.transform.rotation = cs.myFirstHome.transform.rotation;
            }
        }


        if (stuck)
        {
            return;
        }

        if (Input.GetButtonDown("Jump") && !inAir)
        {
            rb.AddForce(0, 10, 0, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Fire1") && spin)
        {
            //anim.SetBool("Jump", false);
            anim.SetBool("SpecialSwordSpin", false);
            spin = false;
            stuck = false;
            rb.velocity = Vector3.zero;
        }
        if (inAir && Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Fire1") && tms.GetActiveToolName().Equals("Sword"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * 3000, ForceMode.Acceleration);
            spin = true;
            stuck = true;
            anim.SetBool("SpecialSwordSpin", true);
        }
        if (inAir && Input.GetAxis("Vertical") < 0 && Input.GetButtonDown("Fire1") && tms.GetActiveToolName().Equals("Sword"))
        {
            rb.AddForce(-transform.up * 1000, ForceMode.Acceleration);
            stuck = true;
            spinDown = true;
            anim.SetBool("SpecialSwordSpin", true);
        }

        if (mounted)
        {
            return;
        }
        float translation = Input.GetAxis("Vertical") * stats.speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * stats.rotationSpeed * Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        if (transform.position.y < -5)
        {
            float newY = Terrain.activeTerrain.SampleHeight(transform.position) + 1;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        if (spinDown)
        {
            transform.Rotate(90, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("World") && spinDown)
        {
            cs.myHome = cs.myFirstHome;
            spinDown = false;
        }
    }
}
