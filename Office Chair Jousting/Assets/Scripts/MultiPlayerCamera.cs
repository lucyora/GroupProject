﻿// "Multiple Target Camera in Unity" Tutorial followed from Brackeys Youtube channel https://www.youtube.com/watch?v=aLpixrPvlB8 published on Dec 17, 2017



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiPlayerCamera : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();  // Dynamic target list to follow.It allows add as many player as controller connected.
    public Vector3 offset;                                  // Offset the camera to keep every player inside the range
    public float minZoom = 10.0f;
    public float maxZoom = 60.0f;
    public float zoomLimiter = 50.0f;
    public float smoothTime = 0.5f;

    private Vector3 velocity;
    private Camera cam;

    private Player player;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        targets = gameManager.PlayerList;
    }


    private void LateUpdate()
    {
        if (targets.Count == 0)                             // Null check condition, if there is no player to follow for camera it just returns. 
        {
            return;
        }

        MoveCamera();
        ZoomCamera();
    }

    Vector3 GetCenterPoint()                                // Keep track of the center point of all the player distances 
    {
        if (targets.Count == 1)                             //If there is only one target no need to keep track of center, but still needs to follow the player
        {
            if (!targets[0].gameObject.GetComponent<Player>().IsInElevator)
            {
                return targets[0].transform.position;
            }
        }

        var bounds = new Bounds();         // Creates the boundary around all connected player.
        //var bounds = new Bounds(targets[0].transform.position, Vector3.zero);         // Creates the boundary around all connected player.
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].gameObject.GetComponent<Player>().IsInElevator)
            {
                bounds.Encapsulate(targets[i].transform.position);        // Resizes the box according to the targets
            }
        }


        return bounds.center;                               // Returns the center point of the boundary
        
    }


    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);     // This line of code smoothens the camera movements
    }

    float GetGreatestDiatance()                             // Finds out the distance between players to adjust zoom and movements of the camera.
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.size.z;
    }
    void ZoomCamera()                                       // Adjusts FOV in the camera when everybody is close to eachother and does viceversa
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDiatance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

}









