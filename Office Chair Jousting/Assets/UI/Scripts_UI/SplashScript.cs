using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {
    Button start;
	// Use this for initialization
	void Start ()
    {
        start = GameObject.Find("Canvas").GetComponent<Button>();
        StartCoroutine(LoadMain());
	}

    IEnumerator LoadMain()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        start.onClick.Invoke();
        //SceneManager.LoadSceneAsync("MainMenu");
    }

    public void MoveToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
