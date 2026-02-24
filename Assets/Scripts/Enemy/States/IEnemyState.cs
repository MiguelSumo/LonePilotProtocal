using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public interface IEnemyState
{
    void Enter(Enemy enemy);
    void Update(Enemy enemy);
    void Exit(Enemy enemy);
}
