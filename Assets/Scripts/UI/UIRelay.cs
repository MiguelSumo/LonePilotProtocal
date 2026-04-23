using UnityEngine;

public class UIRelay : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel; // Drag your Settings Panel here in the Inspector

    public void OpenSettingsMenu()
    {
        if (SingleGameManager.Instance != null && settingsPanel != null)
        {
            // We call your specific OpenPanel function
            SingleGameManager.Instance.OpenPanel(settingsPanel);
        }
        else
        {
            Debug.LogWarning("UIRelay: Missing Manager Instance or Settings Panel reference!");
        }
    }

    public void QuitToMainMenu()
    {
        if (SingleGameManager.Instance != null)
        {
            // You can use SceneManager directly here, or if you prefer 
            // calling your manager, we'll use UnityEngine's library
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}