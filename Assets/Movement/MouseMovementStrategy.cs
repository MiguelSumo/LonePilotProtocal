using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementStrategy : IMovementStrategy
{
    //<variables>

    //<methods>

    public void Move(Transform transform, float speed)
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Set a fixed distance from the camera
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPos.z = 0;

        //Rotate to face the mouse(Steering)
        Vector3 direction = targetPos - transform.position;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust for sprite orientation
        }

        // Move towards the mouse position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }

    //<constructors>
}

