using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationMain : MonoBehaviour
{
    public Agent character;

    public Separation algorithm;
    
    private void Awake()
    {
        character.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;

        character.steering = algorithm.getSteering();
        character.steering.linear *= -1;
        character.doUpdate(algorithm.maxSpeed);
    }
}
