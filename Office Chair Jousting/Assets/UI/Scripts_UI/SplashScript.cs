using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LoadMain());
	}

    IEnumerator LoadMain()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        //SceneManager.LoadSceneAsync("MainMenu");
    }

    public void MoveToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
