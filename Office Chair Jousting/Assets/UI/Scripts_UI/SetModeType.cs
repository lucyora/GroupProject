using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetModeType : MonoBehaviour {

    public bool doneWithMenu = false;
    private float controlDelay;
    public int gameModeIndex = 0;
    public Transform[] gameModePosition = new Transform[3];
    public Button ToChar_btn;
    public RectTransform nav;

    public void navControl()
    {
        if (Input.GetAxis("Joy0X") <= -0.2)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.1)
            {
                Debug.Log("Music");
				SoundManager.instance.selectSound.Play();
                MoveNav(-1);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0X") >= 0.2)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.1)
            {
                SoundManager.instance.selectSound.Play();
                MoveNav(1);
                controlDelay = 0;
            }
        }
    }
     void MoveNav(int change)
    {
        if (change > 0)
        {
            if (gameModeIndex + change < gameModePosition.Length - 1)
            {
                gameModeIndex += change;
            }
            else
            {
                gameModeIndex = gameModePosition.Length - 1;
            }
        }
        else
        {
            if (gameModeIndex + change >= 0)
            {
                gameModeIndex += change;
            }
            else
            {
                gameModeIndex = 0;
            }
        }
        nav.position = gameModePosition[gameModeIndex].position;
    }
}
