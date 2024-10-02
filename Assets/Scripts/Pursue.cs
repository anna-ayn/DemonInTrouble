using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    // the maximum prediction time
    public float maxPrediction;

    //  # ...Other data is derived from the superclass...

    SteeringOutput getSteeringPursue() {
        // 1. calculate the target to delegate to seek
        // work out the distance to target
        Vector3 direction = target.kinematic.position - character.kinematic.position;

        float distance = direction.magnitude;

        // work out our current speed
        float speed = character.kinematic.velocity.magnitude;

        float prediction;
        // check if speed gives a reasonable prediction time
        if (speed <= distance / maxPrediction) {
            prediction = maxPrediction;
        }
        // otherwise calculate the prediction time
        else {
            prediction = distance / speed;
        }

        // put the target together
        target.kinematic.position += target.kinematic.velocity * prediction;

        // 2. Delegate to seek
        return base.getSteering();
    }

    private void Update() {
        character.steering = getSteeringPursue();
        character.kinematic.updateOrientationWithCurrentVelocity();
        character.doUpdate(maxSpeed);
    }
}
