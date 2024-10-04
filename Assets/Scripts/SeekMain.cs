using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public Seek algorithm;
    
    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        character.steering = algorithm.getSteering();
        character.doUpdate(algorithm.maxSpeed);
    }
}
