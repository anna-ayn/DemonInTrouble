using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VelocityMatch
{
    [System.NonSerialized]
    public Kinematic character;

    [System.NonSerialized]
    public Kinematic target;

    public float maxAcceleration;
    // the time over which to achieve target speed
    public float timeToTarget = 0.1f;
    public float maxSpeed;

    public virtual SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // acceleration tries to get to the target velocity
        result.linear = target.velocity - character.velocity;
        result.linear /= timeToTarget;

        // check if the acceleration is too fast
        if (result.linear.magnitude > maxAcceleration) {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }

        result.angular = 0;
        return result;
    }
}
