using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pt_respawn : MonoBehaviour
{
    public ParticleSystem ef_respawn;
    public Transform target_respawn;
    // Use this for initialization
    void Start()
    {
        ef_respawn.loop = false;
        //gameObject.active = true;
        StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target_respawn.position;
    }
    IEnumerator Wait()
    {
        //print(Time.time);
        yield return new WaitForSeconds(1.5f);
        gameObject.active = false;
        ef_respawn.enableEmission = false; // turn the particle system off at startup
        ef_respawn.Stop();
    }
}
