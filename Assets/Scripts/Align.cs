using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Align
{
    [System.NonSerialized]
    public Kinematic character;

    [System.NonSerialized]
    public Kinematic target;

    public float maxAngularAcceleration;
    public float maxRotation;

    // The radius for arriving at the target
    public float targetRadius;
    // the radius for beginning to slow down
    public float slowRadius;
    // the time over which to achieve target speed
    public float timeToTarget = 0.1f;

    protected float orientationFace = 0;

    public float mapToRange(float angle) {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;
        return angle;
    }

    public virtual SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        float rotation;
        // get the naive direction to the target
        if (orientationFace == 0) 
            rotation = (target.orientation - character.orientation) * Mathf.Rad2Deg;
        else 
            rotation = (orientationFace - character.orientation) * Mathf.Rad2Deg;

        // map the result to the (-pi, pi) interval
        rotation = mapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);

        if (rotationSize < targetRadius) {
            return null;
        }

        float targetRotation = 0;
        // if we are outside the slowRadius, then use maximum rotation
        if (rotationSize > slowRadius) {
            targetRotation = maxRotation;
        }
        // otherwise calculate a scaled rotation
        else {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        // the final target rotation combines speed (already in the variable) and direction
        if (rotationSize > 0) { // prevent division by zero
            targetRotation *= rotation / rotationSize;
        }

        // acceleration tries to get to the target rotation
        result.angular = targetRotation - character.rotation;
        result.angular /= timeToTarget;

        // check if the acceleration is too great
        float angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration) {
            result.angular /= angularAcceleration;
            result.angular *= maxAngularAcceleration;
        }

        result.linear = Vector3.zero;
        return result;
    }

}
