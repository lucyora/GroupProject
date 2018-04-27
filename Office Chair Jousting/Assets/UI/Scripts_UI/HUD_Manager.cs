using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{

    public GameManager gamemanager;
    // for invoke GameOverHud
    float call = 0.5f;          
    //Timer
    private float sec, min;
    private float startTime;
    int textrng;

    public float TimeLeft;

    //TODO: Add player data on NHUD
    private int[] PlayerInfo;

    public TextMeshProUGUI TimerText;
    //public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI[] PlayerScoresHUD;
    public TextMeshProUGUI[] TeamScoresHUD;
    public TextMeshProUGUI OttomanScoreHUD;
    public TextMeshProUGUI[] LeaderboardHUD;
    public Image[] playerImage;
    public Sprite[] playerSprite = new Sprite[4];

    private int[] isplayer;

    public GameObject[] gameHUD;

    public TextMeshProUGUI winnerTXT;
    public TextMeshProUGUI EveryFired;
    //[SerializeField]
    public GameObject[] PlayerHUD;

    public GameObject gameOverCanvas;
    public GameObject PauseMenu;
    //public GameObject Testplayer;

    float ps1;
    float ps2;
    float ps3;
    float ps4;



    // Use this for initialization
    void Start()
    {
        if(gamemanager == null)
        {
            gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        isplayer = new int[4] {gamemanager.Player1isAI,
                               gamemanager.Player2isAI,
                               gamemanager.Player3isAI,
                               gamemanager.Player4isAI };
        textrng = Random.Range(0, 4);
        PlayerInfo = new int[4] { 0, 0, 0, 0 };
        
        //playerClass = GetComponent<Player>();

        TimeLeft = gamemanager.MatchTime;

        //Test get player hud object
        //Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");
    }

    private void Awake()
    {

        Time.timeScale = 1;

    }

    public bool PauseEvent(bool GameisPaused)
    {
        if (GameisPaused)
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
            return false;
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.gameObject.SetActive(false);
            return true;
        }
    }
    void Update ()
    {
        ps1 = gamemanager.score[0];
        ps2 = gamemanager.score[1];
        ps3 = gamemanager.score[2];
        ps4 = gamemanager.score[3];

        startTime = Time.timeSinceLevelLoad;
        if (gamemanager.GameMode == GameManager.gamemode.TeamDeathMatch)
        {
            gameHUD[1].gameObject.SetActive(true);
        }
        else if (gamemanager.GameMode == GameManager.gamemode.DeathMatch)
        {
            InitHudTxt();
        }
        else if (gamemanager.GameMode == GameManager.gamemode.OttomanEmpire)
        {
            gameHUD[2].gameObject.SetActive(true);
        }

        //Timer
        TimeLeft = TimeLeft - Time.deltaTime;

        //count goes up for survival mode
        //Up count
        if (gamemanager.GameMode == GameManager.gamemode.OttomanEmpire)
        {
            if (!gamemanager.GameIsOver)
            {
                min = (int)(startTime / 60f);
                sec = (int)(startTime % 60f);
                float timescore = gamemanager.ottomanscores = startTime;
                float gamescore = gamemanager.ottomanKillScore + timescore;
                OttomanScoreHUD.text = "Score: " + gamescore.ToString("f0");
            }
        }
        else         //Down count
        { 
            min = (int)(TimeLeft / 60f);
            sec = (int)(TimeLeft % 60f);
            if (TimeLeft < 0) 
            {
                endthegame();
            }
            if(gamemanager.GameMode == GameManager.gamemode.TeamDeathMatch)
            {
                TeamScroeUpdate();
            }
            else if(gamemanager.GameMode == GameManager.gamemode.DeathMatch)
            {
                ScoreUpdateHUD();
            }
        }

        string temp = "Time " + min.ToString("00") + ":" + sec.ToString("00");
        TimerText.text = temp;
    }
    public void endthegame()
    {
        //Messy and ultimately shouldn't be done like this but we're running out of time
        TimeLeft = 0;
        Invoke("GameOverHUD", call);                //avoid calling function every frame by invoking after specific amount of second
        gamemanager.GameIsOver = true;
    }
    void GameOverHUD()
    {
        call = 1000000000;
        //TimeLeft = 0;
        Time.timeScale = 1;

        

        switch (textrng)
        {
            case 0:
                EveryFired.text = "HR would like a word with you...";
                break;
            case 1:
                EveryFired.text = "This company retreat has not been very productive";
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
        PlayerScoresHUD[0].gameObject.SetActive(true);
        PlayerScoresHUD[1].gameObject.SetActive(true);
        PlayerScoresHUD[2].gameObject.SetActive(true);
        PlayerScoresHUD[3].gameObject.SetActive(true);

        PlayerScoresHUD[0].text = "Score : " + ps1;
        PlayerScoresHUD[1].text = "Score : " + ps2;
        PlayerScoresHUD[2].text = "Score : " + ps3;
        PlayerScoresHUD[3].text = "Score : " + ps4;

        if (ps1 == ps2 || ps2 == ps3 || ps3 == ps4 || ps4 == ps1 || ps4 == ps2 || ps3 == ps1)
        {
            winnerTXT.text = "Looks like a TIE here";
        }
        if (ps1 > ps2 && ps1 > ps3 && ps1 > ps4)
        {
            winnerTXT.text = "Player 1 won free trip to Hawaii \n and will be working remotely";
        }
        if (ps2 > ps1 && ps2 > ps3 && ps2 > ps4)
        {
            winnerTXT.text = "Player 2 got promotion \n without getting actual raise";
        }
        if (ps3 > ps1 && ps3 > ps2 && ps3 > ps4)
        {
            winnerTXT.text = "Player 3 won coffee machine \n for the staff";
        }
        if (ps4 > ps1 && ps4 > ps2 && ps4 > ps3)
        {
            winnerTXT.text = "Player 4 won unpaid vacation \n for three months";
        }
        if (ps1 == 0 && ps2 == 0 && ps3 == 0 && ps4 == 0)
        {
            winnerTXT.text = "No winners, \n now get back to work";
        }

    }
    void TeamScroeUpdate()
    {
        float team1 = gamemanager.teamscores[0];
        float team2 = gamemanager.teamscores[1];

        TeamScoresHUD[0].text = "Score: " + team1;
        TeamScoresHUD[1].text = "Score: " + team2;

        if (team1 > team2)
        {
            winnerTXT.text = "Team 1 won the match";
        }
        else
        {
            winnerTXT.text = "Team 2 won the match";
        }
    }


    public void InitHudTxt()
    {
        gameHUD[0].gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (PlayerPrefs.GetInt("Character" + (i + 1)) == j)
                {
                    playerImage[i].sprite = playerSprite[j];
                }
            }
            if(isplayer[i] == 1 )
            {
                PlayerHUD[i].gameObject.SetActive(true);
            }  
        }
    }
}
