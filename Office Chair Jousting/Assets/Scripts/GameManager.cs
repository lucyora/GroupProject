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
    [Tooltip("The length of the match in seconds for all modes except for Ottoman Empire and Last Man Sitting")]
    public float MatchTime;

    [Tooltip("0 is ai and 1 is human. ")]
    public int Player1isAI;
    [Tooltip("0 is ai and 1 is human. ")]
    public int Player2isAI;
    [Tooltip("0 is ai and 1 is human. ")]
    public int Player3isAI;
    [Tooltip("0 is ai and 1 is human. ")]
    public int Player4isAI;

    [Tooltip("An array for each players score. Index 0 is Player 1's score")]
    public float[] score;
    [Tooltip("An array for all team scores. Set as many teams as you need")]
    public int[] teamscores;
    [Tooltip("Ottoman Game Score based on time")]
    public float ottomanscores;
    [Tooltip("Ottoman Game Score based on Ottoman Kills")]
    public int ottomanKillScore;
    public LargestHit largesthit = new LargestHit();

    [Tooltip("Mostly decouples the game scene from the menu's")]
    public bool DebugMode;
    public GameObject HudManager;
    [Tooltip("A list of points where players can spawn. Each object needs SpawnPoints.cs attached")]
    public GameObject[] SpawnPoints;
    [Tooltip("The UI elemts that hover over players during the game")]
    public GameObject[] PlayerIndicator;
    [Tooltip("All current active players")]
    public GameObject[] TeamIndicator;
    public List<GameObject> PlayerList;
    [Tooltip("Ottoman")]
    public GameObject Ottoman;
    [Tooltip("Storage for the players set options.")]
    public List<PlayerOptions> PlayersOptions;
    public bool GameIsOver = false;
    public bool GameisPaused = false;
    public int botcount;
    public int TotalPlayerCount;
    [Tooltip("Disables the elevator while a player is inside. Does nothing if there are no elevators")]
    public bool PlayerIsInElevator;

	//----------------------------------------------------
	//Ottoman related variables
	//----------------------------------------------------
	private bool waveSpawn = false;
	public bool spawn = true;
	public int totalWaves = 5;
	private int numWaves = 0;
	public int totalEnemy = 10;
	public int numEnemy = 0;
	private int spawnedEnemy = 0;
	public float waveTimer = 30.0f;
	public float timeTillWave = 0.0f;
	//----------------------------------------------------

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


        if (Player1isAI == 1)// If player 1 is a human
        {

            PlayersOptions.Add(new PlayerOptions(true, 0, Player_GameController.current_player.Player_1,PlayerPrefs.GetInt("Player1Team")));// TO DO. Change the last function parameter to be the proper team index for the player
            //Stores player options. Actual player objects are often deleted and replaced so anything that needs to stay through the match and isn't set through playerprefs should be stored in the PlayerOptions class
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));//A new player prefab is set inside the PlayerList arraylist
            SetPlayerOptions(PlayersOptions[0], 0);// Setting the player options to the instantiated player prefab
            SpawnPlayer(0); 
            TotalPlayerCount += 1;
            
        }
        //Human Players 2-4 are handled identically but with their proper values
        if (Player2isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 1, Player_GameController.current_player.Player_2, PlayerPrefs.GetInt("Player2Team")));
            SetPlayerOptions(PlayersOptions[1], 1);
            SpawnPlayer(1);
            TotalPlayerCount += 1;
        }
        if (Player3isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 2, Player_GameController.current_player.Player_3, PlayerPrefs.GetInt("Player3Team")));
            SetPlayerOptions(PlayersOptions[2], 2);
            SpawnPlayer(2);
            TotalPlayerCount += 1;
        }
        if (Player4isAI == 1)
        {
            PlayerList.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true, 3, Player_GameController.current_player.Player_4,PlayerPrefs.GetInt("Player4Team")));
            SetPlayerOptions(PlayersOptions[3], 3);
            SpawnPlayer(3);
            TotalPlayerCount += 1;
        }

        //This is set in the inspector is this script is in debug mode
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

        InvokeRepeating("DeathWatch", 0.01f, 1.0f);//Watching for player deaths
    }


    void Update()
    {
        bool allplayersdead = true;
        int index = 0;
		if (PlayerIndicator.Length == 0)
        {
            Debug.LogError("Player Indicators aren't in the game manager. Please fill the array in");
        }
        foreach (GameObject Player in PlayerList)
        {
            if (Player.GetComponent<Player>().isAlive)
            {
                allplayersdead = false;
            }

            //Pausing the game
		if (Input.GetButtonDown(Player.GetComponent<Player>().SelectedP_Start))
		{
		    GameisPaused = !GameisPaused;
		    HudManager.GetComponent<HUD_Manager>().PauseEvent(GameisPaused);
		}
            
            //
            if (GameMode != gamemode.TeamDeathMatch && Player.GetComponent<Player>().isAlive)
            {
                PlayerIndicator[index].transform.position = new Vector3(Player.transform.position.x, (Player.transform.position.y + 10), Player.transform.position.z);//Setting the player indicators to float just above their respective players.
            }
            else
            {
                PlayerIndicator[index].transform.position = new Vector3(1000, 1000, 1000);
            }

            index++;
        }



        if (GameMode == gamemode.TeamDeathMatch)
        {
            if (PlayerList[0].GetComponent<Player>().team == 0)
            {
                TeamIndicator[0].transform.position = new Vector3(PlayerList[0].transform.position.x, PlayerList[0].transform.position.y + 10, PlayerList[0].transform.position.z);
            }
            else
            {
                TeamIndicator[1].transform.position = new Vector3(PlayerList[0].transform.position.x, PlayerList[0].transform.position.y + 10, PlayerList[0].transform.position.z);
            }


            if (PlayerList[1].GetComponent<Player>().team == 0)
            {
                TeamIndicator[2].transform.position = new Vector3(PlayerList[1].transform.position.x, PlayerList[1].transform.position.y + 10, PlayerList[1].transform.position.z);
            }
            else
            {
                TeamIndicator[3].transform.position = new Vector3(PlayerList[1].transform.position.x, PlayerList[1].transform.position.y + 10, PlayerList[1].transform.position.z);
            }


            if (PlayerList[2].GetComponent<Player>().team == 0)
            {
                TeamIndicator[4].transform.position = new Vector3(PlayerList[2].transform.position.x, PlayerList[2].transform.position.y + 10, PlayerList[2].transform.position.z);
            }
            else
            {
                TeamIndicator[5].transform.position = new Vector3(PlayerList[2].transform.position.x, PlayerList[2].transform.position.y + 10, PlayerList[2].transform.position.z);
            }


            if (PlayerList[3].GetComponent<Player>().team == 0)
            {
                TeamIndicator[6].transform.position = new Vector3(PlayerList[3].transform.position.x, PlayerList[3].transform.position.y + 10, PlayerList[3].transform.position.z);
            }
            else
            {
                TeamIndicator[7].transform.position = new Vector3(PlayerList[3].transform.position.x, PlayerList[3].transform.position.y + 10, PlayerList[3].transform.position.z);
            }
        }

        if (allplayersdead && GameMode == gamemode.OttomanEmpire)
        {
            GameIsOver = true;
            HudManager.GetComponent<HUD_Manager>().endthegame();
        }
    }


    void SpawnPlayer(int index)
    {
        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPoints array in Game Manager has no Spawn Points. Please fill this variable in with game objects");
        }
        //Checking for a spawn point with free space
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
        PlayerList[index] = Instantiate(PlayerList[index], SpawnPoints[SpawnPointIndex].transform.position,Quaternion.identity);//Replacing the current player object with a fresh version
    }

    void SpawnOttoman()
    {
        Debug.Log("spawn enemies" + numEnemy + " DeltaTime:" + Time.time);
        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPoints array in Game Manager has no Spawn Points. Please fill this variable in with game objects");
        }
        //Checking for a spawn point with free space
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
        Debug.Log("spawned enemies" + numEnemy + " DeltaTime:" + Time.time);
        // Increase the total number of enemies spawned and the number of spawned enemies
        numEnemy++;
		spawnedEnemy++;
		Ottoman = Instantiate(Ottoman, SpawnPoints[SpawnPointIndex].transform.position, Quaternion.identity);//Replacing the current player object with a fresh version
    }




    void SetPlayerOptions(PlayerOptions playeroptions, int index)
    {
        if (PlayerList[index].GetComponent<Player>() != null)
        {
            PlayerList[index].GetComponent<Player>().Character = playeroptions.PlayerCharacter;
            PlayerList[index].GetComponent<Player>().Current_Player = playeroptions.Current_Player;
            PlayerList[index].GetComponent<Player>().InternalPlayerIndex = index;
            PlayerList[index].GetComponent<Player>().team = playeroptions.Team;

            //Setting portions of the player object to have a proper player tag. This is used in hit detection
            PlayerList[index].tag = "Player" + index;
            PlayerList[index].gameObject.transform.GetChild(0).tag = "Player" + index;
            PlayerList[index].gameObject.transform.GetChild(1).tag = "Player" + index;
            PlayerList[index].gameObject.transform.GetChild(2).tag = "Player" + index;

            //Setting the children and great granchildren to have the proper player tag.
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
        if(GameMode == gamemode.OttomanEmpire && PlayerList.Count == 0)
        {
            GameIsOver = true;
        }
        if (PlayerList[0] == null)
        {
            return;
        }
        timeTillWave += 1;
        //Checking for death in each object in the PlayerList
        foreach (var player in PlayerList)
        {
            
            if (player != null && player.GetComponent<Player>() != null)
            {
                //Player object is confirmed to be a human
                if (player.GetComponent<Player>().readytorespawn)
                {
                    
                    //We're not respawning the player in ottoman


                    if (GameMode != gamemode.OttomanEmpire)
                    {
                        UpdateScores(player.GetComponent<Player>().LastPlayerHit,player.GetComponent<Player>().team,player.GetComponent<Player>().LastPlayerHitTeam);
                        PlayerList[index].GetComponent<Player>().Death();
                        PlayerList[index] = (GameObject)Resources.Load("prefabs/player", typeof(GameObject));
                        SetPlayerOptions(PlayersOptions[index], index);
                        SpawnPlayer(index);

                    }

                }
            }
            index++;
        }

		if (GameMode == gamemode.OttomanEmpire)
		{
			Ottoman = (GameObject)Resources.Load("prefabs/Ottoman", typeof(GameObject));
            if (timeTillWave >= waveTimer)
            {
                Debug.Log("time till wave");
                // enables the wave spawner
                waveSpawn = true;
                spawn = true;
                // sets the time back to zero
                timeTillWave = 0.0f;
                // increases the number of waves

                // Get it to spawn the same number of enemies regardless of how many have been killed
                numEnemy = 0;
                numWaves++;
            }

            if (spawn)
			{
				// checks to see if the number of spawned ottoman is less than the max num of enemies
				if (numEnemy <= totalEnemy)
				{
					// spawns an ottoman
					SpawnOttoman();
                    SpawnOttoman();
                }
                // checks to see if the overall spawned num of ottoman is more or equal to the total to be spawned
                if (spawnedEnemy >= totalEnemy * (numWaves+1) )
				{
					//sets the spawner to false
					spawn = false;
				}

				if (numWaves < totalWaves + 1)
				{
					if (numEnemy == 0 && !waveSpawn)
					{
						// enables the wave spawner
						waveSpawn = true;
						//increase the number of waves
						numWaves++;
					}
					if (numEnemy == totalEnemy)
					{
						// disables the wave spawner
						waveSpawn = false;
					}
				}
                //Debug.Log("Number of waves, "+numWaves);
                ///Debug.Log("Number of waves "+ totalWaves);

                if (numWaves <= totalWaves)
				{
					// Increases the timer to allow the timed waves to work
					//timeTillWave += Time.deltaTime;
                    Debug.Log("Wave timer");
					if (waveSpawn)
					{
						//spawns an enemy
						SpawnOttoman();
                    }

					if (numEnemy >= totalEnemy)
					{
						// diables the wave spawner
						waveSpawn = false;
					}
				}
				else
				{
					spawn = false;
				}

				//for(int i = 0; i < ottomanCount; i++)
				//{
				//if (Ottoman.GetComponent<AI>() != null)
				//{
				//	if (Ottoman.GetComponent<AI>().readytorespawn)
				//	{
				//		Debug.Log("Inside deepest if statement ready to respawn");

				//		Ottoman.GetComponent<AI>().Death();
				//	}
				//	Debug.Log("Spawning? Should be");
				//	ottomanKillScore += 10;
				//	Ottoman = (GameObject)Resources.Load("prefabs/Ottoman", typeof(GameObject));
				//	SpawnOttoman();
				//}

				//}
			}
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
                if (StrikersTeam != StrikeeTeam && StrikersTeam < (teamscores.Length -1))//If the player who killed the player we're currently respawning isn't on the same team and the team the killers team actually exists...
                {
                    teamscores[StrikersTeam] += 1;
                }
            }
        }

    }
    public void StoreHitMagnitude(float magnitude,int playerindex,string otherplayertag)
    {
        if (magnitude < largesthit.magnitude)//Don't store magnitude if the hit is smaller than the previous
        {
            return;
        }
        string player = "Player" + playerindex;
        largesthit.magnitude = magnitude;
        largesthit.player = player;
        largesthit.otherplayer = otherplayertag;
    }

}
