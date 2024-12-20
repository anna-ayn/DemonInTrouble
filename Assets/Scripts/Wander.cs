using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wander : Face
{
    // the radius and forward offset of the wander circle
    public float wanderOffset;
    public float wanderRadius;

    // the maximum rate at which the wander orientation can change
    public float wanderRate;

    // the current orientation of the wander target
    private float wanderOrientation;

    // the maximum acceleration of the character
    public float maxAcceleration;
    public float maxSpeed;

    // ...Other data is derived from the superclass...

    // get a random number between -1 and 1
    float randomBinomial()
    {
        return Random.Range(-1.0f, 1.0f);
    }

    Vector3 AsVector(float angle)
    {
        Vector3 result = new Vector3(- (float) Mathf.Sin(angle), (float) Mathf.Cos(angle), 0.0f);
        return result;
    }

    public virtual SteeringOutput getSteeringWander() {
        SteeringOutput result = new SteeringOutput();

        // 1. calculate the target to delegate to face
        // update the wander orientation
        wanderOrientation += randomBinomial() * wanderRate;

        // calculate the combined target orientation
        float targetOrientation = wanderOrientation + (character.orientation * Mathf.Rad2Deg);
        
        // calculate the center of the wander circle.
        wanderTarget = character.position + wanderOffset * AsVector(character.orientation);

        // calculate the target location
        wanderTarget += wanderRadius * AsVector(targetOrientation * Mathf.Deg2Rad);

        // 2. delegate to face
        result = base.getSteeringFace();

        // 3. now set the linear acceleration to be at full 
        // acceleration in the direction of the orientation
        result.linear = maxAcceleration * AsVector(character.orientation);
        
        return result;
    
    }
}
