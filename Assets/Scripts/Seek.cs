using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public float maxAcceleration;
    public float maxSpeed;
    public bool escape;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    protected SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        if (escape)
            result.linear = character.kinematic.position - target.kinematic.position;
        else
            result.linear = target.kinematic.position - character.kinematic.position;

        // Give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }

    private void Update() {
        character.steering = getSteering();
        character.kinematic.updateOrientationWithCurrentVelocity();
        character.doUpdate(maxSpeed);
    }
}
