using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauzeController : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseUI;
    public GameObject optionUI;
    public GameObject optionButton;
    public GameObject continueButton;
    public GameObject quitButton;
    public GameObject newCharacterUI;
    public PlayerInfo info;
    public Player.StrStates playerInput;

    void Start()
    {
        if (continueButton == null) return;
        if (info.currentMapIndex == 0)
        {
            continueButton.gameObject.SetActive(false);
            quitButton.transform.position = optionButton.transform.position;
            optionButton.transform.position = continueButton.transform.position;
        }
    }

    void Update()
    {
        if (!playerInput) return;
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
        optionUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void ToOption()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(true);
    }

    public void BackToMenu()
    {
        pauseUI.SetActive(true);
        optionUI.SetActive(false);
        newCharacterUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(false);
        newCharacterUI.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(info.currentMapIndex);
    }
}
