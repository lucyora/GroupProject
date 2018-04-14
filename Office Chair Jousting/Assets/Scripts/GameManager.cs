using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerOptions
{
    public Player.character PlayerCharacter;
    public Player_GameController.current_player Current_Player;
    public bool isPlayer;
    public int PlayerIndex;
    public int Team;
    public PlayerOptions(bool isplayer, int playerindex, Player_GameController.current_player current_player,int team)
    {
        isPlayer = isplayer;
        PlayerIndex = playerindex;
        Current_Player = current_player;
        Team = team;
    }

}
[System.Serializable]
public class LargestHit
{
    public float magnitude;
    public string player;
    public string otherplayer;
    public LargestHit()
    {
        magnitude = 0.0f;
        player = "";
        otherplayer = "";
    }
}

public class GameManager : MonoBehaviour
{
    public enum gamemode { DeathMatch, TeamDeathMatch, OttomanEmpire, LastManSitting };
    public gamemode GameMode;
    public float MatchTime;

    public int Player1isAI;
    public int Player2isAI;
    public int Player3isAI;
    public int Player4isAI;
 
    public float[] score;
    public int[] teamscores;
    public LargestHit largesthit = new LargestHit();

    public bool DebugMode;
    public GameObject HudManager;
    public GameObject[] SpawnPoints;
    public GameObject[] PlayerIndicator;
    public List<GameObject> PlayerList;
    public List<PlayerOptions> PlayersOptions;
    public bool GameIsOver = false;
    public bool GameisPaused = false;
    public int botcount;
    public int TotalPlayerCount;

    public bool PlayerIsInElevator;

