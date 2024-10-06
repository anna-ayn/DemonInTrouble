using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public Face algorithm;

    private void Awake()
    {
        character.Initialize();
        target.Initialize();
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.target = target.kinematic;

        character.steering = algorithm.getSteeringFace();
        character.kinematic.orientation += character.steering.angular * Time.deltaTime;
        character.transform.rotation = Quaternion.Euler(0, 0, character.kinematic.orientation * Mathf.Rad2Deg);
    }
}
