using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private Button previousButton;
    [SerializeField]
    private float scaleAmount = 1.4f;

    [SerializeField]
    private GameObject defaultButton;

	// Use this for initialization
	public void Start ()
    {
	    if(defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        var seletedObj = EventSystem.current.currentSelectedGameObject;
        
        if(seletedObj == null)
        {
            return;
        }

        var selectedAsButton = seletedObj.GetComponent<Button>();
        previousButton = selectedAsButton;
    }

}
