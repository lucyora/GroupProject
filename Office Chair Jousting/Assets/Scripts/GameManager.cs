using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerOptions
{
    public Player.character PlayerCharacter;
    public Controller.current_player Current_Player;
    public float Strength;
    public float Mass;
    public float SpeedLimiter;
    public Vector3 CenterOfGravity;
    public float RotationSnapRange;
    public bool isPlayer;
    public int PlayerIndex;
    public float Score;
    public PlayerOptions(bool isplayer, int playerindex, Controller.current_player current_player)
    {
        isPlayer = isplayer;
        PlayerIndex = playerindex;
        SpeedLimiter = 5;
        Current_Player = current_player;


    }
    public PlayerOptions(Player.character playercharacter, float strength, float mass, Vector3 centerofgravity, float rotationsnaprange, bool isplayer, int playerindex,float score)
    {
        PlayerCharacter = playercharacter;
        Strength = strength;
        Mass = mass;
        CenterOfGravity = centerofgravity;
        RotationSnapRange = rotationsnaprange;
        isPlayer = isplayer;
        PlayerIndex = playerindex;
        Score = score;

    }
    public PlayerOptions(Player.character playercharacter, float strength, float mass, Vector3 centerofgravity, float rotationsnaprange, bool isplayer, int playerindex)
    {
        PlayerCharacter = playercharacter;
        Strength = strength;
        Mass = mass;
        CenterOfGravity = centerofgravity;
        RotationSnapRange = rotationsnaprange;
        isPlayer = isplayer;
        PlayerIndex = playerindex;
        Score = 0.0f;

    }

}
public class GameManager : MonoBehaviour
{
    private GameObject SpawnArea;
    public List<GameObject> PlayerList;
    public List<PlayerOptions> PlayersOptions;
    /// Fill in with values from UI
    public bool Player1isHuman;
    public bool Player2isHuman;
    public bool Player3isHuman;
    public bool Player4isHuman;
    public int botcount;
    ///
    public int TotalPlayerCount;     
    public enum gamemode { DeathMatch, TeamDeathMatch, OttomanEmpire, LastManSitting };
    public gamemode GameMode;
    void Start()
    {
        SpawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
        if (Player1isHuman)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 1, Controller.current_player.Player_1));
            SetPlayerOptions(PlayersOptions[0], 0);
            SpawnPlayer(0);
            TotalPlayerCount += 1;

            if (Player2isHuman)
            {
                PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
                PlayersOptions.Add(new PlayerOptions(true, 1,Controller.current_player.Player_2));
                SetPlayerOptions(PlayersOptions[1], 1);
                SpawnPlayer(1);
                TotalPlayerCount += 1;

                if (Player3isHuman)
                {
                    PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
                    PlayersOptions.Add(new PlayerOptions(true, 1,Controller.current_player.Player_3));
                    SetPlayerOptions(PlayersOptions[2], 2);
                    SpawnPlayer(2);
                    TotalPlayerCount += 1;

                    if (Player4isHuman)
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
            PlayerList[index].GetComponent<Player>().Strength = playeroptions.Strength;
            PlayerList[index].GetComponent<Player>().SpeedLimiter = playeroptions.SpeedLimiter;
            PlayerList[index].GetComponent<Player>().CenterofGravity = playeroptions.CenterOfGravity;
            PlayerList[index].GetComponent<Player>().RotationSnapRange = playeroptions.RotationSnapRange;
            PlayerList[index].GetComponent<Player>().Score = playeroptions.Score;
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
                    //PlayerList.Remove(PlayerList[index]);
                    //Destroy(PlayerList[index]);
                    //PlayerList.Insert(index, (GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
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