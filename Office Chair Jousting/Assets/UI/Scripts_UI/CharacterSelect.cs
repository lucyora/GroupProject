using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour {
    public bool powerUpSelect = false;
    public float strenght;
    public float speed;
    public float stability;
    public Image image;
    public Image powerUp;
    public Sprite[] sprite;
    public Sprite[] powersprite;
    public Slider[] slider;
    public int index =1;
    public int powerIndex = 1;
    public string[] names;
    public GameObject panel;
    public bool[] Ai;
    public bool[] characterSelReady;
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
                playerControl(i);
            }
            else if(string.IsNullOrEmpty(names[i]))
            {
                Ai[i] = true;
            }
        }
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

    void powerupSelect()
    {
        if (index == 1)
        {
            powerUp.sprite = powersprite[0];
        }
        if (index == 2)
        {
            powerUp.sprite = powersprite[1];
        }
        if (index == 3)
        {
            powerUp.sprite = powersprite[2];
        }
        if (index == 4)
        {
            powerUp.sprite = powersprite[3];
        }
        if (index == 5)
        {
            powerUp.sprite = powersprite[4];
        }
    }
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

    void playerControl(int i)
    {
        if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2F && panel == GameObject.Find("Character"+i) && powerUp == false)
        {
            Debug.Log(Input.GetJoystickNames()[i] + " is moved");
            index++;
            if (index > 5)
            {
                index = 1;
            }
            CharacterDisplay();
            Characterstats();
        }
        else if(Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2F && panel == GameObject.Find("Character" + i) && powerUp == true)
        {
            powerIndex++;
            powerupSelect();
        }
    }
}
