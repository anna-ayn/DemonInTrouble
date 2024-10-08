using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public Align algorithm;
    
    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        character.steering = algorithm.getSteering();
        character.doUpdate();
    }
}
