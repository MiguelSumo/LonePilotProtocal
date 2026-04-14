using UnityEngine;

public interface IGameEntityFactory
{
    void CreateAsteroid(Vector3 position, Vector3 direction);
    void CreateEnemy(EnemyType type,Vector3 spawnPos, WaveManager waveManager);

    void CreateBullet(Vector3 position, Quaternion rotation);

}