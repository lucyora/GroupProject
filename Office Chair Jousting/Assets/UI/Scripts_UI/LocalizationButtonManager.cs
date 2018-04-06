using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.IO;
using System.Text;

using UnityEngine.SceneManagement;

public class LocalizationButtonManager: MonoBehaviour
{
    LocalizationManager LocManager;

    // Use this for initialization
    void Start ()
    {
        LocManager = GameObject.FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;
     
        if(LocManager == null)
        {
            Debug.Log("Fail to load locM");
        }

    }

    public void LoadLanguageScene()
    {
        SceneManager.LoadScene("Scenes/Language");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void RefreshScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void TriggerLocalization(string filename)
    {
        LocManager.LoadLocalizedText(filename);
    }
}
