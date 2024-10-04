using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LookWhereYoureGoing : Align
{
    public override SteeringOutput getSteering() {
        // 1. calculate the target to delegate to align
        // check for a zero direction, and make no change if so
        Vector3 velocityLWYG = character.velocity;
        if (velocityLWYG.magnitude > 0) {
            // set the target based on the velocity
            orientationFace = Mathf.Atan2(-velocityLWYG.x, velocityLWYG.y);
        }

        // 2. delegate to align
        return base.getSteering();
    }
}
