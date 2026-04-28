using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer Settings")]
    [Tooltip("Drag your Master Audio Mixer asset here")]
    public AudioMixer masterMixer;

    [Header("Audio Sources")]
    public AudioSource ice;
    public AudioSource enemyDeath;
    public AudioSource enemyAttackSound;
    public AudioSource asteriodHitSound;
    public AudioSource enemyHitSound;
    public AudioSource asteriodExplosion;
    public AudioSource bulletSound;
    public AudioSource pauseSound;
    public AudioSource unPauseSound;
    public AudioSource backgroundMusic;

    // These variables allow the Pause Menu to "remember" the volume levels
    [HideInInspector] public float masterVal = 1f, musicVal = 1f, sfxVal = 1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(AudioSource sound)
    {
        if (sound != null)
        {
            sound.Stop();
            sound.Play();
        }
    }

    // --- CONSOLIDATED SLIDER METHODS ---
    // These convert 0.0001-1 into -80 to 0 Decibels AND save the value for the Pause Menu.

    public void SetMasterVolume(float sliderValue)
    {
        masterVal = sliderValue; // Saves for Pause Menu
        masterMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicVal = sliderValue; // Saves for Pause Menu
        masterMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        sfxVal = sliderValue; // Saves for Pause Menu
        masterMixer.SetFloat("EffectsVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }
}