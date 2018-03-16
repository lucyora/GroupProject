using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Player : MonoBehaviour
{
    public Player playerPrefab;
    public Camera cam;
    public Transform target;

    //private TeextMeshPro;
    private TextMesh m_TextMesh;
   

    //TODO: Manage Player str for hud from platforms - xbox, switch..?
    //player.cs
    // public enum character { Jenny, Steve, Gretchen, Bubba };

    //Player playerClass;

    //HP?

    //Game status text 

    // Use this for initialization
    void Start ()
    {
        //TODO: Initialize TM font, set str	

        //playerClass = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: Need to get Players pos for name tags        

        // transform.position = playerPrefab.transform.position; // Need to manage offsets for canvas

        //playerPrefab.World AND clamp on screen
        //Vector3 coords = cam.WorldToScreenPoint(target.position);
        //coords.y = Screen.height - coords.y;




    }
}
