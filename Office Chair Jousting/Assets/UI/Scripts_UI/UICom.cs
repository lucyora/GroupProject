using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICom : MonoBehaviour {

    CharacterSelect[] charSelect;
    MapSelect map;
    SetModeType gameMode;

    void Start()
    {
        charSelect = new CharacterSelect[4] {GameObject.Find("Character0").GetComponent<CharacterSelect>(),
                                             GameObject.Find("Character1").GetComponent<CharacterSelect>(),
                                             GameObject.Find("Character2").GetComponent<CharacterSelect>(),
                                             GameObject.Find("Character3").GetComponent<CharacterSelect>()};
        map = GameObject.Find("MapCanvas").GetComponent<MapSelect>();
        gameMode = GameObject.Find("SelectPlayMode").GetComponent<SetModeType>();
        gameMode = gameMode.GetComponent<SetModeType>();
    }

    void Update()
    {
        GameMode();
        for (int i = 0; i < 4; i++)
        {

            CharacterToMode(i); // from game mode selection to character selection
            if (charSelect[i].toMap == false && gameMode.doneWithMenu == true)
            {            teamset(i);
                //character selection Screen
                charSelect[i].AiCharSet(i);
                charSelect[i].playerControl(i);
                charSelect[i].checkPlayerReady(i);
                AllPlayersReady(i);// go to map selection
            }
            MapScreen(i); // map Screen Controls
            MaptoCharSelect(i);//go to character select from map

        }
    }
    //when all players are ready go to map selection screen
    void AllPlayersReady(int i)
    {
        bool ready = allReady();
        int teams = DifferentTeams();
        if (gameMode.gameModeIndex == 0)
        {
            if(charSelect[1].IsPlayer[1] == 0)
            {
                return;
            }
            else if(charSelect[1].IsPlayer[1] == 1)
            {
                // INPUT CODE TO CHECK IF ALL PLAYERS ARE ON THE SAME TEAM
            }
        }
        if ( ready == true)
        {
            charSelect[i].ToMap_btn.onClick.Invoke();
            charSelect[i].toMap = true;
        }
    }

    private int DifferentTeams()
    {
        for (int i = 0; i < charSelect.Length; i++)
        {
            if(charSelect[i].teamindex[i] == 0)
            {
                return 0;
            }
            else if(charSelect[i].teamindex[i] == 1)
            {
                return 1;
            }
        }
        return 2;
    }
    // bool function to determine if all array elements are true
    private bool allReady()
    {
        for(int i = 0; i < charSelect.Length; i++)
        {
            if(charSelect[i].playerReady[i] == false)
            {
                return false;
            }
        }
        return true;
    }
    //go from map select back to character select with B button
    void MaptoCharSelect(int i)
    {
        if (charSelect[i].playerReady[i] == true && charSelect[i].toMap == true && gameMode.doneWithMenu == true && Input.GetButtonDown("JoyB0"))
        {
            map.back_btn.onClick.Invoke();
            charSelect[i].toMap = false;
            charSelect[i].powerChosen[i] = false;
            charSelect[i].powerUp.color = Color.white;
            charSelect[i].playerReady[i] = false;
        }
    }

    void CharacterToMode(int i)
    {
        if(charSelect[i].characterChosen[i] == false &&  Input.GetButtonDown("JoyB" + i))
        {
            gameMode.doneWithMenu = false;
            charSelect[i].ToGameMode_btn.onClick.Invoke();
        }
    }
    void MapScreen(int i)
    {
        if (charSelect[i].toMap == true && gameMode.doneWithMenu == true)
        {
            map.navControl(); // map move controls
            map.CamPan(map.mapPosition[map.mapIndex]); // map Camera Pan
            // Player 1 A Press map Selected
            if (Input.GetButtonDown("JoyA0"))
            {
                Save(); // Saves all current player information
                map.LoadMap(map.mapIndex);
            }
        }
    }
    //GameMode Move Controls
    void GameMode()
    {
        if(gameMode.doneWithMenu == false)
        {
            gameMode.navControl();
            if (Input.GetButtonUp("JoyA0"))
            {
                gameMode.ToChar_btn.onClick.Invoke();
                gameMode.doneWithMenu = true;
            }
            else if (Input.GetButtonDown("JoyB0"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void teamset(int i)
    {
        if(gameMode.gameModeIndex == 0)
        {
            charSelect[i].teamNames.gameObject.SetActive(true);
        }
        else
        {
            charSelect[i].teamNames.gameObject.SetActive(false);
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt("GameMode", gameMode.gameModeIndex);

        PlayerPrefs.SetInt("Player1Team", charSelect[0].teamindex[0]);
        PlayerPrefs.SetInt("Player2Team", charSelect[1].teamindex[1]);
        PlayerPrefs.SetInt("Player3Team", charSelect[2].teamindex[2]);
        PlayerPrefs.SetInt("Player4Team", charSelect[3].teamindex[3]);

        PlayerPrefs.SetInt("Character1", charSelect[0].index[0]);
        PlayerPrefs.SetInt("Character2", charSelect[1].index[1]);
        PlayerPrefs.SetInt("Character3", charSelect[2].index[2]);
        PlayerPrefs.SetInt("Character4", charSelect[3].index[3]);

        PlayerPrefs.SetInt("Player1isAI", charSelect[0].IsPlayer[0]);
        PlayerPrefs.SetInt("Player2isAI", charSelect[1].IsPlayer[1]);
        PlayerPrefs.SetInt("Player3isAI", charSelect[2].IsPlayer[2]);
        PlayerPrefs.SetInt("Player4isAI", charSelect[3].IsPlayer[3]);

        PlayerPrefs.SetInt("Char1Power", charSelect[0].powerIndex[0]);
        PlayerPrefs.SetInt("Char2Power", charSelect[1].powerIndex[1]);
        PlayerPrefs.SetInt("Char3Power", charSelect[2].powerIndex[2]);
        PlayerPrefs.SetInt("Char4Power", charSelect[3].powerIndex[3]);
    }

}