    void Start()
    {
        score = new float[4];
        if (!DebugMode)
        {
            Player1isAI = PlayerPrefs.GetInt("Player1isAI");
            Player2isAI = PlayerPrefs.GetInt("Player2isAI");
            Player3isAI = PlayerPrefs.GetInt("Player3isAI");
            Player4isAI = PlayerPrefs.GetInt("Player4isAI");
        }
        if (Player1isAI == 1)
        {
            PlayersOptions.Add(new PlayerOptions(true, 0, Player_GameController.current_player.Player_1,0));
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            SetPlayerOptions(PlayersOptions[0], 0);
            SpawnPlayer(0);
            TotalPlayerCount += 1;
        }
        if (Player2isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 1, Player_GameController.current_player.Player_2,0));
            SetPlayerOptions(PlayersOptions[1], 1);
            SpawnPlayer(1);
            TotalPlayerCount += 1;
        }
        if (Player3isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 2, Player_GameController.current_player.Player_3,0));
            SetPlayerOptions(PlayersOptions[2], 2);
            SpawnPlayer(2);
            TotalPlayerCount += 1;
        }
        if (Player4isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 3, Player_GameController.current_player.Player_4,0));
            SetPlayerOptions(PlayersOptions[3], 3);
            SpawnPlayer(3);
            TotalPlayerCount += 1;
        }





        /* Bot spawning code goes here
         * 
         * 
         * */
        if (!DebugMode)
        {
            switch (PlayerPrefs.GetInt("GameMode"))
            {
                case 0:
                    GameMode = gamemode.TeamDeathMatch;
                    break;
                case 1:
                    GameMode = gamemode.DeathMatch;
                    break;
                case 2:
                    GameMode = gamemode.OttomanEmpire;
                    break;
                case 4:
                    GameMode = gamemode.LastManSitting;
                    break;
            }

        }

        InvokeRepeating("DeathWatch", 0.01f, 1.0f);
    }
    void Update()
    {
        int index = 0;
        if (PlayerIndicator.Length == 0)
        {
            Debug.LogError("Player Indicators aren't in the game manager. Please fill the array in");
        }
        foreach (GameObject Player in PlayerList)
        {
            if (Player.GetComponent<Player>().SelectedP_Start != "")
            {
                if (Input.GetButtonDown(Player.GetComponent<Player>().SelectedP_Start))
                {
                    GameisPaused = !GameisPaused;
                    HudManager.GetComponent<HUD_Manager>().PauseEvent(GameisPaused);
                }
            }

            PlayerIndicator[index].transform.position = new Vector3(Player.transform.position.x,(Player.transform.position.y + 10),Player.transform.position.z);
            index++;
        }
    }


    void SpawnPlayer(int index)
    {
        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPoints array in Game Manager has no Spawn Points. Please fill this variable in with game objects");
        }
        bool freespace = false;
        int SpawnPointIndex = 0;
        while (!freespace)
        {
            SpawnPointIndex = Random.Range(0, SpawnPoints.Length);
            if (!SpawnPoints[SpawnPointIndex].GetComponent<SpawnPoints>().SpaceIsOccupied)
            {
                freespace = true;
            }

        }
        PlayerList[index] = Instantiate(PlayerList[index], SpawnPoints[SpawnPointIndex].transform.position,Quaternion.identity);
    }

    
    void SetPlayerOptions(PlayerOptions playeroptions, int index)
    {
        if (PlayerList[index].GetComponent<Player>() != null)
        {
            PlayerList[index].GetComponent<Player>().Character = playeroptions.PlayerCharacter;
            PlayerList[index].GetComponent<Player>().Current_Player = playeroptions.Current_Player;
            //PlayerList[index].GetComponent<Player>().Current_Player = Controller.current_player.Player_1;
            PlayerList[index].GetComponent<Player>().InternalPlayerIndex = index;
            PlayerList[index].GetComponent<Player>().team = playeroptions.Team;
            PlayerList[index].tag = "Player" + index;

            PlayerList[index].gameObject.transform.GetChild(0).tag = "Player" + index;
            PlayerList[index].gameObject.transform.GetChild(1).tag = "Player" + index;
            PlayerList[index].gameObject.transform.GetChild(2).tag = "Player" + index;
            foreach (Transform child in PlayerList[index].gameObject.transform)
            {
                child.gameObject.tag = "Player" + index;
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.tag = "Player" + index;
                    foreach (Transform greatgrandChild in grandChild)
                    {
                        greatgrandChild.gameObject.tag = "Player" + index;
                    }
                }
                    
            }

        }
        else
        {
            //Bot options go here
        }

    }
    
    void DeathWatch()
    {
        int index = 0;
        if (PlayerList[0] == null)
        {
            return;
        }
        foreach (var player in PlayerList)
        {
            
            if (player.GetComponent<Player>() != null)
            {
                if (player.GetComponent<Player>().readytorespawn)
                {
                    UpdateScores(player.GetComponent<Player>().LastPlayerHit,player.GetComponent<Player>().team,player.GetComponent<Player>().LastPlayerHitTeam);
                    PlayerList[index].GetComponent<Player>().Death();
                    if (GameMode != gamemode.OttomanEmpire)
                    {
                        PlayerList[index] = (GameObject)Resources.Load("prefabs/player", typeof(GameObject));
                        SetPlayerOptions(PlayersOptions[index], index);
                        SpawnPlayer(index);                    
                        //Here lies the final resting place of the worlds most stress inducing foreach escape
                    }

                }

            }
            else
            {
                //Bot Respawning goes here
            }
            index++;

        }

    }
    void UpdateScores(string Striker,int StrikeeTeam,int StrikersTeam)
    {


        if (GameMode == gamemode.DeathMatch)
        {
            switch (Striker)
            {
                case("Player0"):
                    score[0] += 1;
                    break;
                case ("Player1"):
                    score[1] += 1;
                    break;
                case ("Player2"):
                    score[2] += 1;
                    break;
                case ("Player3"):
                    score[3] += 1;
                    break;

            }

        }

        if (GameMode == gamemode.TeamDeathMatch)
        {
            if (Striker == "Player0" || Striker == "Player1" || Striker == "Player2" || Striker == "Player3")
            {
                if (StrikersTeam != StrikeeTeam && StrikersTeam < (teamscores.Length -1))
                {
                    teamscores[StrikersTeam] += 1;
                }
            }
        }

    }
    public void StoreHitMagnitude(float magnitude,int playerindex,string otherplayertag)
    {
        if (magnitude < largesthit.magnitude)
        {
            return;
        }
        string player = "Player" + playerindex;
        largesthit.magnitude = magnitude;
        largesthit.player = player;
        largesthit.otherplayer = otherplayertag;
    }

}