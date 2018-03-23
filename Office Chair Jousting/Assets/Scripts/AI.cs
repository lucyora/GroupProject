using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class AI : Player
{

    public GameObject player;
    public float speed;
    private NavMeshAgent nav;

	// Use this for initialization
	void Awake ()
    {
        //Stops any controller from being set to this player
        Current_Player = current_player.AI;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Debug.Log("I AM AWAKE");
        nav.enabled = true;
        isAlive = true;
    }


    //add deviation stufff
    // Update is called once per frame
    public override void UpdatePosition()//Overrides the player UpdatePosition function that handles player input. Raycasting for death is handled in player.cs
    {
        Vector3 target = player.transform.position;// this.transform.position;
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
//        Quaternion offset = Quaternion.Euler(0, -45, 0);
        transform.LookAt(target);
       //transform.rotation = Quaternion.LookRotation(direction) * offset;
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, -90, 0);
        //Debug.Log(direction);

    }

}
