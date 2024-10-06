using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Separation
{
    [System.NonSerialized]
    public Kinematic character;

    public float maxAcceleration;    
    public float maxSpeed;

    // a list of potential targets
    public List<Agent> targets;
    
    // the threshold to take action
    public float threshold;

    // the constant coefficient of decay for
    // the inverse square law
    public float decayCoefficient;

    public virtual SteeringOutput getSteering() {
        SteeringOutput result = new SteeringOutput();

        // loop through each target
        foreach (Agent t in targets)
        {
            // check if the target is close
            Vector3 direction = t.kinematic.position - character.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                // Calculate the strength of repulsion
                // (here using the inverse square law)
                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);

                // Add the acceleration
                direction.Normalize();
                result.linear += strength * direction;
            } else {
                result.linear += Vector3.zero;
            }
        }
        return result;
    }
}
