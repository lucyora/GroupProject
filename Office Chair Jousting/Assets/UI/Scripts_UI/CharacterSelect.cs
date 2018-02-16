using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour {

    private float strenght;
    private float speed;
    private float stability;
    public Button ToMap;
    public AudioSource selectSound; // sounds DEBUG
    public AudioSource confirmSound; // sounds DEBUG
    public AudioSource deselectSound; // sounds DEBUG
    public AudioSource readySound; //sounds DEBUG
    public Image image;
    public Image powerUp;
    public Sprite[] sprite;
    public Sprite[] powersprite;
    public Slider[] slider;
    public GameObject panel;
    public GameObject readyPanel;
    public TextMeshProUGUI powerstats;
    public TextMeshProUGUI powerName;
    public TextMeshProUGUI StartText;

    private float time;
    public int[] index;
    public int[] powerIndex;
    public string[] names;
    public bool[] Ai;
    public bool[] characterChosen;
    public bool[] powerChosen;
    public bool[] playerReady;
    Animator menuAnim;
    public bool toMap = false;

    void Start()
    {
        menuAnim = GetComponent<Animator>();
        names = new string[4];
        index = new int[5] { 1, 1, 1, 1, 1 };
        powerIndex = new int[6] { 1, 1, 1, 1, 1, 1 };
        Ai = new bool[4] { true, true, true, true };
        characterChosen = new bool[4] { false, false, false, false };
        powerChosen = new bool[4] { false, false, false, false };
        playerReady = new bool[4] { false, false, false, false };
        CharacterDisplay(1);
        powerindex(1);
        Characterstats();

    }
    void Update()
    {
        names = Input.GetJoystickNames();
        //loop for individual controller inputs
        for (int i = 0; i < names.Length; i++)
        {
            if(!string.IsNullOrEmpty(names[i]))
            {
                Ai[i] = false;
                if(toMap == false)
                {
                    playerControl(i);
                    checkPlayerReady(i);
                    AllPlayerReadyCheck(i);
                }
            }
            else if(string.IsNullOrEmpty(names[i]))
            {   
                Ai[i] = true;              
            }
        }
    }
    
    //stats to slider
    void Characterstats()
    {
        slider[0].value = strenght / 100;
        slider[1].value = speed / 100;
        slider[2].value = stability / 100;
    }
    //character index
    void CharacterDisplay(int i)
    {
        if (index[i] ==1)
        {
            character1Stats();
        }
        else if (index[i] == 2)
        {
            character2Stats();
        }
        else if (index[i] == 3)
        {
            character3Stats();
        }
        else if (index[i] == 4)
        {
            character4Stats();
        }
        else if (index[i] == 5)
        {
            character5Stats();
        }
    }

    void playerControl(int i)
    {
        time += Time.deltaTime;
        if (time > 0.1)
        {
            time = 0;
            //character select move left and right
            if (Input.GetAxis("Joy" + i + "X") > 0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == false)
            {
                selectSound.Play();
                menuAnim.Play("CharacterMoveRight");
                index[i]++;
                if (index[i] > 5)
                {
                    index[i] = 1;
                }
                CharacterDisplay(i);
                Characterstats();
            }
            else if (Input.GetAxis("Joy" + i + "X") < -0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == false)
            {
                selectSound.Play();
                menuAnim.Play("CharacterMoveLeft");
                index[i]--;
                if (index[i] < 1)
                {
                    index[i] = 5;
                }
                CharacterDisplay(i);
                Characterstats();
            }
            //power select move left and right
            if (Input.GetAxis("Joy" + i + "X") > 0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                selectSound.Play();
                menuAnim.Play("PowerMoveRight");
                powerIndex[i]++;
                if (powerIndex[i] > 6)
                {
                    powerIndex[i] = 1;
                }
                powerindex(i);
            }
            else if (Input.GetAxis("Joy" + i + "X") < -0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                selectSound.Play();
                menuAnim.Play("PowerMoveLeft");
                powerIndex[i]--;
                if (powerIndex[i] < 1)
                {
                    powerIndex[i] = 6;
                }
                powerindex(i);
            }
        }
    }
    void checkPlayerReady(int i)
    {
        if (panel == GameObject.Find(("Character" + i)))
        {
            //select playable character
            if (Input.GetButtonDown("JoyA" +i ) && characterChosen[i] == false && powerChosen[i] == false)
            {
                confirmSound.Play();
                menuAnim.Play("CharacterAPress");
                characterChosen[i] = true;
                image.color = Color.grey;
            }
            //back to select playable character
            else if (Input.GetButtonDown("JoyB" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                characterChosen[i] = false;
                menuAnim.Play("CharacterIdle");
                deselectSound.Play();
                image.color = Color.white;
            }
            //select power
            else if (Input.GetButtonDown("JoyA" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                confirmSound.Play();
                powerChosen[i] = true;
                menuAnim.Play("PowerAPress");
                menuAnim.Play("PressWhenReadyFlash");
                StartText.gameObject.SetActive(true);
                powerUp.color = Color.grey;
            }
            //back to select powerUp
            else if (Input.GetButtonDown("JoyB" + i) && characterChosen[i] == true && powerChosen[i] == true)
            {
                powerChosen[i] = false;
                powerUp.color = Color.white;
                deselectSound.Play();
                menuAnim.Play("PowerIdle");
            }
            //check if player has selected powerUp and player
            if (characterChosen[i] == false || powerChosen[i] == false)
            {
                StartText.gameObject.SetActive(false);
                readyPanel.gameObject.SetActive(false);
                playerReady[i] = false;
            }
            //indicates player is ready
            else if (characterChosen[i] == true && powerChosen[i] == true && Input.GetButtonDown("JoyStart" + i))
            {
                readySound.Play();
                playerReady[i] = true;
                readyPanel.gameObject.SetActive(true);
            }
        }
    }
    void AllPlayerReadyCheck(int i)
    {
  //      for (int j = 0; j <= playerReady.Length; j++)
  //      {
         //  bool ready = isReady(playerReady);
            Debug.Log("J is: " + i);
            if (Ai[i] == false && playerReady[i] == false)
            {
                Debug.Log("All Players are not Ready");
                readyPanel.SetActive(false);
            }
            else if (Ai[i] == false && playerReady[i] == true)
            {
                Debug.Log("All Players Ready");
                ToMap.onClick.Invoke();
                toMap = true;
            }
       // }
    }
  /*  bool isReady(bool[] playerred)
    {
        for (int i = 0; i < playerred.Length; i++)
        {
            if (playerred[i] == false)
            {
                return false;
            }
        }
        return true;
    }*/

    //charcter stats to display
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

    //power index
    void powerindex(int i)
    {
        if (powerIndex[i] == 1)
        {
            powerUp.sprite = powersprite[0];
            powerName.text = "Donut";
            powerstats.text = "Strenght + \n speed---\nStability++";
        }
        else if (powerIndex[i] == 2)
        {
            powerUp.sprite = powersprite[1];
            powerName.text = "Promotion";
            powerstats.text = "Strenght +++ \n speed---\nStability+++";
        }
        else if (powerIndex[i] == 3)
        {
            powerUp.sprite = powersprite[2];
            powerName.text = "WD-40";
            powerstats.text = "Strenght \n speed++\nStability+++";
        }
        else if (powerIndex[i] == 4)
        {
            powerUp.sprite = powersprite[3];
            powerName.text = "Coffee";
            powerstats.text = "Strenght  \n speed+++\nStability--";
        }
        else if (powerIndex[i] == 5)
        {
            powerUp.sprite = powersprite[4];
            powerName.text = "Rocket Booster";
            powerstats.text = "Strenght+  \n speed+++\nStability---";
        }
        else if (powerIndex[i] == 6)
        {
            powerUp.sprite = powersprite[5];
            powerName.text = "Kale";
            powerstats.text = "Strenght+++  \n speed\nStability-";
        }
    }
}
