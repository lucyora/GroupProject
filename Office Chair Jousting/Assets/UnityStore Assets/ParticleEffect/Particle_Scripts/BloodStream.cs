using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStream : MonoBehaviour
{
    public ParticleSystem BloodStreamEfect;

//    GameObject Player;
    public Transform target;

    //GameObject[] Monsters;

    // Use this for initialization
    void Start()
    {
  //      Player = GameObject.Find("Player");
        //Monsters = GameObject.FindGameObjectsWithTag("Monster");
        BloodStreamEfect.Stop();
}

// Update is called once per frame
void Update()
    {
        //this.enabled = true;
        //transform.position = GameObject.Find("Player").transform.position;
        transform.position = target.position;

}
}
