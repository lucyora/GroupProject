using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {
    public RectTransform nav;
    public RectTransform[] slots = new RectTransform[4];
    private float controlDelay;
    int navPos = 0;
    public Button back_btn;
    public CharacterSelect charSelect;
    // Use this for initialization
    void Start ()
    {
        charSelect = GameObject.Find("Character0").GetComponent<CharacterSelect>();
        MoveNav(0);
	}
	
	// Update is called once per frame
	void Update () {
        //fix for all players are ready
        if(charSelect.playerReady[0]==true && Input.GetButtonDown("JoyB0"))
        {
            back_btn.onClick.Invoke();
            charSelect.toMap = false;
            charSelect.playerReady[0] = false;
        }
        navControl();
    }


    void navControl()
    {
        if (Input.GetAxis("Joy0X") <= -0.2)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.1)
            {
                Debug.Log("Music");

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
