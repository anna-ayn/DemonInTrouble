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
    public float maxDistance; // Distancia máxima permitida

    protected Vector3 futureTargetPosition = Vector3.zero;

    public virtual SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        result.linear = character.position - target.position;

        // Check if the character is beyond the maximum distance
        if (result.linear.magnitude > maxDistance) {
            // Calculate the position at the maximum allowed distance in the direction of the target
            Vector3 maxDistancePosition = character.position + result.linear.normalized * maxDistance;
            
            // Set the direction to this position
            result.linear = - maxDistancePosition + character.position;
        } else {
            // Give full acceleration along this direction
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }
        result.linear += futureTargetPosition;

        result.angular = 0;
        return result;
    }
}
