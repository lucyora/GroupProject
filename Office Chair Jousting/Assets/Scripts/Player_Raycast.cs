using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raycast : Player_GameController {

    
    private float downlength = 10;
    private float leftlength = 2;
    private float fwdlength = 2;
    private float backlength = 2;
    private float rightlength = 2;
    private float uplength = 5;
    private bool[] hitarray = new bool[4];
	//<summary>
    // returns true if the player is alive based on the raycasts. Returns false if the player is dead.
    //</summary>
	public bool isOBJAlive () {

        RaycastHit dhit;
        RaycastHit lhit;
        RaycastHit rhit;
        RaycastHit fhit;
        RaycastHit bhit;
        RaycastHit uhit;

        Ray downRay = new Ray(transform.position, -transform.up);
        Ray upRay = new Ray(transform.position, transform.up);
        Ray leftRay = new Ray(transform.position, -transform.right);
        Ray rightRay = new Ray(transform.position, transform.right);
        Ray fwdRay = new Ray(transform.position, transform.forward);
        Ray backRay = new Ray(transform.position, -transform.forward);

        Debug.DrawRay(transform.position, transform.up * downlength, Color.black);
        Debug.DrawRay(transform.position, -transform.up * downlength, Color.black);
        Debug.DrawRay(transform.position, -transform.right * leftlength, Color.black);
        Debug.DrawRay(transform.position, transform.right * rightlength, Color.black);
        Debug.DrawRay(transform.position, transform.forward * fwdlength, Color.black);
        Debug.DrawRay(transform.position, -transform.forward * backlength, Color.black);

        Physics.Raycast(leftRay, out lhit, leftlength);
        Physics.Raycast(rightRay, out rhit, rightlength);
        Physics.Raycast(fwdRay, out fhit, fwdlength);
        Physics.Raycast(backRay, out bhit, backlength);
        Physics.Raycast(upRay, out uhit, uplength);

        if (Physics.Raycast(downRay, out dhit, downlength))
        {
            if (dhit.collider.tag == "Ground")
            {
                return true;
            }
            if (dhit.collider.tag == "DeathPlane")
            {
                return false;
            }

        }
        hitarray[0] = sidesraycast(leftRay, lhit, leftlength);
        hitarray[1] = sidesraycast(rightRay, rhit, rightlength);
        hitarray[2] = sidesraycast(fwdRay, fhit, fwdlength);
        hitarray[3] = sidesraycast(backRay, bhit, backlength);
        foreach (bool goodhit in hitarray)
        {
            if (!goodhit)
            {
                return false;
            }
        }
        if (Physics.Raycast(upRay, out uhit, uplength))
        {
            return false;
        }

            return true;
    
	}
    public bool sidesraycast(Ray raycast, RaycastHit hit, float length)
    {
        if (Physics.Raycast(raycast, out hit, length))
        {
            if (hit.collider.tag == "Ground" || hit.collider.tag == "DeathPlane")
            {
                return false;
            }
        }
        return true;
    }
}
