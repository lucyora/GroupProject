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

    public float timeleft;
    public bool bSurvivalMode;
    public TextMeshProUGUI TimerText;

    // Use this for initialization
    void Start ()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        playerClass = GetComponent<Player>();

    }

    private void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
    }
    // Update is called once per frame
    void Update ()
    {
        Scores = gamemanager.getScores();
        //TODO: grap and use player status on screen

        timeleft -= Time.deltaTime;

        if (timeleft < 0)
        {
            GameOver();
        }
        

        //Up count
        if(bSurvivalMode == true)
        { 
            min = (int)(Time.time / 60f);
            sec = (int)(Time.time % 60f);
        }
        else         //Down count
        { 
            min = (int)(timeleft / 60f);
            sec = (int)(timeleft % 60f);
        }

        string temp = "Time " + min.ToString("00") + ":" + sec.ToString("00");
        TimerText.text = temp;

        //TODO: Scoring

        //TODO: think about localization...
        //m_Text = GetComponent<TextMeshProUGUI>();
        //string temp = LocalizationManager.instance.GetLocalizedValue(key);
        //m_Text.SetText(temp);   //Replace text form the localization file

    }

    void GameOver()
    {
        
    }
}
