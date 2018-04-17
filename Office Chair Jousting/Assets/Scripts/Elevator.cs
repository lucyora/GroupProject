using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform Destination;
    public Collider player;
    private GameManager gamemanager;
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter(Collider other)   
    {
        //First I'm checking if the collided object is an alive player. Dead players will have the tag "DeadPlayer"
        if (other.gameObject.tag == "Player0" || other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            if (other.gameObject.GetComponent<Player>() != null && !gamemanager.PlayerIsInElevator)
            {
                gamemanager.PlayerIsInElevator = true;// This variable is used so the other elevator knows not to transport someone until the other player reaches their destination
                player = other; // Storing the player for when we invoke the function to bring the player to their destination
                other.gameObject.active = false; //I'm setting the player to be inactive so physics don't mess with the player once they reach their destination
                player.transform.position = new Vector3(1000, 1000, 1000); //Setting the player somewhere out of the cameras viewable range. If the player isn't moved outside of the cameras range the player icon will continue to be on screen.
			    SoundManager.instance.elevatorDing.Play();
                Invoke("reactivateplayer", 1.0f); 
                
            }
            
        }
    }
    //Re-activating the player
    public void reactivateplayer()
    {
        player.gameObject.transform.position = Destination.transform.position;
        player.gameObject.active = true;
        SoundManager.instance.elevatorDing.Play();
        gamemanager.PlayerIsInElevator = false;

    }
}

