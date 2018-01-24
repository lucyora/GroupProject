using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiPlayerCamera : MonoBehaviour {

    public List<Transform> targets;
    public Vector3 offset;
    public float minZoom = 10.0f;
    public float maxZoom = 60.0f;
    public float zoomLimiter = 50.0f;
    public float smoothTime = 0.5f;


    private Vector3 velocity;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }

        MoveCamera();
        ZoomCamera();
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count== 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }


    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDiatance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i< targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.z;
    }
    void ZoomCamera()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDiatance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        Debug.Log(GetGreatestDiatance());
    }

}
