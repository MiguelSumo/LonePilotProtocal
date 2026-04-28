using UnityEngine;
using UnityEngine.UI;

public class PauseMenuAudioBridge : MonoBehaviour
{
    [Header("Sliders from Pause Menu UI")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            // 1. Set the sliders to match the current volume in the Manager
            masterSlider.value = AudioManager.Instance.masterVal;
            musicSlider.value = AudioManager.Instance.musicVal;
            sfxSlider.value = AudioManager.Instance.sfxVal;

            // 2. Add Listeners so moving the slider calls the Manager
            masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    void OnDisable()
    {
        // 3. Always remove listeners when the menu closes to prevent memory leaks
        if (AudioManager.Instance != null)
        {
            masterSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMasterVolume);
            musicSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSFXVolume);
        }
    }
}