using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraMove : MonoBehaviour {
    public float cameraSpeed = 0.01f;
    public float cameraZoom = 1;
    public Transform CurrentPosition;
    Vector3 lastPossition;
    public Camera cam;
	// Use this for initialization
	void Start ()
    {
        lastPossition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //go between Current Camera Position and next Camera Position
        cam.transform.position = Vector3.Lerp(transform.position, CurrentPosition.position, cameraSpeed);
        cam.transform.rotation = Quaternion.Slerp(transform.rotation, CurrentPosition.rotation, cameraSpeed);
        float velocity = Vector3.Magnitude(transform.position - lastPossition);
        cam.fieldOfView = 60 + velocity * cameraZoom;
        lastPossition = transform.position;
	}

    public void SetPosition(Transform newPossition)
    {
        //set current camera position to new camera position
        CurrentPosition = newPossition;
    }
}
