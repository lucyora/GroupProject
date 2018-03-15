using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
 
public class Controller : MonoBehaviour {
    //Set sensitivity to one
    public enum current_player {Player_1,Player_2,Player_3,Player_4,AI};
    public current_player Current_Player;
    [HideInInspector] public bool isAI;

    private string P1_LX = "Joy0XL";
    private string P1_LY = "Joy0YL";
 
    private string P1_RX = "Joy0XR";
    private string P1_RY = "Joy0YR";

    private string P2_LX = "Joy1XL";
    private string P2_LY = "Joy1YL";
 
    private string P2_RX = "Joy1XR";
    private string P2_RY = "Joy1YR";
 
    private string P3_LX = "Joy2XL";
    private string P3_LY = "Joy2YL";
 
    private string P3_RX = "Joy2XR";
    private string P3_RY = "Joy2YR";
 
    private string P4_LX = "Joy3XL";
    private string P4_LY = "Joy3YL";
 
    private string P4_RX = "Joy3XR";
    private string P4_RY = "Joy3YR";
    //

    //Strings to be used once player is determined
    [HideInInspector] public string SelectedP_LX;
    [HideInInspector] public string SelectedP_LY;
    [HideInInspector] public string SelectedP_RX;
    [HideInInspector] public string SelectedP_RY;
    
    // Use this for initialization
    public void InitController()
    {
        switch (Current_Player)
        {
            case current_player.Player_1:
                SelectedP_LX = P1_LX;
                SelectedP_LY = P1_LY;
                SelectedP_RX = P1_RX;
                SelectedP_RY = P1_RY;
                break;
            case current_player.Player_2:
                SelectedP_LX = P2_LX;
                SelectedP_LY = P2_LY;
                SelectedP_RX = P2_RX;
                SelectedP_RY = P2_RY;
                break;
            case current_player.Player_3:
                SelectedP_LX = P3_LX;
                SelectedP_LY = P3_LY;
                SelectedP_RX = P3_RX;
                SelectedP_RY = P3_RY;
                break;
            case current_player.Player_4:
                SelectedP_LX = P4_LX;
                SelectedP_LY = P4_LY;
                SelectedP_RX = P4_RX;
                SelectedP_RY = P4_RY;
                break;
            case current_player.AI:
                SelectedP_LX = "AI";
                SelectedP_LY = "AI";
                SelectedP_RX = "AI";
                SelectedP_RY = "AI";
                isAI = true;
                break;
        }
    }
   
   
   

}