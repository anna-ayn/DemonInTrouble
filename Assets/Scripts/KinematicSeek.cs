using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    public Agent character;
    public Agent target;
    public float maxSpeed;
    public bool escape;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    KinematicSteeringOutput getSteering() {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get the direction to the target
        if (escape)
            result.velocity = character.kinematic.position - target.kinematic.position;
        else
            result.velocity = target.kinematic.position - character.kinematic.position;

        // The velocity is along this direction, at full speed
        result.velocity.Normalize();
        result.velocity *= maxSpeed;

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