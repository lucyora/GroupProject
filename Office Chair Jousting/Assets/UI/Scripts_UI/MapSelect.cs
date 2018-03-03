using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {
//    public RectTransform[] slots = new RectTransform[4];
    private float controlDelay;
    public int mapIndex =0;
    UICameraMove mapCamPos;
    public Button back_btn;
    public Transform[] mapPosition = new Transform[4];


    void Start ()
    {
        mapCamPos = GameObject.Find("Camera").GetComponent<UICameraMove>();
	}
	
	void Update ()
    {

    }

   public void CamPan(Transform pos)
    {
        mapCamPos.SetPosition(pos);
    }


    public void navControl()
    {
        if (Input.GetAxis("Joy0X") <= -1)
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
        if (Input.GetAxis("Joy0X") >= 1)
        {
            controlDelay += Time.deltaTime;
            if (controlDelay >= 0.5)
            {
//				SoundManager.instance.selectSound.Play();
				MoveNav(1);
                controlDelay = 0;
            }
        }
        if (Input.GetAxis("Joy0Y") <= -0.8)
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
        if (Input.GetAxis("Joy0Y") >= 1)
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
            SceneManager.LoadScene("Office");
        }
        if (level == 1)
        {
            Debug.Log("No Level Currently Added for Map");
            SceneManager.LoadScene("add level");
        }
        if (level == 2)
        {
            Debug.Log("No Level Currently Added for Map");
            SceneManager.LoadScene("add level");
        }
        if (level == 3)
        {
            Debug.Log("No Level Currently Added for Map");
            SceneManager.LoadScene("add level");
        }
    }
}
