using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMain : MonoBehaviour
{
    public Agent character;
    public Agent target;
    
    public Wander algorithm;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        character.steering = algorithm.getSteeringWander();
        character.doUpdate(algorithm.maxSpeed);
    }
}
