using UnityEngine;

[CreateAssetMenu(menuName = "Economy/Currency Data")]
public class CurrencyDataSO : ScriptableObject
{
    [Header("Economy")]
    public int TotalCoins;

    [Header("Records")]
    public int HighScore;

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
    }

    public void SubtractCoins(int amount)
    {
        TotalCoins -= amount;
    }

    public void ResetCoins()
    {
        TotalCoins = 0;
    }

    // Call this at the end of a run or every time the score increases
    public void CheckAndSetHighScore(int currentScore)
    {
        if (currentScore > HighScore)
        {
            HighScore = currentScore;
            Debug.Log($"New High Score! {HighScore}");
        }
    }
}
