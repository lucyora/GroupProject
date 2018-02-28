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
        if(UnityEngine.VR.VRSettings.loadedDeviceName == "Oculus")
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
        UnityEngine.VR.VRSettings.LoadDeviceByName(newDevice);
        yield return null;
        UnityEngine.VR.VRSettings.enabled = true;
    }
}
