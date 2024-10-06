using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidanceMain : MonoBehaviour
{
    public Agent character;

    public CollisionAvoidance algorithm;
    
    private void Awake()
    {
        character.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;

        var steering = algorithm.getSteering();
        if (steering != null) {
            character.steering = steering;
            character.doUpdate(algorithm.maxSpeed);
        }
    }
}
