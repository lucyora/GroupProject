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
    void OnTriggerEnter(Collider other)    // once the trigger on the collider activates 
    {
        if (other.gameObject.tag == "Player0" || other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4") // and the other object's tag is "Player"
        {
            if (other.gameObject.GetComponent<Player>() != null && !gamemanager.PlayerIsInElevator)
            {
                gamemanager.PlayerIsInElevator = true;
                player = other;
                other.gameObject.active = false;
                player.transform.position = new Vector3(1000, 1000, 1000); 
			    SoundManager.instance.elevatorDing.Play();
                Invoke("reactivateplayer", 1.0f); //currently disabled as it causes a crash if the player is in the elevator and another player is getting ready to respawn from their blow. 
                //reactivateplayer();
            }
            
        }
    }
    public void reactivateplayer()
    {
        player.gameObject.transform.position = Destination.transform.position;
        player.gameObject.active = true;
        SoundManager.instance.elevatorDing.Play();
        gamemanager.PlayerIsInElevator = false;

    }
}

