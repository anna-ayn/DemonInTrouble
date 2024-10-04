using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Agent target;
    public float maxSpeed;

    private void Update()
        {
            target.kinematic.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            target.kinematic.velocity.Normalize();
            target.kinematic.velocity *= maxSpeed;

            target.kinematic.updateOrientationWithCurrentVelocity();
            target.doUpdate();
        }
}
