using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Kinematic kinematic;
    public SteeringOutput steering;

    public void updateKinematicWithTransform() {
        // get the position with transform.position
        kinematic.position = transform.position;
        // transform.eulerAngles.z gives the z-axis roation in euler angles
        // then we convert it in radians
        kinematic.orientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    public void Initialize()
    {
        updateKinematicWithTransform();
    }

    public void updateTransform()
    {
        // Update transform.position and transform.rotation after updating the kinematic
        transform.position = kinematic.position;
        transform.rotation = Quaternion.Euler(0, 0, kinematic.orientation * Mathf.Rad2Deg);
    }

    // Time.deltaTime represents the time in seconds that has elapsed since the last frame was rendered
    public void doUpdate()
    {
        kinematic.doUpdate(steering, Time.deltaTime);
        updateTransform();
    }

    public void doUpdate(float maxSpeed) 
    {
        kinematic.doUpdate(steering, maxSpeed, Time.deltaTime);
        updateTransform();
    }

}
