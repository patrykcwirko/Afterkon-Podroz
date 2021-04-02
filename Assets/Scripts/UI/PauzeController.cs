using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzeController : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseUI;
    public Player.StrStates playerInput;

    void Start()
    {
        
    }

    void Update()
    {
        if(playerInput.pausePush)
        {            
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            playerInput.pausePush = false;
        }
    }

    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
