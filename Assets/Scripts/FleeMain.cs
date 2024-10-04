using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public Flee algorithm;
    
    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        character.steering = algorithm.getSteering();
        character.kinematic.updateOrientationWithCurrentVelocity();
        character.doUpdate(algorithm.maxSpeed);
    }
}
