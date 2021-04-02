using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionControler : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider shakeSlider;
    [SerializeField] OptionData current;
    [SerializeField] OptionData defoult;

    void Start()
    {
        volumeSlider.value = current.Volume;
        shakeSlider.value = current.Shake;
    }

    void Update()
    {
        current.Volume = volumeSlider.value;
        current.Shake = shakeSlider.value;
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

    public void SetDefaults()
    {
        volumeSlider.value = defoult.Volume;
        shakeSlider.value = defoult.Shake;
    }
}
