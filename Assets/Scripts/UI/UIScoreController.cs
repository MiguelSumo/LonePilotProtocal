using TMPro;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreEvent;
    [SerializeField] private TMP_Text scoreText;

    private int displayedScore;

    private void OnEnable()
    {
        scoreEvent.OnEventRaised += UpdateScoreUI;
    }

    private void OnDisable()
    {
        scoreEvent.OnEventRaised -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int amount)
    {
        displayedScore += amount;
        scoreText.text = $"Score: {displayedScore}";
    }
}