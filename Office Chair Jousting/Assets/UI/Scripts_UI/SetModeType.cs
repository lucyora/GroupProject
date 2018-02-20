using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModeType : MonoBehaviour {

    public int gameMode;
    public bool doneWithMenu = false;

    public void done()
    {
        doneWithMenu = true;
    }
    public void ModeType(int index)
    {
        gameMode = index;
    }
}
