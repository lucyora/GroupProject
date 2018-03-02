using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
 
public class Controller : MonoBehaviour {
    //Set sensitivity to one
    public enum current_player {Player_1,Player_2,Player_3,Player_4,AI};
    public current_player Current_Player;
    private bool isAI;

    private string P1_LX = "Horizontal";
    private string P1_LY = "Vertical";
 
    private string P1_RX = "Horizontal2";
    private string P1_RY = "Vertical2";
    //Placeholder Values. Fill in with appropriate axis labels later
    private string P2_LX = "2Horizontal";
    private string P2_LY = "2Vertical";
 
    private string P2_RX = "2Horizontal2";
    private string P2_RY = "2Vertical2";
 
    private string P3_LX = "3Horizontal";
    private string P3_LY = "3Vertical";
 
    private string P3_RX = "3Horizontal2";
    private string P3_RY = "3Vertical2";
 
    private string P4_LX = "4Horizontal";
    private string P4_LY = "4Vertical";
 
    private string P4_RX = "4Horizontal2";
    private string P4_RY = "4Vertical2";
    //

    //Strings to be used once player is determined
    [HideInInspector] public string SelectedP_LX;
    [HideInInspector] public string SelectedP_LY;

    [HideInInspector]    public string SelectedP_RX;
    [HideInInspector]    public string SelectedP_RY;
    
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