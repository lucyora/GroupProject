using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGrav : MonoBehaviour
{      void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player1")
        {
            other.rigidbody.mass = 0.1f;
        }
        
    }
}
