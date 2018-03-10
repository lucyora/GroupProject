using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class AI : Player
{

    public GameObject player;
    private NavMeshAgent nav;

	// Use this for initialization
	void Awake ()
    {
        Current_Player = current_player.AI;//Stops any controller from being set to this player
        //player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Debug.Log("I AM AWAKE");
        nav.enabled = true;
        isAlive = true;
    }
	
	// Update is called once per frame
	public override void UpdatePosition()//Overrides the player UpdatePosition function that handles player input. Raycasting for death is handled in player.cs
    {
            Vector3 direction = player.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            //Debug.Log("I AM MOVING");
	}
}
