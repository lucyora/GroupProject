using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {
    private float controlDelay;
    public int mapIndex =0;
    public Transform[] mapPosition = new Transform[4];
    public GameObject world;
    public GameObject Load;
    UICameraMove mapCamPos;
    public Button back_btn;

    void Start ()
    {
        mapCamPos = GameObject.Find("Camera").GetComponent<UICameraMove>();
	}

   public void CamPan(Transform pos)
    {
        mapCamPos.SetPosition(pos);
    }

    public void navControl()
    {
        if (Input.GetAxis("Joy0XL") <= -1)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.5)
            {
                Debug.Log("Music");
//				SoundManager.instance.selectSound.Play();
				MoveNav(-1);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0XL") >= 1)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.5)
            {
//				SoundManager.instance.selectSound.Play();
				MoveNav(1);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0YL") <= -0.8)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.5)
            {
                Debug.Log("Music");
//				SoundManager.instance.selectSound.Play();
				MoveNav(2);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0YL") >= 1)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.5)
            {
//				SoundManager.instance.selectSound.Play();
				MoveNav(-2);
                controlDelay = 0;
            }
        }
    }

    void MoveNav(int change)
    {
        if (change > 0)
        {
            if (mapIndex + change < mapPosition.Length - 1)
            {
                mapIndex += change;
            }
            else
            {
                mapIndex = mapPosition.Length - 1;
            }
        }
        else
        {
            if (mapIndex + change >= 0)
            {
                mapIndex += change;
            }
            else
            {
                mapIndex = 0;
            }
        }
    }
    public void LoadMap(int level)
    {
        Debug.Log(level);
        if(level == 0)
        {
            world.gameObject.SetActive(false);
            Load.gameObject.SetActive(true);
            SceneManager.LoadSceneAsync("Office");
        }
        if (level == 1)
        {
            world.gameObject.SetActive(false);
            Load.gameObject.SetActive(true);
            SceneManager.LoadSceneAsync("Ice Ice Baby");
        }
        if (level == 2)
        {
            world.gameObject.SetActive(false);
            Load.gameObject.SetActive(true);
            Debug.Log("No Level Currently Added for Map");
            SceneManager.LoadSceneAsync("add level");
        }
        if (level == 3)
        {
            world.gameObject.SetActive(false);
            Load.gameObject.SetActive(true);
            Debug.Log("No Level Currently Added for Map");
            SceneManager.LoadSceneAsync("add level");
        }
    }
}
