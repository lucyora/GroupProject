using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float delay = 3f;
    public float radius = 5f;
    public float force = 500f;

    public GameObject explosionEffect;

    float fCountdown;
    bool bHasExploded = false;

	// Use this for initialization
	void Start () {
        fCountdown = delay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        fCountdown -= Time.deltaTime;
        if( fCountdown <= 0f && !bHasExploded)
        {
            Explode();
            bHasExploded = true;
        }
    }
    void Explode()
    {
        //1. Effect
        //2. Add force, Damage
        //3. Remove grenade

        GameObject GrenadeExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[]  collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in collidersToDestroy)
        {
            //Damage


            //Destruction effect
            Destructible destruction = nearbyObject.GetComponent<Destructible>();
            if(destruction != null)
            {
                destruction.Destroy(); //Check
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

        }

        Destroy(gameObject);
        Destroy(GrenadeExplosion, 3.8f);

        Debug.Log("BAAAAAAM!!");
    }
}
