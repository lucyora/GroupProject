using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavScript : MonoBehaviour {

    private float controlDelay;
    public int Index;
    public Transform[] mapPosition;
    public float ControllerDelayBy;
    public float HorizontalInput;
    public float VerticalInput;
    public int HorizontalJump;
    public int VerticalJump;


    public void navControl()
    {
        if (Input.GetAxis("Joy0XL") <= -HorizontalInput)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= ControllerDelayBy)
            {
                MoveNav(-HorizontalJump);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0XL") >= HorizontalInput)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= ControllerDelayBy)
            {
                MoveNav(HorizontalJump);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0YL") <= -VerticalInput)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= ControllerDelayBy)
            {
                MoveNav(VerticalJump);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0YL") >= VerticalInput)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= ControllerDelayBy)
            {
                MoveNav(-VerticalJump);
                controlDelay = 0;
            }
        }
    }

    void MoveNav(int change)
    {
        if (change > 0)
        {
            if (Index + change < mapPosition.Length - 1)
            {
                Index += change;
            }
            else
            {
                Index = mapPosition.Length - 1;
            }
        }
        else
        {
            if (Index + change >= 0)
            {
                Index += change;
            }
            else
            {
                Index = 0;
            }
        }
    }
}
