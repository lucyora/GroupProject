/*
 Character Stats
 
 Setting Stats for specific characters
 
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{

    public float Strength;
    public float Mass;
    public float SpeedLimiter;
    public float RotationSnapRange;
    public int Gender;
    public CharacterStats(Player.character Character)
    {
        switch (Character)
        {
            case Player.character.Jenny:
                Strength = 30;
                Mass = 136;
                SpeedLimiter = 10;
                RotationSnapRange = 5;
                Gender = 1;
                break;
            case Player.character.Steve:
                Strength = 85;
                Mass = 136;
                SpeedLimiter = 2;
                RotationSnapRange = 1;
                Gender = 0;
                break;
            case Player.character.Judith:
                Strength = 70;
                Mass = 136;
                SpeedLimiter = 15;
                RotationSnapRange = 1;
                Gender = 1;
                break;
            case Player.character.Bubba:
                Strength = 2;
                Mass = 140;
                SpeedLimiter = 7;
                RotationSnapRange = 3;
                Gender = 0;
                break;
            case Player.character.Harry:
                Strength = 2;
                Mass = 140;
                SpeedLimiter = 7;
                RotationSnapRange = 3;
                Gender = 0;
                break;

        }
    }
}
