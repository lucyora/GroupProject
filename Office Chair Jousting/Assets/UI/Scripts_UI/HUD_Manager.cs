using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Manager : MonoBehaviour
{

    private GameManager gamemanager;
    private List<float> Scores = new List<float>();


    Player playerClass;
    private float sec, min;
    private float startTime;

    public float TimeLeft;
    public bool SurvivalMode;

    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI GameStatusText;
    public TextMeshProUGUI P1score;
    public TextMeshProUGUI P2score;
    public TextMeshProUGUI P3score;
    public TextMeshProUGUI P4score;

    public GameObject Testplayer;

    public int GameStatus;

    public enum MatchMode { MatchDone, DeathMatch, Team, Survival };

    // Use this for initialization
    void Start ()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        playerClass = GetComponent<Player>();
        InitHudTxt();


        //Test get player hud object
        //Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");

    }

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad;

        InitHudTxt();

        //TODO: set player img
        // Player1 == Player.character.Jenny... etc
    }
    // Update is called once per frame
    void Update ()
    {
        //Timer
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0)
        {
            GameOver();
        }

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
            GameOver();
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

        P1score.text = "Score : " + ps1; 
        P2score.text = "Score : " + ps2;
        P3score.text = "Score : " + ps3;
        P4score.text = "Score : " + ps4;
    }

    void GameOver()
    {
        GameStatusText.enabled = true;

    }
    void InitHudTxt() //
    {
        GameStatusText.enabled = false;
    }

    void subMenu()
    {
        //Restart
        //Back to main menu  
    }
}
