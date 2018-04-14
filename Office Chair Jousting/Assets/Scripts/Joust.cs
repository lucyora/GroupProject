using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joust : MonoBehaviour {
    public float Strength;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player0" || col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Player3")
        {
            col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(Strength, col.gameObject.transform.position, 3.0f,(Strength/ 5));
        }
        
    }

}
