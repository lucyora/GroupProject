using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour {

    public int power1RequiredCoins = 100;                   // required amount of coins 
    public int power2RequiredCoins = 200;
    public int power3RequiredCoins = 300;
    public int power4RequiredCoins = 400;
    public int power5RequiredCoins = 500;
    public int playerCoins = 0;                             // coins collected by hitting someone will be stored in this variable
    private bool power1Unlocked = false;                    // boolean for power up unlocks
    private bool power2Unlocked = false;
    private bool power3Unlocked = false;
    private bool power4Unlocked = false;
    private bool power5Unlocked = false;
    public float strength;
    public float speed;
    public float stability;
    public Button ToMap_btn;
    public Button ToGameMode_btn;
    public Image image;
    public Image powerUp;
    public Text bottxt;
    public Sprite[] sprite;
    public Sprite[] powersprite;
    public Slider[] slider;
    public GameObject panel;
    public RectTransform charpanel;
    public GameObject readyPanel;
    public TextMeshProUGUI powerstats;
    public TextMeshProUGUI powerName;
    public TextMeshProUGUI StartText;

    public int botCount = 0;
    private float time;
    public int[] index;
    public int[] powerIndex;
    public string[] names;
    public int[] IsPlayer;
    public bool[] characterChosen;
    public bool[] powerChosen;
    public bool[] playerReady;
    Animator menuAnim;
    public bool toMap = false;

    void Start()
    {
        bottxt = GameObject.Find("PostItBotText").GetComponent<Text>();
        bottxt.text = "Employee Count " +botCount;
        menuAnim = GetComponent<Animator>();
        names = new string[4];      names = Input.GetJoystickNames();
        index = new int[4] { 1, 1, 1, 1 };
        powerIndex = new int[4] { 1, 1, 1, 1, };
        IsPlayer = new int[4] { 1, 0, 0, 0 };
        characterChosen = new bool[4] { false, false, false, false };
        powerChosen = new bool[4] { false, false, false, false };
        playerReady = new bool[4] { false, false, false, false };
        CharacterDisplay(1);
        powerindex(1);
        Characterstats();
    }

    //stats to slider
    void Characterstats()
    {
        slider[0].value = strength / 100;
        slider[1].value = speed / 100;
        slider[2].value = stability / 100;
    }
    //character index
    void CharacterDisplay(int i)
    {
        if (index[i] ==1)
        {
            character1Stats();
        }
        else if (index[i] == 2)
        {
            character2Stats();
        }
        else if (index[i] == 3)
        {
            character3Stats();
        }
        else if (index[i] == 4)
        {
            character4Stats();
        }
        else if (index[i] == 5)
        {
            character5Stats();
        }
    }
    //Character Selection Screen Player cycles through characters and powers
   public void playerControl(int i)
    {
        time += Time.deltaTime;
        if (time > 0.1)
        {
            time = 0;
            //character select move left and right
            if (Input.GetAxis("Joy" + i + "XL") > 0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == false)
            {
				SoundManager.instance.selectSound.Play();
                menuAnim.Play("CharacterMoveRight");
                index[i]++;
                if (index[i] > 5)
                {
                    index[i] = 1;
                }
                CharacterDisplay(i);
                Characterstats();
            }
            else if((Input.GetAxis("Joy0YL") > 0.99F))
            {
                SoundManager.instance.selectSound.Play();
                botCount += 1;
                if(botCount >= 9)
                {
                    botCount = 9;
                }
                bottxt.text = "Employee Count " + botCount;
            }
            else if ((Input.GetAxis("Joy0YL") < -0.99F))
            {
                SoundManager.instance.selectSound.Play();
                botCount -= 1;
                if(botCount <= 0)
                {
                    botCount = 0;
                }
                bottxt.text = "Employee Count " + botCount;
            }
            else if (Input.GetAxis("Joy" + i + "XL") < -0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == false)
            {
                SoundManager.instance.selectSound.Play();
                menuAnim.Play("CharacterMoveLeft");
                index[i]--;
                if (index[i] < 1)
                {
                    index[i] = 5;
                }
                CharacterDisplay(i);
                Characterstats();
            }
            //power select move left and right
            if (Input.GetAxis("Joy" + i + "XL") > 0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                SoundManager.instance.selectSound.Play();
                menuAnim.Play("PowerMoveRight");
                powerIndex[i]++;
                if (powerIndex[i] > 6)
                {
                    powerIndex[i] = 1;
                }
                powerindex(i);
            }
            else if (Input.GetAxis("Joy" + i + "XL") < -0.2F && panel == GameObject.Find("Character" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                SoundManager.instance.selectSound.Play();
                menuAnim.Play("PowerMoveLeft");
                powerIndex[i]--;
                if (powerIndex[i] < 1)
                {
                    powerIndex[i] = 6;
                }
                powerindex(i);
            }
        }
    }
    //Character Selection Screen Player chooses character and power
    public void checkPlayerReady(int i)
    {
        if (panel == GameObject.Find(("Character" + i)))
        {
            //select playable character
            if (Input.GetButtonDown("JoyA" +i ) && characterChosen[i] == false && powerChosen[i] == false)
            {
				SoundManager.instance.confirmSound.Play();
                menuAnim.Play("CharacterAPress");
                characterChosen[i] = true;
                image.color = Color.grey;
            }
            //back to select playable character
            else if (Input.GetButtonDown("JoyB" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                characterChosen[i] = false;
                menuAnim.Play("CharacterIdle");
				SoundManager.instance.deselectSound.Play();
                image.color = Color.white;
            }
            //select power
            else if (Input.GetButtonDown("JoyA" + i) && characterChosen[i] == true && powerChosen[i] == false)
            {
                if (powerIndex[i] == 1 && playerCoins >= 0) // first power up will be unlocked by default
                {
                    powerUp.sprite = powersprite[0];        // loads powerup 0 sprite
                    powerChosen[i] = true;                  // changes boolean for future reference
                    powerUp.color = Color.grey;             // visual feedback upon selection
					SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                    strength = 10.0f;
                    speed = -30.0f;
                    stability = 20.0f;

				}
				else if (powerIndex[i] == 2)
                {
                    if (power1Unlocked)                     // carry out following segment of code if power is already unlocked and ignores the rest
                    {
                        powerUp.sprite = powersprite[1];
                        powerChosen[i] = true;
                        powerUp.color = Color.grey;
						SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                        strength = 30.0f;
                        speed = -30.0f;
                        stability = 30.0f;
                    }
					else
                    {
                        if (playerCoins >= power1RequiredCoins)     // if player has more coins than required, it unlocks powerup
                        {
                            playerCoins -= 100;
                            powerUp.sprite = powersprite[1];
                            power1Unlocked = true;
                            powerChosen[i] = true;
                            powerUp.color = Color.grey;
							SoundManager.instance.confirmSound.Play();// audio feedback upon selection
						}                                           // if not shows visual message with audio feedback
						else
                        {
                            powerUp.sprite = powersprite[11];
                            powerUp.color = Color.white;
							SoundManager.instance.deselectSound.Play();
						}
					}  
                }
                // Following code works same as above
                else if (powerIndex[i] == 3)
                {
                    if (power2Unlocked)
                    {
                        powerUp.sprite = powersprite[2];
                        powerChosen[i] = true;
                        powerUp.color = Color.grey;
						SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                        strength = 0.0f;
                        speed = 20.0f;
                        stability = 30.0f;
                    }
					else
                    {
                        if (playerCoins >= power2RequiredCoins)
                        {
                            playerCoins -= 200;
                            powerUp.sprite = powersprite[2];
                            power2Unlocked = true;
                            powerChosen[i] = true;
                            powerUp.color = Color.grey;
							SoundManager.instance.confirmSound.Play();// audio feedback upon selection
						}
						else
                        {
                            powerUp.sprite = powersprite[11];
                            powerUp.color = Color.white;
							SoundManager.instance.deselectSound.Play();
						}
					}
                }
                else if (powerIndex[i] == 4)
                {
                    if (power3Unlocked)
                    {
                        powerUp.sprite = powersprite[3];
                        powerChosen[i] = true;
                        powerUp.color = Color.grey;
						SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                        strength = 0.0f;
                        speed = 30.0f;
                        stability = -20.0f;
                    }
					else
                    {
                        if (playerCoins >= power3RequiredCoins)
                        {
                            playerCoins -= 300;
                            powerUp.sprite = powersprite[3];
                            power3Unlocked = true;
                            powerChosen[i] = true;
                            powerUp.color = Color.grey;
							SoundManager.instance.confirmSound.Play();// audio feedback upon selection
						}
						else
                        {
                            powerUp.sprite = powersprite[11];
                            powerUp.color = Color.white;
							SoundManager.instance.deselectSound.Play();
						}
					}
                }
                else if (powerIndex[i] == 5)
                {
                    if (power4Unlocked)
                    {
                        powerUp.sprite = powersprite[4];
                        powerChosen[i] = true;
                        powerUp.color = Color.grey;
						SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                        strength = 10.0f;
                        speed = 30.0f;
                        stability = -30.0f;
                    }
					else
                    {
                        if (playerCoins >= power4RequiredCoins)
                        {
                            playerCoins -= 400;
                            powerUp.sprite = powersprite[4];
                            power4Unlocked = true;
                            powerChosen[i] = true;
                            powerUp.color = Color.grey;
							SoundManager.instance.confirmSound.Play();// audio feedback upon selection
						}
						else
                        {
                            powerUp.sprite = powersprite[11];
                            powerUp.color = Color.white;
							SoundManager.instance.deselectSound.Play();
						}
					}
                }
                else if (powerIndex[i] == 6)
                {
                    if (power5Unlocked)
                    {
                        powerUp.sprite = powersprite[5];
                        powerChosen[i] = true;
                        powerUp.color = Color.grey;
						SoundManager.instance.confirmSound.Play();// audio feedback upon selection
                        strength = 30.0f;
                        speed = 0.0f;
                        stability = -30.0f;
                    }
					else
                    {
                        if (playerCoins >= power5RequiredCoins)
                        {
                            playerCoins -= 500;
                            powerUp.sprite = powersprite[5];
                            power5Unlocked = true;
                            powerChosen[i] = true;
                            powerUp.color = Color.grey;
							SoundManager.instance.confirmSound.Play();// audio feedback upon selection
						}
						else
                        {
                            powerUp.sprite = powersprite[11];
                            powerUp.color = Color.white;
							SoundManager.instance.deselectSound.Play();
						}
					}
                }
                menuAnim.Play("PowerAPress");
                menuAnim.Play("PressWhenReady");
                StartText.gameObject.SetActive(true);
            }
            //back to select powerUp
            else if (Input.GetButtonDown("JoyB" + i) && characterChosen[i] == true && powerChosen[i] == true)
            {
                powerChosen[i] = false;
                powerUp.color = Color.white;
				SoundManager.instance.deselectSound.Play();
				menuAnim.Play("PowerIdle");
            }
            //check if player has selected powerUp and player
            if (characterChosen[i] == false || powerChosen[i] == false)
            {
                StartText.gameObject.SetActive(false);
                readyPanel.gameObject.SetActive(false);
                playerReady[i] = false;
            }
            //indicates player is ready
            else if (characterChosen[i] == true && powerChosen[i] == true && Input.GetButtonDown("JoyStart" + i))
            {
				SoundManager.instance.readySound.Play();
                playerReady[i] = true;
                readyPanel.gameObject.SetActive(true);
            }
        }
    }
    //Hides none player Panels from screen until players join with Start button
    public void AiCharSet(int i)
    {
        if (panel == GameObject.Find("Character" + i))
        {
            if (Input.GetButtonDown("JoyStart" + i) && IsPlayer[i] == 0)
            {
                characterChosen[i] = false;
                powerChosen[i] = false;
                playerReady[i] = false;
                panel.transform.localScale = new Vector3(1, 1, 1);
                IsPlayer[i] = 1;
            }
            else if (IsPlayer[i] == 0)
            {
                panel.transform.localScale = new Vector3(0, 0, 0);
                index[i] = Random.Range(1, 6);
                characterChosen[i] = true;
                powerIndex[i] = Random.Range(1, 7);
                powerChosen[i] = true;
                playerReady[i] = true;
            }
        }
    }

    public struct Stats
    {
        public float strengthStats;
        public float speedStats;
        public float stabilityStats;
    }

    static int NUM_CHARACTERS = 5;
    static int NUM_POWERUPS = 6;

    public static Stats[] CharStats = new Stats[NUM_CHARACTERS];
    public static Stats[] PowerUpStats = new Stats[NUM_POWERUPS];

    public Stats GetCharacterStats(int characterIndex, int PowerupIndex)
    {
        Stats retStats = new Stats();
        retStats.strengthStats = CharStats[characterIndex - 1].strengthStats + PowerUpStats[PowerupIndex-1].strengthStats;
        retStats.speedStats = CharStats[characterIndex-1].speedStats + PowerUpStats[PowerupIndex-1].speedStats;
        retStats.stabilityStats = CharStats[characterIndex-1].stabilityStats + PowerUpStats[PowerupIndex-1].stabilityStats;
        return retStats;
    }
    //charcter stats to display
    void character1Stats()
    {
        strength = 25;
        speed = 75;
        stability = 45;
        image.sprite = sprite[0];
    }
    void character2Stats()
    {
        strength = 85;
        speed = 20;
        stability = 7;
        image.sprite = sprite[1];
    }
    void character3Stats()
    {
        strength = 52;
        speed = 98;
        stability = 10;
        image.sprite = sprite[2];
    }
    void character4Stats()
    {
        strength = 10;
        speed = 86;
        stability = 45;
        image.sprite = sprite[3];
    }
    void character5Stats()
    {
        strength = 25;
        speed = 32;
        stability = 87;
        image.sprite = sprite[4];
    }

    //sets power index
    void powerindex(int i)
    {
        if (powerIndex[i] == 1)
        { 
            powerUp.sprite = powersprite[0];
            powerName.text = "Donut";
            powerstats.text = "Strenght + \n speed---\nStability++";
        }
        else if (powerIndex[i] == 2)
        {
            if (powerIndex[i] == 2 && power1Unlocked)
            {
                powerUp.sprite = powersprite[1];
            }
            else
            {
                powerUp.sprite = powersprite[6];
            }
            powerName.text = "Promotion";
            powerstats.text = "Strenght +++ \n speed---\nStability+++";
        }
        else if (powerIndex[i] == 3)
        {
            if (powerIndex[i] == 3 && power2Unlocked)
            {
                powerUp.sprite = powersprite[2];
            }
            else
            {
                powerUp.sprite = powersprite[7];
            }
            powerName.text = "WD-40";
            powerstats.text = "Strenght \n speed++\nStability+++";
        }
        else if (powerIndex[i] == 4)
        {
            if (powerIndex[i] == 4 && power3Unlocked)
            {
                powerUp.sprite = powersprite[3];
            }
            else
            {
                powerUp.sprite = powersprite[8];
            }
            powerName.text = "Coffee";
            powerstats.text = "Strenght  \n speed+++\nStability--";
        }
        else if (powerIndex[i] == 5)
        {
            if (powerIndex[i] == 5 && power4Unlocked)
            {
                powerUp.sprite = powersprite[4];
            }
            else
            {
                powerUp.sprite = powersprite[9];
            }
            powerName.text = "Rocket Booster";
            powerstats.text = "Strenght+  \n speed+++\nStability---";
        }
        else if (powerIndex[i] == 6)
        {
            if (powerIndex[i] == 6 && power5Unlocked)
            {
                powerUp.sprite = powersprite[5];
            }
            else
            {
                powerUp.sprite = powersprite[10];
            }
            powerName.text = "Kale";
            powerstats.text = "Strenght+++  \n speed\nStability-";
        }
    }
}
