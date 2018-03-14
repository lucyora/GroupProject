using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raycast {
    public enum character {Jenny,Steve,Gretchen,Bubba};
    public character Character;
    [HideInInspector] public bool isAlive;
    public float Strength;//TODO. Implement This
    public float Mass;
    public float MaxSpeed;
    public Vector3 CenterofGravity;
    public bool detect;
    public GameObject[] SolidCharacters;
    public GameObject[] RagdollCharacters;
    private float rotationoffsetx;
    private float rotationoffsetz;
    private float rotationincrement = 0.001f;
    public double storedrotation;

    // Use this for initialization
    void Start () {
        SolidCharacters[(int)Character].SetActive(true);
        GetComponent<Rigidbody>().mass = Mass;
        GetComponent<Rigidbody>().centerOfMass = CenterofGravity;
        InitController();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            isAlive = isOBJAlive();
            UpdatePosition();
        }
        else
        {
            SolidCharacters[(int)Character].SetActive(false);
            RagdollCharacters[(int)Character].SetActive(true);
        }
    }

    public virtual void UpdatePosition()
    {
        /////TODO Cheat rotation speed by not checking every frame?
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_RX), 2) + Math.Pow(Input.GetAxis(SelectedP_RY), 2)), 0) != 0)
        {
            //If the distance between 0,0 and the joysticks current axis does not equal 0 set a new rotation
            storedrotation = Math.Atan2(-Input.GetAxis(SelectedP_RY), Input.GetAxis(SelectedP_RX)) * 180 / Math.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, transform.eulerAngles.z);
        }

        //Camera must face +Z for rotation to work properly. I'm not going to keep changing the axies

        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_LX), 2) + Math.Pow(Input.GetAxis(SelectedP_LY), 2)), 0) != 0)
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp((GetComponent<Rigidbody>().velocity.x + Input.GetAxis(SelectedP_LX)), -MaxSpeed, MaxSpeed), GetComponent<Rigidbody>().velocity.y, Mathf.Clamp((GetComponent<Rigidbody>().velocity.z - Input.GetAxis(SelectedP_LY)), -MaxSpeed, MaxSpeed));
            transform.position = new Vector3((transform.position.x + (Input.GetAxis(SelectedP_LX) / MaxSpeed)), transform.position.y, (transform.position.z + (Input.GetAxis(SelectedP_LY) / MaxSpeed)));

            //Tilt control. Gives the player a chance to re orient themselves
            if (Input.GetAxis(SelectedP_LX) < 0)
            {
                rotationoffsetz = -rotationincrement;
            }
            else if (Input.GetAxis(SelectedP_LX) > 0)
            {
                rotationoffsetz = rotationincrement;
            }
            else
            {
                rotationoffsetz = 0;
            }

            if (Input.GetAxis(SelectedP_LY) < 0)
            {
                rotationoffsetx = -rotationincrement;
            }
            else if (Input.GetAxis(SelectedP_LY) > 0)
            {
                rotationoffsetx = rotationincrement;
            }
            else
            {
                rotationoffsetx = 0;
            }
            //transform.rotation = new Quaternion((transform.rotation.x + rotationoffsetx), (float)storedrotation, (transform.rotation.z + rotationoffsetz), 1.0f);
            transform.eulerAngles = new Vector3((transform.eulerAngles.x+rotationoffsetx), (float)storedrotation, (transform.eulerAngles.z+rotationoffsetz));

            ////////

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //TODO Either ignore collision with self and the floor
        if (detect)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
        }
        
    }

}
