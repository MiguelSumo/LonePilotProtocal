using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Required for AudioMixer control

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

    void Awake()
    {
        // Singleton Pattern with persistence
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

    /// <summary>
    /// Plays a specific AudioSource, stopping it first if it's already playing.
    /// </summary>
    public void PlaySound(AudioSource sound)
    {
        if (sound != null)
        {
            sound.Stop();
            sound.Play();
        }
    }

    // --- SLIDER METHODS ---
    // These convert a 0.0001 to 1 slider value into a -80 to 0 Decibel scale.
    // Ensure your Mixer parameters are named exactly: MasterVol, MusicVol, EffectsVol

    public void SetMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        masterMixer.SetFloat("EffectsVol", Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20);
    }
}