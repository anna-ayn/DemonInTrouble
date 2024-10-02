using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    //  # ...Data is derived from the superclass...

    SteeringOutput getSteeringAlign() {
        // 1. calculate the target to delegate to align
        // work out the direction to target
        Vector3 direction = target.kinematic.position - character.kinematic.position;

        // check for a zero direction, and make no change if so
        if (direction.magnitude == 0) {
            return null;
        }

        // 2. delegate to align
        target.kinematic.orientation = Mathf.Atan2(-direction.x, direction.y);

        return getSteering();
    }

    private void Update() {
        var steering = getSteeringAlign();
        if (steering != null) {
            character.steering = steering;
            character.doUpdate();
        }
    }
}
