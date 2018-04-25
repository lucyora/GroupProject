using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {

    public float throwForce = 40f;
    public GameObject grenadePrefab;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            ThrowGrenade();
        }
	}

    void ThrowGrenade()
    {
        GameObject grenade = (GameObject)Instantiate(grenadePrefab, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        
        //Throwing Audio
        //audio.Play();
    }
}
