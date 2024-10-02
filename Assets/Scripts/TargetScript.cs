using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Agent target;
    public float maxSpeed;

    private void Update()
    {
        KinematicSteeringOutput move = new KinematicSteeringOutput();
        move.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (move.velocity != Vector3.zero) {
            move.velocity.Normalize();
            move.velocity *= maxSpeed;
            move.rotation = 0;

            target.kinematic.velocity = move.velocity;
            target.doUpdate();
            target.kinematic.orientation = target.kinematic.newOrientation(transform.rotation.eulerAngles.z, move.velocity);

            
        }

    }
}
