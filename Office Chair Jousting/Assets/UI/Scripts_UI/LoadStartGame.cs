using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartGame : MonoBehaviour {
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    } 

}
