using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raycast {

    bool isAlive;
    public float Strength;//Can only be implemented once we're at the point of hit detection
    public float Mass;
    public float MaxSpeed;
    public Vector3 CenterofGravity;

    //private double storedrotation;
    public double storedrotation { get; private set; }

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().mass = Mass;
        GetComponent<Rigidbody>().centerOfMass = CenterofGravity;
        InitController();
    }

    // Update is called once per frame
    void Update()
    {
        isAlive = isOBJAlive();
        if (isAlive)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_LX), 2) + Math.Pow(Input.GetAxis(SelectedP_LY), 2)), 0) != 0)
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp((GetComponent<Rigidbody>().velocity.x + Input.GetAxis(SelectedP_LX)), -MaxSpeed, MaxSpeed), GetComponent<Rigidbody>().velocity.y, Mathf.Clamp((GetComponent<Rigidbody>().velocity.z - Input.GetAxis(SelectedP_LY)), -MaxSpeed, MaxSpeed));
            transform.position = new Vector3(Mathf.Clamp((transform.position.x - (Input.GetAxis(SelectedP_LY) /MaxSpeed)), -MaxSpeed, MaxSpeed), transform.position.y, Mathf.Clamp((transform.position.z - (Input.GetAxis(SelectedP_LX)/ MaxSpeed)), -MaxSpeed, MaxSpeed));

        }
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_RX), 2) + Math.Pow(Input.GetAxis(SelectedP_RY), 2)), 0) != 0)
        {
            //If the distance between 0,0 and the joysticks current axis does not equal 0 set a new rotation
            storedrotation = Math.Atan2(-Input.GetAxis(SelectedP_RY), -Input.GetAxis(SelectedP_RX)) * 180 / Math.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, transform.eulerAngles.z);

        }                                            

    }

}
