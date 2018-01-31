using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour {
    public float strenght;
    public float speed;
    public float stability;
    public Image image;
    public Sprite[] sprite;
    public Slider[] slider;
    public int index =1;
    public string[] names;
    public GameObject panel;
    public bool[] Ai;
    void Start()
    {
        Ai = new bool[] { true, true, true, true};
        CharacterDisplay();
        Characterstats();
    }
    void Update()
    {
         names = Input.GetJoystickNames();
        for (int i = 0; i < names.Length; i++)
        {
            if(!string.IsNullOrEmpty(names[i]))
            {
                Ai[i] = false;
                playerControl();
            }
            else if(string.IsNullOrEmpty(names[i]))
            {
                Ai[i] = true;
                Debug.Log(names[i]+"Is not Connected");
            }
        }
       // toggleLeft();
       // toggleRight();
    }

   /* void toggleLeft()
    {
       
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            if(index < 1)
            {
                index = 5;
            }
            CharacterDisplay();
            Characterstats();
        }
    }
    void toggleRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }
    }*/
    void Characterstats()
    {
        slider[0].value = strenght / 100;
        slider[1].value = speed / 100;
        slider[2].value = stability / 100;
    }
    void CharacterDisplay()
    {
        if (index ==1)
        {
            character1Stats();
        }
        if (index == 2)
        {
            character2Stats();
        }
        if (index == 3)
        {
            character3Stats();
        }
        if (index == 4)
        {
            character4Stats();
        }
        if (index == 5)
        {
            character5Stats();
        }
    }

    void character1Stats()
    {
        strenght = 25;
        speed = 75;
        stability = 45;
        image.sprite = sprite[0];
    }
    void character2Stats()
    {
        strenght = 85;
        speed = 20;
        stability = 7;
        image.sprite = sprite[1];
    }
    void character3Stats()
    {
        strenght = 52;
        speed = 98;
        stability = 10;
        image.sprite = sprite[2];
    }
    void character4Stats()
    {
        strenght = 10;
        speed = 86;
        stability = 45;
        image.sprite = sprite[3];
    }
    void character5Stats()
    {
        strenght = 25;
        speed = 32;
        stability = 87;
        image.sprite = sprite[4];
    }

    void playerControl()
    {
        if (Mathf.Abs(Input.GetAxis("Joy" + 0 + "X")) > 0.2F && panel == GameObject.Find("Character1"))
        {
            Debug.Log(Input.GetJoystickNames()[0] + " is moved");
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }
        else if (Mathf.Abs(Input.GetAxis("Joy" + 1 + "X")) > 0.2F && panel == GameObject.Find("Character2"))
        {
            Debug.Log(Input.GetJoystickNames()[1] + " is moved");
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }
        else if (Mathf.Abs(Input.GetAxis("Joy" + 2 + "X")) > 0.2F /*|| Mathf.Abs(Input.GetAxis("Joy" + 0 + "Y")) > 0.2F*/ && panel == GameObject.Find("Character3"))
        {
            Debug.Log(Input.GetJoystickNames()[2] + " is moved");
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }
        else if (Mathf.Abs(Input.GetAxis("Joy" + 3 + "X")) > 0.2F /*|| Mathf.Abs(Input.GetAxis("Joy" + 0 + "Y")) > 0.2F*/ && panel == GameObject.Find("Character4"))
        {
            Debug.Log(Input.GetJoystickNames()[3] + " is moved");
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }

    }
}
