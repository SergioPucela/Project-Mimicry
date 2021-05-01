using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private static float currentSliderValue = 0.7f;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = currentSliderValue;
    }

    public void SetVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("AudioVolume", Mathf.Log10(sliderValue) * 20);

        currentSliderValue = sliderValue;
    }
}
