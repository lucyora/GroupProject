using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public ParticleSystem BloodSpray;
    public Transform target;

    //GameObject Player;
    //GameObject[] Monsters;

	// Use this for initialization
	void Start ()
    {
        //Player = GameObject.Find("Player");
        //Monsters = GameObject.FindGameObjectsWithTag("Monster");
        BloodSpray.Stop();
    }

    // Update is called once per frame
    void Update ()
    {
        //this.enabled = true;
        //transform.position = GameObject.Find("Player").transform.position;
        transform.position = target.position;
    }
}
