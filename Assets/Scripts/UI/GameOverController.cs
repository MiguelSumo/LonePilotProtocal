using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text currentWaveText;


    void Start()
    {   
        scoreText.text = $"{GameData.currentScore}";
        currentWaveText.text = $"{GameData.waveNumber}";

    }


    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
