using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChair : MonoBehaviour {
    private Rigidbody rb;
    //public Transform Wheel1;
    //public Transform Wheel2;
    //public Transform Wheel3;
    //public Transform Wheel4;
    //public Transform Wheel5;
    public float torque;
    public float speed;
    private float velocitycap = 100.0f;

    public float steerAngle { get; private set; }

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        steerAngle = Input.GetAxis("Horizontal") ;

        //rb.AddForce(transform.forward * speed);
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0.0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);
        //GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp((GetComponent<Rigidbody>().velocity.x - Input.GetAxis("Vertical")), -velocitycap, velocitycap), GetComponent<Rigidbody>().velocity.y, Mathf.Clamp((GetComponent<Rigidbody>().velocity.z - Input.GetAxis("Horizontal")), -velocitycap, velocitycap));

        //Wheel1.localEulerAngles = new Vector3(Wheel1.localEulerAngles.x, steerAngle - Wheel1.localEulerAngles.z, Wheel1.localEulerAngles.z);
    }
}
