using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Evade : Flee
{
    // The maximum prediction time.
    public float maxPrediction;

    // ...Other data is derived from the superclass...

    public override SteeringOutput getSteering() {
        // 1. calculate the target to delegate to seek
        // work out the distance to target
        Vector3 direction = target.position - character.position;
        float distance = direction.magnitude;

        // work out our current speed
        float speed = character.velocity.magnitude;

        float prediction;
        // check if speed gives a reasonable prediction time
        if (speed <= distance / maxPrediction)
            prediction = maxPrediction;
        else //  # otherwise calculate the prediction time.
            prediction = distance / speed;

        // put the target together
        futureTargetPosition = target.velocity * prediction;

        // 2. delegate to seek
        return base.getSteering();
    }
}
