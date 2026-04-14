using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Enemy BlueEnemyPreFab;
    public Enemy RedEnemyPreFab;

    public Enemy CreateEnemy(EnemyType type, Vector3 position, WaveManager waveManager)
    {
        Enemy prefabToSpawn = null;

        switch (type)
        {
            case EnemyType.BasicBlue:
                prefabToSpawn = BlueEnemyPreFab;
                break;
            case EnemyType.BasicRed:
                prefabToSpawn = RedEnemyPreFab;
                break;

            case EnemyType.ZigZagBlue:
                prefabToSpawn = BlueEnemyPreFab;
                break;

            case EnemyType.ZigZagRed:
                prefabToSpawn = RedEnemyPreFab;
                break;
        }

        Enemy enemy = Instantiate(prefabToSpawn, position, Quaternion.identity);

        // Set behavior based on type
        switch (type)
        {
            case EnemyType.BasicBlue:
            case EnemyType.BasicRed:
                enemy.SetTrackingType(Enemy.TrackingType.Simple);
                break;

            case EnemyType.ZigZagBlue:
            case EnemyType.ZigZagRed:
                enemy.SetTrackingType(Enemy.TrackingType.ZigZag);
                break;
        }


        
        enemy.Initialize(waveManager);

        return enemy;
    }
}
