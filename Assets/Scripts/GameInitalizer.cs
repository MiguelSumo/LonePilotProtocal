using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(InitializeGameSequence());
    }

    IEnumerator InitializeGameSequence()
    {
        // 1. Load Background/Environment
        LoadEnvironment();
        yield return new WaitForSeconds(0.5f); // Half-second breather

        // 2. Load UI Systems
        LoadUI();
        yield return new WaitForSeconds(0.5f);

        // 3. Spawn Player/Enemies
        SpawnEntities();

        Debug.Log("Initialization Complete");
    }

    void LoadEnvironment() { /* Your Logic */ }
    void LoadUI() { /* Your Logic */ }
    void SpawnEntities() { /* Your Logic */ }
}