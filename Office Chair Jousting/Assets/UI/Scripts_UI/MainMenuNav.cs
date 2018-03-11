using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : NavScript
{
    public Animator menuAnim;
    public bool onMainMenu = true;

    void Awake()
    {
        ControllerDelayBy = 0.15f;
        VerticalInput = 0.2f;
        VerticalJump = 1;
        HorizontalInput = 1.0f;
        HorizontalJump = 1;
    }
    void Start()
    {
        menuAnim = GetComponent<Animator>();
    }
    public void Update()
    {
        navControl();
    }

    public void MainMenuAnimations()
    {
        switch(Index)
        {
            case 0:
                menuAnim.Play("PlayHighlightbtn");
                if(Input.GetButtonUp("JoyA0"))
                {
                    menuAnim.Play("PlayPress");
                    SceneManager.LoadSceneAsync("CharacterSelect");
                }
                break;
            case 1:
                menuAnim.Play("OptionsHighlightbtn");
                if (Input.GetButtonUp("JoyA0"))
                {
                    menuAnim.Play("OptionsPress");
                    menuAnim.Play("ToOptionsMenu");
                    onMainMenu = false;
                }
                break;
            case 2:
                menuAnim.Play("CreditHighlightbtn");
                if (Input.GetButtonUp("JoyA0"))
                {
                    menuAnim.Play("CreditsPress");
                    SceneManager.LoadSceneAsync("credits");
                }
                break;
            case 3:
                menuAnim.Play("QuitHighlight");
                if (Input.GetButtonUp("JoyA0"))
                {
                    menuAnim.Play("QuitPress");
                    Application.Quit();
                }
                break;
        }
    }
}
