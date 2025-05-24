using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource audiosource;
    public Slider slider;
    public static float vol = 1;
    void Start()
    {
        audiosource.volume = vol;
        slider.value = vol;
    }

    // Update is called once per frame
    void Update()
    {
        vol = audiosource.volume;
        slider.value = vol;
    }
}
