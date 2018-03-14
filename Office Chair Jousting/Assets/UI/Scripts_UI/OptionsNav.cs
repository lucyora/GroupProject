using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsNav : NavScript{
    Animator optionsAnim;
    public bool toMain = false;
    public bool toVolume = false;
    public bool toLanguages = false;


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
        optionsAnim = GetComponent<Animator>();
    }
    void Update ()
    {
        navControl();
    }

    public void OptionsAnimation()
    {
        switch(Index)
        {
            case 0:
                optionsAnim.Play("VolumeHighlight");
                if(Input.GetButtonDown("JoyA0"))
                {
                    optionsAnim.Play("VolumePress");
                    optionsAnim.Play("ToVolume");
                    toVolume = true;
                }
                break;
            case 1:
                optionsAnim.Play("LanguagesHighlight");
                break;
            case 2:
                optionsAnim.Play("BackHighlight");
                if(Input.GetButtonUp("JoyA0"))
                {
                    optionsAnim.Play("BackPress");
                    toMain = true;
                }
                break;
        }
    }
}
