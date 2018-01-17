using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuMovement : MonoBehaviour {
    Button button;
    RectTransform rt;
    RectTransform myRt;
    bool clicked;

    private void Start()
    {
        clicked = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(Drag);
        rt = transform.parent.GetComponent<RectTransform>();
        myRt = GetComponent<RectTransform>();
    }

    private void Drag()
    {
        clicked = !clicked;
    }

    private void Update()
    {
        if (clicked)
        {
            rt.position = Input.mousePosition - myRt.localPosition; 
            if (rt.name.Equals("Inventory"))
            {
                rt.position += new Vector3(0, myRt.rect.height + 10, 0);
            }
            else if (rt.name.Equals("StatsMenu"))
            {
                rt.position += new Vector3(0, myRt.rect.height - 100, 0);
            }
        }
    }
}
