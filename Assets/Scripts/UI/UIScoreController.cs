using TMPro;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;

            // immediate sync when panel becomes active
            UpdateScoreUI(ScoreManager.Instance.GetScore());
        }
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
        }
    }

    private void UpdateScoreUI(int totalScore)
    {
        if (scoreText == null) return;

        scoreText.text = $"Points: {totalScore}";
    }
}