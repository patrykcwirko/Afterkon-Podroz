using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayClip();
        DontDestroyOnLoad(this);
    }

    public void PlayClip()
    {
        var sourceAudio = GetComponent<AudioSource>();
        var clip = FindObjectOfType<LevelLoader>().GetAudio();
        if(sourceAudio.clip != clip)
        {
            sourceAudio.clip = clip;
            sourceAudio.Play();
        }
    }
}
