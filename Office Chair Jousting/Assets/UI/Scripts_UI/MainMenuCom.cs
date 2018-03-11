using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCom : MonoBehaviour {
    public MainMenuNav mainmenu;
    public OptionsNav optionsmenu;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (mainmenu.onMainMenu == true)
        {
            mainmenu.MainMenuAnimations();
        }
        else if (optionsmenu.toVolume == false && optionsmenu.toLanguages == false)
        {
            optionsmenu.OptionsAnimation();
        }
        if(optionsmenu.toMain == true)
        {
            mainmenu.menuAnim.Play("BackToMain");
            mainmenu.onMainMenu = true;
            optionsmenu.toMain = false;
        }
        if(optionsmenu.toVolume == true && Input.GetButtonDown("JoyB0"))
        {
            optionsmenu.toVolume = false;
        }

    }
}
