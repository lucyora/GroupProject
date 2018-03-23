using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : Controller {
    private float downlength = 30;
    private float leftlength = 2;
    private float fwdlength = 2;
    private float backlength = 2;
    private float rightlength = 2;
	
	// Update is called once per frame
	public bool isOBJAlive () {

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

        Debug.DrawRay(transform.position, -transform.up * downlength, Color.black);
        Debug.DrawRay(transform.position, -transform.right * leftlength, Color.black);
        Debug.DrawRay(transform.position, transform.right * rightlength, Color.black);
        Debug.DrawRay(transform.position, transform.forward * fwdlength, Color.black);
        Debug.DrawRay(transform.position, -transform.forward * backlength, Color.black);

        Physics.Raycast(leftRay, out lhit, leftlength);
        Physics.Raycast(rgtRay, out rhit, rightlength);
        Physics.Raycast(fwdRay, out fhit, fwdlength);
        Physics.Raycast(backRay, out bhit, backlength);

        if (Physics.Raycast(downRay, out dhit, downlength))
        {
            if (dhit.collider.tag == "Ground")
            {
                return true;
            }             
        }
        else
        {
            if (dhit.collider == null)
            {
                return false;
            }
            else
            {
                if (dhit.collider.tag != "Ground" || lhit.collider.tag == "Ground" || rhit.collider.tag == "Ground" || fhit.collider.tag == "Ground" || bhit.collider.tag == "Ground")
                {
                    return false;
                }
            }
            if (dhit.collider.tag == null && lhit.collider.tag == null && rhit.collider.tag == null && fhit.collider.tag == null && bhit.collider.tag == null)
            {
                return false;
            }

        }
        return false;
    
	}
}
