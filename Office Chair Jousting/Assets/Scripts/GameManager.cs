using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerOptions
{
    public Player.character PlayerCharacter;
    public float Strength;
    public float Mass;
    public float SpeedLimiter;
    public Vector3 CenterOfGravity;
    public float RotationSnapRange;
    public bool isPlayer;
    public int PlayerIndex;
    public float Score;
    public PlayerOptions(bool isplayer, int playerindex)
    {
        isPlayer = isplayer;
        PlayerIndex = playerindex;


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
    public List<GameObject> Players;
    public List<PlayerOptions> PlayersOptions;
    
    /// TEMP. Fill in with values from UI
    public bool Player1isHuman;
    public bool Player2isHuman;
    public bool Player3isHuman;
    public bool Player4isHuman;
    public int botcount;
    ///
    public int TotalPlayerCount;     
    public enum gamemode { DeathMatch, TeamDeathMatch, OttomanEmpire, LastManSitting };
    public gamemode GameMode;
    public bool Spawn;
    void Start()
    {
        SpawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
        if (Player1isHuman)
        {
            Players.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
            PlayersOptions.Add(new PlayerOptions(true,1));
            //SetPlayerOptions(PlayersOptions[0], ref Players[0].GetComponent<Player>());


        }
        if (Player2isHuman)
        {


        }
        if (Player3isHuman)
        {

        }
        if (Player4isHuman)
        {

        }
        //Players.Add((GameObject)Resources.Load("prefabs/player", typeof(GameObject)));
        //SetPlayerOptions(ref Player1Options, Players[0].GetComponent<Player>());
        //SpawnPlayer();

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
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPlayer(ref GameObject player)
    {
        Vector3 Center = SpawnArea.GetComponent<BoxCollider>().center;
        float max = SpawnArea.GetComponent<BoxCollider>().bounds.max.x;
        Vector3 spawnposition = new Vector3(Random.Range(-max, max), SpawnArea.transform.position.y, Random.Range(-max, max));
        Instantiate(player, spawnposition,Quaternion.identity);
    }
    void SetPlayerOptions(PlayerOptions playeroptions, ref Player player)
    {
        player.Character = playeroptions.PlayerCharacter;
        player.Strength = playeroptions.Strength;
        player.SpeedLimiter = playeroptions.SpeedLimiter;
        player.CenterofGravity = playeroptions.CenterOfGravity;
        player.RotationSnapRange = playeroptions.RotationSnapRange;
    }
}