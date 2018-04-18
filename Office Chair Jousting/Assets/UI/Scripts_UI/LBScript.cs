using System.Collections;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LBScript : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start()
    {
        buttonSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    public void target()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
    }
    
}
