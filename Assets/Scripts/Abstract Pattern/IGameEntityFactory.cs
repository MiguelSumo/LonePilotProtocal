using UnityEngine;

public interface IGameEntityFactory
{
    void CreateAsteroid(Vector3 position, Vector3 direction);
    void CreateEnemy(Vector3 position);

    void CreateBullet(Vector3 position, Quaternion rotation);

}