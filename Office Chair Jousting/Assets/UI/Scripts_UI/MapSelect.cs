using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {
    public RectTransform nav;
    public RectTransform[] slots = new RectTransform[4];
    private float controlDelay;
    int navPos = 0;
    // Use this for initialization
    void Start () {
        MoveNav(0);
	}
	
	// Update is called once per frame
	void Update () {
        navControl();
    }


    void navControl()
    {
        if (Input.GetAxis("Joy0X") <= -0.2)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.1)
            {
                MoveNav(-1);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0X") >= 0.2)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.1)
            {
                MoveNav(1);
                controlDelay = 0;
            }
        }
    }

    void MoveNav(int change)
    {
        if (change > 0)
        {
            if (navPos + change < slots.Length - 1)
            {
                navPos += change;
            }
            else
            {
                navPos = slots.Length - 1;
            }
        }
        else
        {
            if (navPos + change >= 0)
            {
                navPos += change;
            }
            else
            {
                navPos = 0;
            }
        }
        nav.position = slots[navPos].position;
    }
}
