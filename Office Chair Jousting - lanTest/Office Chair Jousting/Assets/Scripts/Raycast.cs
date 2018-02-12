using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {
    public float downlength;
    public float leftlength;
    public float fwdlength;
    public float backlength;
    public float rightlength;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit dhit;
        RaycastHit lhit;
        RaycastHit rhit;
        RaycastHit fhit;
        RaycastHit bhit;

        Ray downRay = new Ray(transform.position, -transform.up);
        Ray leftRay = new Ray(transform.position, -transform.right);
        Ray rgtRay = new Ray(transform.position, transform.right);
        Ray fwdRay = new Ray(transform.position, transform.forward);
        Ray backRay = new Ray(transform.position, -transform.forward);

        Debug.DrawRay(transform.position, -transform.up * downlength);
        Debug.DrawRay(transform.position, -transform.right * leftlength);
        Debug.DrawRay(transform.position, transform.right * rightlength);
        Debug.DrawRay(transform.position, transform.forward * fwdlength);
        Debug.DrawRay(transform.position, -transform.forward * backlength);

        Physics.Raycast(leftRay, out lhit, leftlength);
        Physics.Raycast(rgtRay, out rhit, rightlength);
        Physics.Raycast(fwdRay, out fhit, fwdlength);
        Physics.Raycast(backRay, out bhit, backlength);

        if (Physics.Raycast(downRay, out dhit, downlength))
        {
            if (dhit.collider.tag == "Ground" )
                Debug.Log("On ground");
        }
        else if (dhit.collider == null && lhit.collider == true || rhit.collider == true || fhit.collider == true || bhit.collider == true)
        {
            Debug.Log("Prone");
        }
        
	}
}
