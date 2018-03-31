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
        Testplayer = GameObject.Find("HUDCanvas/HUDManager/Player1");

    }

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad;

        InitHudTxt();

        //TODO: set player img
        // Player1 == Player.character.Jenny...
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
  
        //TODO: think about localization...
        //m_Text = GetComponent<TextMeshProUGUI>();
        //string temp = LocalizationManager.instance.GetLocalizedValue(key);
        //m_Text.SetText(temp);   //Replace text form the localization file


        if (TimeLeft < 0 || GameStatus == (int)MatchMode.MatchDone) //Time out or match done
        {
            GameOver();
        }

        //Test for checking player death
        if (!playerClass.isAlive)
        {
            GameOver();
            Debug.Log("Player1's gone far away...");
        }

        //TODO: Think about pop up hud with sub-menu to choose restart
        if (Input.GetButtonDown("JoyA"))
        {
            //GameOver();
        }
        else if (Input.GetButtonDown("JoyB"))
        {
            //GameStart();
            GameStatusText.text = "Test";
        }

    }
    void ScoreUpdateHUD()
    {
        //TODO: Keep tracking players' score.
        P1score.text = "Score : " + Scores;

       
        

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
