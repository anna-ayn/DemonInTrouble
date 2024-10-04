using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public Arrive algorithm;
    
    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        var steering = algorithm.getSteering();
        if (steering != null) {
            character.steering = steering;
            character.doUpdate(algorithm.maxSpeed);
        }
    }
}
