using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform Destination;
     void OnTriggerEnter(Collider other)    // once the trigger on the collider activates 
    {
        if (other.gameObject.tag == "Player") // and the other object's tag is "Player"
        {
            other.gameObject.transform.position = Destination.transform.position; // set the player's position to the destination item's.
            other.gameObject.transform.rotation = Destination.transform.rotation; // set the player's position to the destination item's.
        }
    }
}

