using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreEvent;

    private int score;

    private void OnEnable()
    {
        scoreEvent.OnEventRaised += AddScore;
    }

    private void OnDisable()
    {
        scoreEvent.OnEventRaised -= AddScore;
    }

    private void AddScore(int amount)
    {
        score += amount;
    }
}
