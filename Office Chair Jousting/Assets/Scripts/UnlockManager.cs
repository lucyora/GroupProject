using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnlockManager : MonoBehaviour
{
    [System.Serializable]
    public class Unlock
    {
        public string unlockText;
        public int unlocked;
        public bool isInteractable;
        public Button.ButtonClickedEvent OnClickEvent;

    }
    public GameObject button;
    public Transform spacer;
    public List<Unlock> PowerupList;
     
    void Start()
    {
        FillList();
    }
    void FillList()
    {
        foreach (var Powerup in PowerupList)
        {
            GameObject newButton = Instantiate(button) as GameObject;
            UnlockButton buttonInst = newButton.GetComponent<UnlockButton>();
            buttonInst.powerUpName.text = Powerup.unlockText;

            //if (PlayerPrefs.GetString(buttonInst.powerUpName.text) == "Kale")
            {
                Powerup.unlocked = 1;
                Powerup.isInteractable = true;
            }

            buttonInst.isUnlocked = Powerup.unlocked;
            buttonInst.GetComponent<Button>().interactable = Powerup.isInteractable;


            newButton.transform.SetParent(spacer);
            //newButton.transform.SetParent(spacer, false);
        }
        SaveAll();
    }

    //private static void writingData(Unlock Powerup, UnlockButton buttonInst)
    //{
    //    buttonInst.powerUpName.text = Powerup.unlockText;
    //}

    void SaveAll()
    {
        if (PlayerPrefs.HasKey("Kale"))
        {
            return;
        }
        else
        {
            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("PowerUpButton");
            foreach (GameObject buttons in allButtons)
            {
                UnlockButton buttonSaved = buttons.GetComponent<UnlockButton>();
                PlayerPrefs.SetString(buttonSaved.powerUpName.text, buttonSaved.isUnlocked.ToString());
            }
        }
    }

}
