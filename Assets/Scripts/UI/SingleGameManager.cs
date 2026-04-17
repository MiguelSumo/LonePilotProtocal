using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleGameManager : MonoBehaviour
{
    public static SingleGameManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject menuContainer;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopContainer;

    // Track the currently open panel so we can close it automatically
    private GameObject _currentActivePanel;

    private void Awake()
    {
        // --- Singleton Pattern ---
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure we start fresh
        CloseAllPanels();
    }

    // --- Universal Navigation ---

    // Pass the specific panel (Shop, Settings, etc.) via the Unity Inspector Button
    public void OpenPanel(GameObject panelToOpen)
    {
        if (panelToOpen != null && menuContainer != null)
        {
            // Hide the main menu
            menuContainer.SetActive(false);

            // Hide any other panel that might be open
            if (_currentActivePanel != null) _currentActivePanel.SetActive(false);

            // Open the new panel and track it
            panelToOpen.SetActive(true);
            _currentActivePanel = panelToOpen;

            Debug.Log($"Navigated to: {panelToOpen.name}");
        }
    }

    // Use this for ALL "Back" or "X" buttons
    public void ReturnToMainMenu()
    {
        // Close whatever is currently open
        if (_currentActivePanel != null)
        {
            _currentActivePanel.SetActive(false);
            _currentActivePanel = null;
        }

        // Show the main menu
        if (menuContainer != null) menuContainer.SetActive(true);

        Debug.Log("Returning to Main Menu.");
    }

    private void CloseAllPanels()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (shopContainer != null) shopContainer.SetActive(false);

        // Reset our tracker
        _currentActivePanel = null;

        // Ensure the main menu is the only thing visible
        if (menuContainer != null) menuContainer.SetActive(true);
    }

    // --- Scene Actions ---

    public void StartGame()
    {
        Debug.Log("Starting Solo Pilot Protocol...");
        // Ensure "GameScene" is added to your Build Settings!
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();
    }
}