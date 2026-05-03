using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreEvent;

    public static ScoreManager Instance;

    private int score;

    // Property so the Shop can check if we have enough money
    public int CurrentScore => score;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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

    // NEW: Method to spend points in the shop
    public void SubtractScore(int amount)
    {
        score -= amount;
        // Make sure it doesn't go below zero
        if (score < 0) score = 0;

        OnScoreChanged?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}