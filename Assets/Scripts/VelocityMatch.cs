using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public float maxAcceleration;
    // the time over which to achieve target speed
    public float timeToTarget = 0.1f;
    public float maxSpeed;
    

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // acceleration tries to get to the target velocity
        result.linear = target.kinematic.velocity - character.kinematic.velocity;
        result.linear /= timeToTarget;

        // check if the acceleration is too fast
        if (result.linear.magnitude > maxAcceleration) {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }

        result.angular = 0;
        return result;
    }

    private void Update() {
        character.steering = getSteering();
        character.kinematic.updateOrientationWithCurrentVelocity();
        character.doUpdate(maxSpeed);
    }
}
