﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{

    private GameManager gamemanager;
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
    public Image[] playerImageT;
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



    // Use this for initialization
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        isplayer = new int[4] {gamemanager.Player1isAI,
                               gamemanager.Player2isAI,
                               gamemanager.Player3isAI,
                               gamemanager.Player4isAI };
        textrng = Random.Range(0, 4);
        PlayerInfo = new int[4] { 0, 0, 0, 0 };

        if (gamemanager.GameMode == GameManager.gamemode.TeamDeathMatch)
        {
            InitTeamHudTxt();
        }
        else if(gamemanager.GameMode == GameManager.gamemode.DeathMatch)
        {
            InitHudTxt();
        }
        else if (gamemanager.GameMode == GameManager.gamemode.OttomanEmpire)
        {
            gameHUD[2].gameObject.SetActive(true);
        }
        
        //playerClass = GetComponent<Player>();

        TimeLeft = gamemanager.MatchTime;

        //Test get player hud object
        //Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");
    }

    private void Awake()
    {
        Time.timeScale = 1;
        startTime = Time.timeSinceLevelLoad;
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
        //Timer
        TimeLeft = TimeLeft - Time.deltaTime;

        //count goes up for survival mode
        //Up count
        if (gamemanager.GameMode == GameManager.gamemode.OttomanEmpire)
        { 
            min = (int)(Time.time / 60f);
            sec = (int)(Time.time % 60f);
            float timescore = gamemanager.ottomanscores = Time.time;
            float gamescore = gamemanager.ottomanKillScore + timescore;
            OttomanScoreHUD.text = "Score: " + gamescore.ToString("f0");
        }
        else         //Down count
        { 
            min = (int)(TimeLeft / 60f);
            sec = (int)(TimeLeft % 60f);
            if (TimeLeft < 0) 
            {
                TimeLeft = 0;
                Invoke("GameOverHUD", call);                //avoid calling function every frame by invoking after specific amount of second
                gamemanager.GameIsOver = true;
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

        //Score




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
        //Temporary scores setting
        float ps1 = gamemanager.score[0];
        float ps2 = gamemanager.score[1];
        float ps3 = gamemanager.score[2];
        float ps4 = gamemanager.score[3];

        PlayerScoresHUD[0].gameObject.SetActive(true);
        PlayerScoresHUD[1].gameObject.SetActive(true);
        PlayerScoresHUD[2].gameObject.SetActive(true);
        PlayerScoresHUD[3].gameObject.SetActive(true);

        PlayerScoresHUD[0].text = "Score : " + ps1;
        PlayerScoresHUD[1].text = "Score : " + ps2;
        PlayerScoresHUD[2].text = "Score : " + ps3;
        PlayerScoresHUD[3].text = "Score : " + ps4;

    }
    void TeamScroeUpdate()
    {
        float team1 = gamemanager.teamscores[0];
        float team2 = gamemanager.teamscores[1];

        TeamScoresHUD[0].text = "Team Score: " + team1;
        TeamScoresHUD[1].text = "Team Score: " + team2;
    }


    public void InitHudTxt()
    {
        gameHUD[0].gameObject.SetActive(true);
        //TODO: Only active players get HUD

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
    
       /* if (PlayerPrefs.GetInt("Player1isAI") == 1)
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
            }*/
    }
    public void InitTeamHudTxt()
    {
        gameHUD[1].gameObject.SetActive(true);

            for (int j = 0; j < 4; j++)
            {
                if(PlayerPrefs.GetInt("Player"+(j+1)+"Team") == 0)
                {
                    //check which team is each player is on
                }
            }
        
    }



    void SubMenu()
    {
        //Restart
        //Back to main menu  
    }
}
