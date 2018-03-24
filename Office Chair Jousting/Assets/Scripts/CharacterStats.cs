/*
 Character Stats
 
 Setting Stats for specific characters
 Values are currently temporary. Fill in at least 3 others with unique values
 
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
    public CharacterStats(Player.character Character)
    {
        switch (Character)
        {
            case Player.character.Jenny:
                Strength = 0;
                Mass = 136;
                SpeedLimiter = 5;
                RotationSnapRange = 3;
                break;
            case Player.character.Steve:
                Strength = 0;
                Mass = 136;
                SpeedLimiter = 5;
                RotationSnapRange = 3;
                break;
            case Player.character.Gretchen:
                Strength = 0;
                Mass = 136;
                SpeedLimiter = 5;
                RotationSnapRange = 3;
                break;
            case Player.character.Bubba:
                Strength = 0;
                Mass = 136;
                SpeedLimiter = 5;
                RotationSnapRange = 3;
                break;

        }
    }
}
