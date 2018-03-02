using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRToggle : MonoBehaviour {



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ToggleVR();
        }        
    }

    void ToggleVR()
    {
        if(UnityEngine.XR.XRSettings.loadedDeviceName == "Oculus")
        {
            StartCoroutine(LoadDevice("None"));
            Debug.Log("None");
        }
        else
        {
            StartCoroutine(LoadDevice("Oculus"));
            Debug.Log("Oculus");
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }
}
