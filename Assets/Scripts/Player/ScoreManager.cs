using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreEvent;

    public static ScoreManager Instance;

    private int score;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        Instance = this;
    }

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

        OnScoreChanged?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}