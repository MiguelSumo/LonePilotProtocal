using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleGameManager : MonoBehaviour
{
    public static SingleGameManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject menuContainer;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopContainer;

    [Header("Economy System")]
    [SerializeField] private int totalCoins = 0;
    public int TotalCoins => totalCoins;

    private GameObject _currentActivePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CloseAllPanels();
    }

    // --- Economy Logic ---

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log($"Coins Collected! New Balance: {totalCoins}");
    }

    public bool TrySpendCoins(int cost)
    {
        if (totalCoins >= cost)
        {
            totalCoins -= cost;
            Debug.Log($"Spent {cost} coins. Remaining: {totalCoins}");
            return true;
        }

        Debug.Log("Not enough coins!");
        return false;
    }

    // --- Navigation Logic ---

    public void OpenPanel(GameObject panelToOpen)
    {
        if (panelToOpen != null && menuContainer != null)
        {
            menuContainer.SetActive(false);
            if (_currentActivePanel != null) _currentActivePanel.SetActive(false);
            panelToOpen.SetActive(true);
            _currentActivePanel = panelToOpen;
        }
    }

    public void ReturnToMainMenu()
    {
        if (_currentActivePanel != null)
        {
            _currentActivePanel.SetActive(false);
            _currentActivePanel = null;
        }
        if (menuContainer != null) menuContainer.SetActive(true);
    }

    private void CloseAllPanels()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (shopContainer != null) shopContainer.SetActive(false);
        _currentActivePanel = null;
        if (menuContainer != null) menuContainer.SetActive(true);
    }

    public void StartGame() => SceneManager.LoadScene("GameScene");

    public void QuitGame() => Application.Quit();

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}