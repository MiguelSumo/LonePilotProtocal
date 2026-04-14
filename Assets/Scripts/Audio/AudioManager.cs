using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource ice;
    public AudioSource enemyDeath;
    public AudioSource enemyAttackSound;
    public AudioSource asteriodHitSound;
    public AudioSource enemyHitSound;

    public AudioSource asteriodExplosion;
    public AudioSource bulletSound;
    public AudioSource pauseSound;
    public AudioSource unPauseSound;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
}
