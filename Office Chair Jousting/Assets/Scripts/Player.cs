﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Raycast {
    public enum character {Jenny,Steve,Gretchen,Bubba};
    public character Character;
    public bool isAlive;
    public float Strength= 0;//TODO. Implement This
    public float Mass = 0;
    public float SpeedLimiter = 100;
    public Vector3 CenterofGravity;
    public float RotationSnapRange;
    public bool detect;
    public GameObject[] SolidCharacters;
    public GameObject[] RagdollCharacters;
    private double storedrotation;
    public int InternalPlayerIndex;
    public float Score;
    private GameManager gamemanager;


    void Start ()
    {
        //gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        POWERUP powerup = new POWERUP(PlayerPrefs.GetInt("Char"+(InternalPlayerIndex+1)+"Power"));
        //Character = (character)PlayerPrefs.GetInt("Character"+ (InternalPlayerIndex + 1));
        //CharacterStats stats = new CharacterStats(Character);

        /*Strength += (powerup.Strength + stats.Strength);
        SpeedLimiter = stats.SpeedLimiter;
        SpeedLimiter -= powerup.Speed;
        RotationSnapRange += (powerup.Stability + stats.RotationSnapRange);
        */
        Debug.Log((int)Character);
        SolidCharacters[(int)Character].SetActive(true);
        GetComponent<Rigidbody>().mass = Mass;
        GetComponent<Rigidbody>().centerOfMass = CenterofGravity;
        InitController();

    }

    void Update()
    {

        if (isAlive)
        {
//            gamemanager.UpdateScore(InternalPlayerIndex,Score);
            isAlive = isOBJAlive();
            UpdatePosition();
        }
        else
        {
            SolidCharacters[(int)Character].SetActive(false);
            RagdollCharacters[(int)Character].SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            Invoke("Death",8.0f);
        }

    }

    public virtual void UpdatePosition()
    {


        //Player Movement
        //Camera must face +Z for rotation to work properly. I'm not going to keep changing the axies
        
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_LX), 2) + Math.Pow(Input.GetAxis(SelectedP_LY), 2)), 0) != 0)
        {
            transform.position = new Vector3((transform.position.x + (Input.GetAxis(SelectedP_LX) / SpeedLimiter)), transform.position.y, (transform.position.z + (Input.GetAxis(SelectedP_LY) / SpeedLimiter)));         
        }
        Debug.Log(SelectedP_LX);
        //Joust Rotation
        /////TODO Cheat rotation speed by not checking every frame?
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_RX), 2) + Math.Pow(Input.GetAxis(SelectedP_RY), 2)), 0) != 0)
        {
            //If the distance between 0,0 and the joysticks current axis does not equal 0 set a new rotation
            storedrotation = Math.Atan2(-Input.GetAxis(SelectedP_RY), Input.GetAxis(SelectedP_RX)) * 180 / Math.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, transform.eulerAngles.z);
        }


        //Tilt Correction
        if (Math.Round(transform.eulerAngles.x) != 0 || Math.Round(transform.eulerAngles.z) != 0)
        {
            if (Math.Round(Input.GetAxis(SelectedP_LX)) !=0)
            {
                //transform.eulerAngles = new Vector3((transform.eulerAngles.x + Input.GetAxis(SelectedP_LX)), (float)storedrotation,transform.eulerAngles.z); 
                transform.Rotate(new Vector3(Input.GetAxis(SelectedP_LX), 0.0f, 0.0f), Space.World);
            }
            if (Math.Round(Input.GetAxis(SelectedP_LY)) != 0)
            {
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, (transform.eulerAngles.z - Input.GetAxis(SelectedP_LY))); 
                transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis(SelectedP_LY)), Space.World);
            }
        }

        //Snapping Tilt when angles are within range
        if ((transform.eulerAngles.x < RotationSnapRange) || ((-RotationSnapRange) > transform.eulerAngles.x))
        {
            transform.eulerAngles = new Vector3(0.0f, (float)storedrotation, transform.eulerAngles.z);
            //transform.Rotate(new Vector3(0.0f, (float)storedrotation, transform.eulerAngles.z),Space.World);
        }
        if ((transform.eulerAngles.z < RotationSnapRange) || ((-RotationSnapRange) > transform.eulerAngles.z))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, 0.0f);
            //transform.Rotate(new Vector3(transform.eulerAngles.x, (float)storedrotation, 0.0f), Space.World);
        }


    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //TODO ignore collision with self and the floor for this value
        if (detect)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
        }
        
    }

}
