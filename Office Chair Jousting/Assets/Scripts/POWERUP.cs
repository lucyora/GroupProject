/**
 *Power UP DB 
 * All stat increases will be defined here
 *
 * INTERNAL INDEX TO UI 
 * 
 * 1 = Donut
 * 2 = Promotion
 * 3 = WD-40
 * 4 = Coffee
 * 5 = Rocket Boosters
 * 6 = Kale
 *  
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWERUP
{
    public float Strength;
    public float Speed;
    public float Stability;
    public POWERUP(int powerindex)
    {
        switch (powerindex)
        {
            case 1:
                Strength = 10;
                Speed = 0;
                Stability = 3;
                break;
            case 2:
                Strength = 30;
                Speed = 0;
                Stability = 5;
                break;
            case 3:
                Strength = 0;
                Speed = 10;
                Stability = 5;
                break;
            case 4:
                Strength = 0;
                Speed = 15;
                Stability = 0;
                break;
            case 5:
                Strength = 5;
                Speed = 15;
                Stability = 0;
                break;
            case 6:
                Strength = 20;
                Speed = 0;
                Stability = 0;
                break;
            default:
                Debug.LogWarning("Powerup index is either too high or too low. Powerup value is "+powerindex);
                Strength = 0;
                Speed = 0;
                Stability = 0;
                break;
        }
    }
}
