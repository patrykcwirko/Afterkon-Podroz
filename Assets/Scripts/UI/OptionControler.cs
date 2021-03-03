using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionControler : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefController.GetMasterVolume();
    }

    // Update is called once per frame
    void Update()
    {
        var musicPlayer = FindObjectOfType<AudioSource>();
        if (musicPlayer)
        {
            musicPlayer.volume = volumeSlider.value;
        }
        else
        {
            Debug.LogWarning("No music player found... did you start from splash?");
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefController.SetMasterVolume(volumeSlider.value);
        FindObjectOfType<LevelLoader>().LoadStartScene();
    }

    public void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
    }
}
