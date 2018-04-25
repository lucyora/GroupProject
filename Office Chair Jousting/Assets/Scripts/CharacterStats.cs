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
                Strength = 1;
                Mass = 136;
                SpeedLimiter = 10;
                RotationSnapRange = 10;
                Gender = 1;
                break;
            case Player.character.Steve:
                Strength = 85;
                Mass = 136;
                SpeedLimiter = 3;
                RotationSnapRange = 5;
                Gender = 0;
                break;
            case Player.character.Judith:
                Strength = 70;
                Mass = 136;
                SpeedLimiter = 15;
                RotationSnapRange = 10;
                Gender = 1;
                break;
            case Player.character.Bubba:
                Strength = 2;
                Mass = 140;
                SpeedLimiter = 20;
                RotationSnapRange = 15;
                Gender = 0;
                break;
            case Player.character.Harry:
                Strength = 2;
                Mass = 140;
                SpeedLimiter = 25;
                RotationSnapRange = 5;
                Gender = 0;
                break;
            case AI.character.AI:
                Strength = 2;
                Mass = 140;
                SpeedLimiter = 7;
                RotationSnapRange = 3;
                Gender = 2;
                break;

        }
    }
}
