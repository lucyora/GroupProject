using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject destroyedVersion;  //shttered version 

    public void Destroy()
    {
        //Spawn destruction effect
        Instantiate(destroyedVersion, transform.position, transform.rotation);

        //Remove the current object
        Destroy(gameObject);
    }
}
