using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Player_Raycast {
    public enum character {Jenny,Steve, Judith, Bubba,Harry, AI};
    [Tooltip("What character will be spawned. The value set in the inspector will be ignored if debug mode isn't enabled")]
    public character Character;
    public bool isAlive;
    public int Gender;
    public float Strength= 0;
    public float Mass = 0;
    public float SpeedLimiter = 100;
    public Vector3 CenterofGravity;
    [Tooltip("If angle x and angle z are between -RotationSnapRange and RotationSnapRange then the angle in this range will snap to 0. The higher the number, the higher the stabilty")]
    public float RotationSnapRange;
    [Tooltip("Models shown when the player is alive. The oder MUST match the Ragdoll Characters array")]
    public GameObject[] SolidCharacters;
    [Tooltip("Models shown when the player is alive. The oder MUST match the Solid Characters array")]
    public GameObject[] RagdollCharacters;
    private double storedrotation;
    [Tooltip("Used in the game manager for score keeping, options setting and respawning")]
    public int InternalPlayerIndex;
    public float Score;
    [Tooltip("Mostly decouples this prefab from the ui")]
    public bool DebugMode;
    public GameManager gamemanager;
    private HUD_Manager hud_Manager;
    [Tooltip("If angle x and angle z are between -RotationSnapRange and RotationSnapRange then the angle in this range will snap to 0. The higher the number, the higher the stabilty")]
    public bool TiltCorrection;
    public string LastPlayerHit;
    public string lastOttomanHit;
    public int LastPlayerHitTeam;
    public bool readytorespawn = false;
    private GameOverManager gameOverManager;
    public int team = 3;
    public bool IsInElevator;
    private bool deathSoundPlayed;


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
        }
        else if (charint > 5)
        {
            charint = 5;
        }
        if (!DebugMode)
        {
            Character = (character)(charint);
        } 
        CharacterStats stats = new CharacterStats(Character);
        Strength += (powerup.Strength + stats.Strength);
        SpeedLimiter = stats.SpeedLimiter;
        Gender = stats.Gender;
        SpeedLimiter -= powerup.Speed;
        RotationSnapRange += (powerup.Stability + stats.RotationSnapRange);        
        SolidCharacters[(int)Character].SetActive(true);
        GetComponent<Rigidbody>().mass = Mass;
        GetComponent<Rigidbody>().centerOfMass = CenterofGravity;
        this.gameObject.transform.GetChild(1).GetComponent<Joust>().Strength = Strength;//The joust


        InitController();

    }

    void Update()
    {

        if (isAlive)
        {
            if(gamemanager.GameIsOver == false && gamemanager.GameisPaused == false) {
              
                UpdatePosition();
                isAlive = isOBJAlive();
            }
            
           
        }
        else
        {
            SpawnRagdolls();

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
                else if (Gender == 2)
                {
                    SoundManager.instance.ottomanDeath.Play();
                }
                deathSoundPlayed = true;
            }
            
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            this.gameObject.tag = "DeadPlayer";
            this.gameObject.transform.GetChild(0).tag = "DeadPlayer";
            this.gameObject.transform.GetChild(1).tag = "DeadPlayer";
            this.gameObject.transform.GetChild(2).tag = "DeadPlayer";
            Invoke("preparetorespawn", 2.0f);
            
        }

    }

    public virtual void SpawnRagdolls()
    {
        SolidCharacters[(int)Character].SetActive(false);
        RagdollCharacters[(int)Character].SetActive(true);
    }

    public virtual void UpdatePosition()
    {


        //Player Movement
        //Camera must face +Z for rotation to work properly. I'm not going to keep changing the axies

        if (Math.Round(Math.Sqrt(Math.Pow(Input.GetAxis(SelectedP_LX), 2) + Math.Pow(Input.GetAxis(SelectedP_LY), 2)), 0) != 0)
        {
            if (!SoundManager.instance.movechair.isPlaying)
            {
                SoundManager.instance.movechair.Play();
            }
            transform.position = new Vector3((transform.position.x + (Input.GetAxis(SelectedP_LX) / SpeedLimiter)), transform.position.y, (transform.position.z + (Input.GetAxis(SelectedP_LY) / SpeedLimiter)));
        }
        else
        {
            SoundManager.instance.movechair.Stop();
        }
        //Joust Rotation
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
                    transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis(SelectedP_LX)), Space.World);
                }
                if (Math.Round(Input.GetAxis(SelectedP_LY)) != 0)
                {
                    transform.Rotate(new Vector3(Input.GetAxis(SelectedP_LY), 0.0f, 0.0f), Space.World);
                }
          

                //Snapping Tilt when angles are within range
                if ((transform.eulerAngles.x < RotationSnapRange) || ((-RotationSnapRange) > transform.eulerAngles.x))
                {
                    transform.eulerAngles = new Vector3(0.0f, (float)storedrotation, transform.eulerAngles.z);
                }
                if ((transform.eulerAngles.z < RotationSnapRange) || ((-RotationSnapRange) > transform.eulerAngles.z))
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)storedrotation, 0.0f);
                }
            }

        }



    }
    public virtual void preparetorespawn()
    {
        readytorespawn = true;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //TODO ignore collision with self and the floor for this value
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Untagged" && collision.gameObject.tag !="DeathPlane")
        {
            LastPlayerHit = collision.gameObject.tag;

            if (collision.gameObject.GetComponent<Player>() == null)
            {
                LastPlayerHitTeam = collision.transform.parent.gameObject.GetComponent<Player>().team;
            }
            else 
            {
                LastPlayerHitTeam = collision.gameObject.GetComponent<Player>().team;
            }

            Invoke("ClearPlayerHit", 5.0f);
            //gamemanager.StoreHitMagnitude(collision.relativeVelocity.magnitude,InternalPlayerIndex,LastPlayerHit);
            // one grunt for jenny
            switch (Character)
            {
                case Player.character.Bubba:
                    SoundManager.instance.maleGrunt3.Play();
                    SoundManager.instance.Hit1.Play();
                    break;

                case Player.character.Steve:
                    SoundManager.instance.maleGrunt2.Play();
                    SoundManager.instance.Hit1.Play();
                    break;

                case Player.character.Jenny:
                    SoundManager.instance.femaleGrunt1.Play();
                    SoundManager.instance.Hit1.Play();
                    break;

                case Player.character.Judith:
                    SoundManager.instance.femaleGrunt1.Play();
                    SoundManager.instance.Hit1.Play();
                    break;
            }


        }
        
         
        
        
        
    }
    private void ClearPlayerHit()
    {
        LastPlayerHit = "";
        LastPlayerHitTeam = 800;
    }

}
