using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoingMain : MonoBehaviour
{
    public Agent character;
    public Agent target;

    public LookWhereYoureGoing algorithm;

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
            character.transform.rotation = Quaternion.Euler(0, 0, character.kinematic.orientation * Mathf.Rad2Deg);
            character.kinematic.orientation += steering.angular * Time.deltaTime;
        }
    }
}
