using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kinematic 
{
    public Vector3 position;
    public float orientation; // angle from z-axis
    public Vector3 velocity;
    public float rotation;

    public float newOrientation(float current, Vector3 velocity)
    {
        // Make sure we have a velocity.
        if (velocity.magnitude > 0)
            return Mathf.Atan2(-velocity.x, velocity.y);
        //Otherwise use the current orientation.
        return current;
    }

    public void updateOrientationWithCurrentVelocity() 
    {
        if (velocity.magnitude > 0) 
            orientation = Mathf.Atan2(-velocity.x, velocity.y);
    }

    public void doUpdate(SteeringOutput steering, float time)
    {
        // update the position and orientation
        position += velocity * time;
        orientation += rotation * time; 

        // and the velocity and rotation
        velocity += steering.linear * time;
        rotation += steering.angular * time;
    }

    public void doUpdate(SteeringOutput steering, float maxSpeed, float time) 
    {
        // update the position and orientation
        position += velocity * time;
        orientation += rotation * time; 

        // and the velocity and rotation
        velocity += steering.linear * time;
        rotation += steering.angular * time;

        // check for speeding and clip
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }  
}