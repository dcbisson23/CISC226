using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource mainTheme;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 1;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
