using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Enemy BlueEnemyPreFab;
    public Enemy RedEnemyPreFab;
    public Enemy AbyssalTwoa;
    public Enemy AbyssalSixc;
    public Enemy AbyssalFiveb;
    public Enemy AbyssalThreec;


    public Enemy CreateEnemy(EnemyType type, Vector3 position, WaveManager waveManager)
    {
        Enemy prefabToSpawn = null;

        switch (type)
        {
            case EnemyType.BasicBlue:
            case EnemyType.ZigZagBlue:
                prefabToSpawn = BlueEnemyPreFab;
                break;
            case EnemyType.BasicRed:
            case EnemyType.ZigZagRed:
                prefabToSpawn = RedEnemyPreFab;
                break;
            case EnemyType.Basic2a:
            case EnemyType.ZigZag2a:
                prefabToSpawn = AbyssalTwoa;
                break;
            case EnemyType.Basic6c:
            case EnemyType.ZigZag6c:
                prefabToSpawn = AbyssalSixc;
                break;
            case EnemyType.Basic5b:
            case EnemyType.ZigZag5b:
                prefabToSpawn = AbyssalFiveb;
                break;
            case EnemyType.Basic3c:
            case EnemyType.ZigZag3c:
                prefabToSpawn = AbyssalThreec;
                break;

        }

        Enemy enemy = Instantiate(prefabToSpawn, position, Quaternion.identity);

        // Set behavior based on type
        switch (type)
        {
            case EnemyType.BasicBlue:
            case EnemyType.BasicRed:
            case EnemyType.Basic2a:
            case EnemyType.Basic3c:
            case EnemyType.Basic5b:
            case EnemyType.Basic6c:
                enemy.SetTrackingType(Enemy.TrackingType.Simple);
                break;

            case EnemyType.ZigZagBlue:
            case EnemyType.ZigZagRed:
            case EnemyType.ZigZag2a:
            case EnemyType.ZigZag3c:
            case EnemyType.ZigZag5b:
            case EnemyType.ZigZag6c:
                enemy.SetTrackingType(Enemy.TrackingType.ZigZag);
                break;
        }


        
        enemy.Initialize(waveManager);

        return enemy;
    }
}

