using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public Agent character;
    public Agent target;
    public float maxAcceleration;
    public float maxSpeed;

    // the radius for arriving at the target
    public float targetRadius;
    // the radius for beginning to slow down
    public float slowRadius;
    // the time over which to achieve target speed
    public float timeToTarget = 0.1f;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        Vector3 direction = target.kinematic.position - character.kinematic.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < targetRadius) {
            return null;
        }

        float targetSpeed;

        // if we are outside the slowRadius then move at max speed
        if (distance > slowRadius) 
            targetSpeed = maxSpeed;
        // otherwise calculate a scaled speed
        else 
            targetSpeed = maxSpeed * distance / slowRadius;
        
        // the target velocity combines speed and direction
        Vector3 targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        // acceleration tries to get to the target velocity
        result.linear = targetVelocity - character.kinematic.velocity;
        result.linear /= timeToTarget;

        // check if the acceleration is too fast
        if (result.linear.magnitude > maxAcceleration) {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }

        result.angular = 0;
        return result;
    }
    
    // Update is called once per frame
    void Update()
    { 
        var steering = getSteering();
        if (steering != null) {
            character.steering = getSteering();
            character.kinematic.updateOrientationWithCurrentVelocity();
            character.doUpdate(maxSpeed);
        }
    }
}
