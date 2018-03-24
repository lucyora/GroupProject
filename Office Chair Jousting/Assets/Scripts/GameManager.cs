using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerOptions
{
    public Player.character PlayerCharacter;
    public Controller.current_player Current_Player;
    public bool isPlayer;
    public int PlayerIndex;
    public float Score;
    public PlayerOptions(bool isplayer, int playerindex, Controller.current_player current_player)
    {
        isPlayer = isplayer;
        PlayerIndex = playerindex;
        Current_Player = current_player;
        Score = 0;
    }

}

public class GameManager : MonoBehaviour
{
    private GameObject SpawnArea;
    public List<GameObject> PlayerList;
    public List<PlayerOptions> PlayersOptions;
    /// Fill in with values from UI
    public int Player1isAI;
    public int Player2isAI;
    public int Player3isAI;
    public int Player4isAI;
    public int botcount;
    ///
    public int TotalPlayerCount;     
    public enum gamemode { DeathMatch, TeamDeathMatch, OttomanEmpire, LastManSitting };
    public gamemode GameMode;

    public void UpdateScore(int index, float value)
    {
        PlayersOptions[index].Score = value;
    }

    public List<float> getScores()
    {
        List<float> ScoresList = new List<float>();
        foreach (PlayerOptions PlayerOption in PlayersOptions)
        {
            ScoresList.Add(PlayerOption.Score);
        }
        return ScoresList;
    }

    void Start()
    {

        Player1isAI = PlayerPrefs.GetInt("Player1isAI");
        Player2isAI = PlayerPrefs.GetInt("Player2isAI");
        Player3isAI = PlayerPrefs.GetInt("Player3isAI");
        Player4isAI = PlayerPrefs.GetInt("Player4isAI");
        SpawnArea = GameObject.FindGameObjectWithTag("SpawnArea");

        //Player Spawning. This assumes Player 2 can only be a human is player 1 human and player 3 can only be human if player 2 is human, etc.
        if (Player1isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 1, Controller.current_player.Player_1));
            SetPlayerOptions(PlayersOptions[0], 0);
            SpawnPlayer(0);
            TotalPlayerCount += 1;

            if (Player2isAI == 1)
            {
                PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
                PlayersOptions.Add(new PlayerOptions(true, 1,Controller.current_player.Player_2));
                SetPlayerOptions(PlayersOptions[1], 1);
                SpawnPlayer(1);
                TotalPlayerCount += 1;

                if (Player3isAI == 1)
                {
                    PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
                    PlayersOptions.Add(new PlayerOptions(true, 1,Controller.current_player.Player_3));
                    SetPlayerOptions(PlayersOptions[2], 2);
                    SpawnPlayer(2);
                    TotalPlayerCount += 1;

                    if (Player4isAI == 1)
                    {
                        PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
                        PlayersOptions.Add(new PlayerOptions(true, 1,Controller.current_player.Player_4));
                        SetPlayerOptions(PlayersOptions[3], 3);
                        SpawnPlayer(3);
                        TotalPlayerCount += 1;
                    }
                }
            }
            
        }

        /* Bot spawning code goes here
         * 
         * 
         * */


        
        switch (GameMode)
        {
            case gamemode.DeathMatch:
                break;
            case gamemode.TeamDeathMatch:
                break;
            case gamemode.OttomanEmpire:
                break;
            case gamemode.LastManSitting:
                break;
        }
        InvokeRepeating("DeathWatch", 0.01f, 1.0f);
    }


    void SpawnPlayer(int index)
    {
        Vector3 Center = SpawnArea.GetComponent<BoxCollider>().center;
        float max = SpawnArea.GetComponent<BoxCollider>().bounds.max.x;
        Vector3 spawnposition = new Vector3(Random.Range(-max, max), SpawnArea.transform.position.y, Random.Range(-max, max));
        PlayerList[index] = Instantiate(PlayerList[index], spawnposition,Quaternion.identity);
    }

    
    void SetPlayerOptions(PlayerOptions playeroptions, int index)
    {

        if (PlayerList[index].GetComponent<Player>() != null)
        {
            PlayerList[index].GetComponent<Player>().Character = playeroptions.PlayerCharacter;
            PlayerList[index].GetComponent<Player>().Current_Player = playeroptions.Current_Player;
            PlayerList[index].GetComponent<Player>().Score = playeroptions.Score;
            PlayerList[index].GetComponent<Player>().InternalPlayerIndex = playeroptions.PlayerIndex;
        }
        else
        {
            //Bot options go here
        }

    }
    
    void DeathWatch()
    {
        int index = 0;
        foreach (var player in PlayerList)
        {
            
            if (player.GetComponent<Player>() != null)
            {
                if (!player.GetComponent<Player>().isAlive)
                {
                    PlayerList[index] = (GameObject)Resources.Load("prefabs/player", typeof(GameObject));
                    SetPlayerOptions(PlayersOptions[index], index);
                    SpawnPlayer(index);
                    break;
                }

            }
            else
            {
                //Bot Respawning goes here
            }
            index++;

        }

    }

}