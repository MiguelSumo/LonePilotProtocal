using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleGameManager : MonoBehaviour
{
    public static SingleGameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject settingsPanel;   // Drag 'settingsPanel' here
    [SerializeField] private GameObject menuContainer;   // Drag 'MenuContainer' here

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initial UI State: Show Menu, Hide Settings
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (menuContainer != null) menuContainer.SetActive(true);
    }

    // --- Navigation Functions ---

    public void OpenSettings()
    {
        if (settingsPanel != null && menuContainer != null)
        {
            Debug.Log("Switching to Settings...");
            menuContainer.SetActive(false); // Hide Main Menu
            settingsPanel.SetActive(true);  // Show Settings
        }
        else
        {
            Debug.LogWarning("Missing UI References in SingleGameManager!");
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null && menuContainer != null)
        {
            Debug.Log("Returning to Main Menu...");
            settingsPanel.SetActive(false); // Hide Settings
            menuContainer.SetActive(true);  // Show Main Menu
        }
    }

    // --- Action Functions ---

    public void StartGame()
    {
        Debug.Log("Starting Solo Pilot Protocol...");
        SceneManager.LoadScene("GameScene");
    }

    public void OpenShop()
    {
        Debug.Log("Opening Shop...");
        // Future logic for ship upgrades goes here!
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}