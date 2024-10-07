using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Face : Align
{
    // for Wander
    protected Vector3 wanderTarget = Vector3.zero;

    public virtual SteeringOutput getSteeringFace() {
        // 1. calculate the target to delegate to align
        // work out the direction to target
        Vector3 direction;
        if (wanderTarget.magnitude == 0)
            direction = target.position - character.position;
        else
            direction = wanderTarget - character.position;

        // Check for a zero direction, and make no change if so
        if (direction.magnitude != 0) {
            // 2. delegate to align
            orientationFace = Mathf.Atan2(-direction.x, direction.y);
        } else {
            SteeringOutput result = new SteeringOutput();
            result.linear = Vector3.zero;
            result.angular = 0.0f;
            return result;
        }

        return base.getSteering();
    }
}
