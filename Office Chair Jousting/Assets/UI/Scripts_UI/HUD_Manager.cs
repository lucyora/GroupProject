using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{

    private GameManager gamemanager;
    private List<float> Scores = new List<float>();

//    Player playerClass;

    //Timer
    private float sec, min;
    private float startTime;

    private float TimeLeft;
    public bool SurvivalMode;

    //TODO: Add player data on NHUD
    private int[] PlayerInfo;

    public TextMeshProUGUI TimerText;
    //public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI[] PlayerScoresHUD;
  //  public GameObject[] playersHUD;
    public Image[] playerImage;
    public Sprite[] playerSprite = new Sprite[4];

    //[SerializeField]
    public GameObject[] PlayerHUD;

    public GameObject gameOverCanvas;
    //public GameObject Testplayer;

    public int GameStatus;

    public enum MatchMode { MatchDone, DeathMatch, Team, Survival };


    // Use this for initialization
    void Start ()
    {
        PlayerInfo = new int[4] { 0, 0, 0, 0 };
        //assigns sprite to Player Images
        for (int i = 0; i<4; i++)
        {
            if(PlayerPrefs.GetInt("Character" + i) == i)
            {

                playerImage[i].sprite = playerSprite[i];
                Debug.Log("Is character Sprite: " +playerSprite[i]);
            }
        }

        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        //playerClass = GetComponent<Player>();
        InitHudTxt();


        //Test get player hud object
        //Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");
    }

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
        TimeLeft = 180;
        InitHudTxt();

        //TODO: set player img
        // Player1 == Player.character.Jenny... etc
    }
    // Update is called once per frame
    void Update ()
    {
        //Timer
        TimeLeft -= Time.deltaTime;


        //count goes up for survival mode
        //Up count
        if (SurvivalMode == true)
        { 
            min = (int)(Time.time / 60f);
            sec = (int)(Time.time % 60f);
        }
        else         //Down count
        { 
            min = (int)(TimeLeft / 60f);
            sec = (int)(TimeLeft % 60f);
        }

        string temp = "Time " + min.ToString("00") + ":" + sec.ToString("00");
        TimerText.text = temp;

        //Score
        ScoreUpdateHUD();

        if (TimeLeft < 0 || GameStatus == (int)MatchMode.MatchDone) //Time out or match done
        {
            TimeLeft = 0;
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = -1;
        }

        //Test for checking player death
        //if (!playerClass.isAlive)
        //{
        //    GameOver();
        //    Debug.Log("Player1's gone far away...");
        //}

        //TODO: Think about pop up hud with sub-menu to choose restart
        //if (Input.GetButtonDown("JoyA"))
        //{
        //    //GameOver();
        //}
        //else if (Input.GetButtonDown("JoyB"))
        //{
        //    //GameStart();
        //    GameStatusText.text = "Test";
        //}

    }
    void ScoreUpdateHUD()
    {
        //Temporary scores setting
        float ps1 = gamemanager.score[0];
        float ps2 = gamemanager.score[1];
        float ps3 = gamemanager.score[2];
        float ps4 = gamemanager.score[3];

        PlayerScoresHUD[0].text = "Score : " + ps1;
        PlayerScoresHUD[1].text = "Score : " + ps2;
        PlayerScoresHUD[2].text = "Score : " + ps3;
        PlayerScoresHUD[3].text = "Score : " + ps4;

    }


    public void InitHudTxt()
    {
        //TODO: Only active players get HUD

        if(PlayerPrefs.GetInt("Player1isAI") == 1)
        {
            PlayerHUD[0].gameObject.SetActive(true);
        }
        else
        {
            PlayerHUD[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Player2isAI") == 1)
        {
            PlayerHUD[1].gameObject.SetActive(true);
        }
        else
        {
            PlayerHUD[1].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Player3isAI") == 1)
        {
            PlayerHUD[2].gameObject.SetActive(true);
        }
        else
        {
            PlayerHUD[2].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Player4isAI") == 1)
        {
            PlayerHUD[3].gameObject.SetActive(true);
        }
        else
        {
            PlayerHUD[3].gameObject.SetActive(false);
        }


    }

    void SubMenu()
    {
        //Restart
        //Back to main menu  
    }
}
