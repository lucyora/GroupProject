using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : Controller
{

    private GameManager gamemanager;
    private List<float> Scores = new List<float>();

//    Player playerClass;

    //Timer
    private float sec, min;
    private float startTime;
    int textrng;


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

    public TextMeshProUGUI winnerTXT;
    public TextMeshProUGUI EveryFired;
    //[SerializeField]
    public GameObject[] PlayerHUD;

    public GameObject gameOverCanvas;
    public GameObject PauseMenu;
    //public GameObject Testplayer;

    public int GameStatus;

    public enum MatchMode { MatchDone, DeathMatch, Team, Survival };


    // Use this for initialization
    void Start ()
    {
        textrng = Random.Range(0, 4);
        PlayerInfo = new int[4] { 0, 0, 0, 0 };
        
        //assigns sprite to Player Images
        for (int i = 0; i<4; i++)
        {
            Debug.Log(PlayerPrefs.GetInt("Character" + (i+1)));
            for (int j = 0; j < 4; j++)
            {
                if (PlayerPrefs.GetInt("Character" + (i+1)) == j)
                {
                    playerImage[i].sprite = playerSprite[j];
                    Debug.Log("Is character Sprite: " + playerSprite[j]);
                }
            }

        }

        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        //playerClass = GetComponent<Player>();
        InitHudTxt();
        TimeLeft = 180;

        //Test get player hud object
        //Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");
    }

    private void Awake()
    {
        Time.timeScale = 1;
        startTime = Time.timeSinceLevelLoad;

        InitHudTxt();

        //TODO: set player img
        // Player1 == Player.character.Jenny... etc
    }
    // Update is called once per frame
    void Update ()
    {
        //Timer
        TimeLeft = TimeLeft - Time.deltaTime;

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


        //if (TimeLeft < 0 || GameStatus == (int)MatchMode.MatchDone) //Time out or match done
        if (TimeLeft < 0) 
        {
            TimeLeft = 0;
            GameOverHUD();
            gamemanager.gameisover = true;
        }
        if(gameOverCanvas.gameObject.activeInHierarchy == true && Input.GetButtonDown(SelectedP_Start))
        {
            Time.timeScale = 1;
            PauseMenu.gameObject.SetActive(false);
        }
        else if (gameOverCanvas.gameObject.activeInHierarchy == false && Input.GetButtonDown(SelectedP_Start))
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
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
    void GameOverHUD()
    {
        //TimeLeft = 0;
        Time.timeScale = 1;
        switch (textrng)
        {
            case 0:
                EveryFired.text = "HR would like a word with you...";
                break;
            case 1:
                EveryFired.text = "This company retreat sucked";
                break;
            case 2:
                EveryFired.text = "We don't get paid enough for this";
                break;
            case 3:
                EveryFired.text = "If we unionized, this game wouldn't make sense";
                break;
            case 4:
                EveryFired.text = "This is what happens when you give employees jousts instead of laptops.";
                break;


        }
        gameOverCanvas.gameObject.SetActive(true);

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
