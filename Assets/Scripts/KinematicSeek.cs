using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    public Agent character;
    public Agent target;
    public float maxSpeed;
    public bool escape;
    public float maxDistance; // Distancia máxima permitida

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    KinematicSteeringOutput getSteering() {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get the direction to the target
        Vector3 direction;
        if (escape)
            direction = character.kinematic.position - target.kinematic.position;
        else
            direction = target.kinematic.position - character.kinematic.position;

        // Check if the distance is greater than the maximum allowed distance
        if (escape && direction.magnitude > maxDistance)
        {
            // Calculate the position at the maximum allowed distance in the direction of the target
            Vector3 maxDistancePosition = character.kinematic.position + direction.normalized * maxDistance;
            
            // Set the direction to this position
            direction = - maxDistancePosition + character.kinematic.position;
        }

        // The velocity is along this direction, at full speed
        result.velocity = direction.normalized * maxSpeed;

        // Face in the direction we want to move
        character.kinematic.orientation = character.kinematic.newOrientation(character.kinematic.orientation, result.velocity);
        result.rotation = 0;
        return result;
    }

    private void Update() {
        KinematicSteeringOutput steering = getSteering();
        character.kinematic.velocity = steering.velocity;
        character.doUpdate();
    }
}