using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    public Agent character;
    public Agent target;
    public float maxSpeed;

    // Satisfaction radius
    public float radius;

    // Time to target
    public float timeToTarget = 0.25f;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    KinematicSteeringOutput getSteering() {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get the direction to the target
        result.velocity = target.kinematic.position - character.kinematic.position;

        // Check if we are within radius
        if (result.velocity.magnitude < radius)
            // Request no steering
            return null;

        // We need to move to our target, we'd like to get
        // there in timeToTarget seconds
        result.velocity /= timeToTarget;

        // If this is too fast, clip it to the max speed
        if (result.velocity.magnitude > maxSpeed){
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }

        // Face in the direction we want to move
        character.kinematic.orientation = character.kinematic.newOrientation(character.kinematic.orientation, result.velocity);

        result.rotation = 0;
        return result;
    }
    
    // Update is called once per frame
    void Update()
    {
        var steering = getSteering();
        if (steering != null) {
            character.kinematic.velocity = steering.velocity;
            character.doUpdate();
        }
    }
}