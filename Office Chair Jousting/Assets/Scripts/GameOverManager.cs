using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public bool isGameOver = false;

    [SerializeField]
    private GameObject gameOverCanvas;

    public  void Update()
    {
        GameOver();
    }
    

    public void GameOver()
    {

        gameOverCanvas.SetActive(false);
        Time.timeScale = 1.0f;                      // the time is passing as fast as realtime

        if (isGameOver == true)
        {
            gameOverCanvas.SetActive(true);         // activates game over canvas
            Time.timeScale = 0.0f;                  // stop time once game is over
        }
    }
}
