using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //configuration parameters
    [SerializeField] int waitTime = 4;
    [SerializeField] AudioClip levelAudio;

    //state variable
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<SoundController>().PlayClip();
        if(currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(waitTime);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start Screen");
    }
    
    public void LoadOptionScene()
    {
        SceneManager.LoadScene("Option Screen");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public AudioClip GetAudio()
    {
        return levelAudio;
    }
}
