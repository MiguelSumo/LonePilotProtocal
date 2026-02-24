using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackingStrategy
{
    void Move(Enemy enemy, Transform target, float moveSpeed);
}
