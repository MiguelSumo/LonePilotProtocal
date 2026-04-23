using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SingleGameManager : MonoBehaviour
{
    public static SingleGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // --- Title Screen Functions ---

    public void StartGame()
    {
        Debug.Log("Starting Solo Pilot Protocol...");
        // Replace "GameScene" with the actual name of your play level
        SceneManager.LoadScene("GameScene");
    }

    public void OpenShop()
    {
        Debug.Log("Opening Shop...");
        // You could load a Shop scene or just enable a Shop UI Panel
    }

    public void OpenSettings()
    {
        Debug.Log("Opening Settings...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main Menu");
    }
}