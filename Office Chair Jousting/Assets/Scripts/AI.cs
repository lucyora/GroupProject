using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class AI : Player
{

    public GameObject player;
    public float speed;
    float timer = 0.0f;
    private NavMeshAgent nav;

	// Use this for initialization
	void Awake ()
    {
        //Stops any controller from being set to this player
        Current_Player = current_player.AI;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Debug.Log("I AM AWAKE");
        //nav.enabled = true;
        isAlive = true;
    }

    //add deviation stufff
    // Update is called once per frame
    public override void UpdatePosition()//Overrides the player UpdatePosition function that handles player input. Raycasting for death is handled in player.cs
    {
        timer += Time.deltaTime;

        if (player != null)
        {
            Vector3 target = player.transform.position;
            transform.LookAt(target);
        }
       //transform.rotation = Quaternion.LookRotation(direction) * offset;
        transform.position += transform.forward * speed * Time.deltaTime;
        


        if (timer > 2)
        { 
            // timer resets at 2, allowing .5 s to do the rotating
            transform.rotation *= Quaternion.Euler(transform.rotation.x, Random.Range(-180, 180), transform.rotation.z);
            timer = 0.0f;
        }


        //transform.rotation *= Quaternion.Euler(0, -90, 0);
        //Debug.Log(direction);

    }

}
