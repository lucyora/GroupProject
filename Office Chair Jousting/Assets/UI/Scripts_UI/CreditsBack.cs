using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("JoyB"+i))
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }
	}
}
