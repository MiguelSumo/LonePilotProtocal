using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleGameManager : MonoBehaviour
{
    public static SingleGameManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject menuContainer;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopContainer; // Drag ShopContainer here

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Standardized Reset
        CloseAllPanels();
    }

    // --- Universal Navigation ---

    // Use this for any button that opens a specific menu (Shop, Settings, etc.)
    public void OpenPanel(GameObject panelToOpen)
    {
        if (panelToOpen == null)
        {
            return;
        }

        CloseAllPanels();

        if (panelToOpen != menuContainer && menuContainer != null)
        {
            menuContainer.SetActive(false);
        }

        panelToOpen.SetActive(true);
        Debug.Log("Switched to " + panelToOpen.name);
    }

    // Use this for ALL "Back" or "X" buttons to return to the Main Menu
    public void ReturnToMainMenu()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (shopContainer != null) shopContainer.SetActive(false);

        if (menuContainer != null) menuContainer.SetActive(true);
        Debug.Log("Returning to Main Menu.");
    }

    private void CloseAllPanels()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (shopContainer != null) shopContainer.SetActive(false);
        if (menuContainer != null) menuContainer.SetActive(true);
    }

    // --- Scene Actions ---

    public void StartGame()
    {
        Debug.Log("Starting Solo Pilot Protocol...");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main Menu");
    }
}