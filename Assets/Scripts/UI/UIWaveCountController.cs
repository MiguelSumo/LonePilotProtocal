using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveCountController : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO waveEvent;
    [SerializeField] private TMP_Text waveText;

    private int displayedScore;

    private void OnEnable()
    {
        waveEvent.OnEventRaised += UpdateWaveUI;
    }

    private void OnDisable()
    {
        waveEvent.OnEventRaised -= UpdateWaveUI;
    }

    private void UpdateWaveUI(int amount)
    {
        //displayedScore += amount;
        waveText.text = $"Wave: {amount}";
    }
}
