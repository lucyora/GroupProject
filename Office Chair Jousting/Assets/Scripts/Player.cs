using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Raycast {
    public enum character {Jenny,Steve,Gretchen,Bubba};
    public character Character;
    public bool isAlive;
    int Gender;
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
    public bool DebugMode;
    private GameManager gamemanager;
    private HUD_Manager hud_Manager;
    public bool TiltCorrection;
    public string LastPlayerHit;
    private GameOverManager gameOverManager;
    //public AudioSource maleScreams;



    void Start ()
    {
        GameObject Camera = GameObject.FindGameObjectWithTag("Camera");
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        POWERUP powerup = new POWERUP(PlayerPrefs.GetInt("Char"+(InternalPlayerIndex+1)+"Power"));
        //maleScreams = GameObject.Find("MaleScream").GetComponent<AudioSource>();
        int charint = PlayerPrefs.GetInt("Character"+ (InternalPlayerIndex + 1));
        if (charint < 0)
        {
            charint = 0;
            Debug.LogWarning("Character index was set below 0");
        }
        else if (charint > 3)
        {
            charint = 3;
            Debug.LogWarning("Character index was set above 3. It cannot be above 3");

        }
        Character = (character)(charint);
        CharacterStats stats = new CharacterStats(Character);
        Strength += (powerup.Strength + stats.Strength);
        SpeedLimiter = stats.SpeedLimiter;
        Gender = stats.Gender;
        SpeedLimiter -= powerup.Speed;
        RotationSnapRange += (powerup.Stability + stats.RotationSnapRange);        
        SolidCharacters[(int)Character].SetActive(true);
        GetComponent<Rigidbody>().mass = Mass;
        GetComponent<Rigidbody>().centerOfMass = CenterofGravity;
        InitController();

    }

    void Update()
    {

        if (isAlive)
        {
            if(gamemanager.gameisover == false) {
              
                UpdatePosition();
            }
            
            isAlive = isOBJAlive();
        }
        else
        {           
            SolidCharacters[(int)Character].SetActive(false);
            RagdollCharacters[(int)Character].SetActive(true);
            
            if(!deathSoundPlayed)
            {
                if (Gender == 0)
                {
                    SoundManager.instance.maleScream3.Play();
                }
                else if (Gender == 1)
                {
                    SoundManager.instance.femaleScream1.Play();
                }
                deathSoundPlayed = true;
            }
            
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            this.gameObject.tag = "DeadPlayer";
            this.gameObject.transform.GetChild(0).tag = "DeadPlayer";
            this.gameObject.transform.GetChild(1).tag = "DeadPlayer";
            this.gameObject.transform.GetChild(2).tag = "DeadPlayer";
            Invoke("Death", 3.0f);
            //if(timesup)
            //{
            //    gameOverManager.isGameOver = true;             // Add different conditions for different game modes to end the game by setting isGameOver bool to true
            //}
            
        }

    }

    public virtual void UpdatePosition()
    {


        //Joust Rotation
        /////TODO Cheat rotation speed by not checking every frame?
        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_RX), 2) + Math.Pow(Input.GetAxis(SelectedP_RY), 2)), 0) != 0)
        {
            //If the distance between 0,0 and the joysticks current axis does not equal 0 set a new rotation
            storedrotation = Math.Atan2(-Input.GetAxis(SelectedP_RY), Input.GetAxis(SelectedP_RX)) * 180 / Math.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, transform.eulerAngles.z);
        }

        if (TiltCorrection)
        {
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

        }



    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //TODO ignore collision with self and the floor for this value
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Untagged")
        {
            LastPlayerHit = collision.gameObject.tag;
            Invoke("ClearPlayerHit", 5.0f);

        }
        
         
        if (detect)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
        }
        
    }
    private void ClearPlayerHit()
    {
        LastPlayerHit = "";
    }

}
