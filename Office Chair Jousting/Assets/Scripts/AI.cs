using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class AI : MonoBehaviour
{


    public static Controller controllerInstance;
    public static Raycast raycastInstance;
    public static Controller controller;
    public GameObject player;
    private Transform playerTransform;
    private NavMeshAgent nav;


	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Debug.Log("I AM AWAKE");
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if(raycastInstance.isOBJAlive() == true)
        { 
            Vector3 direction = playerTransform.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            nav.enabled = true;
            Debug.Log("I AM MOVING");
        }
        else
        {
            nav.enabled = false;
        }
	}
}
