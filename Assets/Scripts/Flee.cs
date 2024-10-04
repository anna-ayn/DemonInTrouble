using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Flee
{
    [System.NonSerialized]
    public Kinematic character;
    
    [System.NonSerialized]
    public Kinematic target;

    public float maxAcceleration;    
    public float maxSpeed;

    protected Vector3 futureTargetPosition = Vector3.zero;

    public virtual SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        result.linear = character.position - target.position;
        result.linear += futureTargetPosition;

        // Give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
