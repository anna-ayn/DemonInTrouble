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
        character.kinematic.orientation += character.kinematic.rotation * Time.deltaTime;
        character.kinematic.rotation += character.steering.angular * Time.deltaTime;
        character.transform.rotation = Quaternion.Euler(0, 0, character.kinematic.orientation * Mathf.Rad2Deg);
    }
}
