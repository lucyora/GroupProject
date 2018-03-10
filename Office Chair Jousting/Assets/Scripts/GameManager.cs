using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private GameObject SpawnArea;
    public GameObject Player;
    public GameObject AI;
    public bool Spawn;
	// Use this for initialization
	void Start () {
        SpawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
	}
	
	// Update is called once per frame
	void Update () {
        if (Spawn)
        {
            SpawnPlayer(true);
            Spawn = false;
        }
		
	}
    void SpawnPlayer(bool isAI)
    {
        Vector3 Center = SpawnArea.GetComponent<BoxCollider>().center;
        float max = SpawnArea.GetComponent<BoxCollider>().bounds.max.x;
        Vector3 spawnposition = new Vector3(Random.Range(-max,max),0.0f,Random.Range(-max, max));
        Instantiate(AI, spawnposition,Quaternion.identity);
    }
}
