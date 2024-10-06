using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionAvoidance
{
    [System.NonSerialized]
    public Kinematic character;

    public float maxAcceleration;    
    public float maxSpeed;

    // a list of potential targets
    public List<Agent> targets;
    
    // the collision radius of a character (assuming
    // all characters have the same radius here)
    public float radius;

    public virtual SteeringOutput getSteering() {
        // 1. find the target that's closest to collision
        // store the first collision time
        float shortestTime = float.PositiveInfinity;
        Debug.Log(shortestTime);

        // store the target that collides them, and
        // other data that we will need and can avoid
        // recalculating
        Kinematic firstTarget = null;
        float firstMinSeparation = float.PositiveInfinity, firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.zero, firstRelativeVel = Vector3.zero;

        Vector3 relativePos;

        // loop through each target
        foreach (Agent t in targets)
        {
            // calculate the time to collision
            relativePos = t.kinematic.position - character.position;
            Vector3 relativeVel = t.kinematic.velocity - character.velocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            // check if it is going to be a collision at all
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2*radius) 
                continue;

            // check if it is the shortest
            if (timeToCollision > 0 && timeToCollision < shortestTime) {
                // store the time, target and other data
                shortestTime = timeToCollision;
                firstTarget = t.kinematic;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }    
        }
        // 2. calculate the steering
        // if we have no target, then exit
        if (firstTarget == null) {
            return null;
        } 

        // if we're going to hit exactly, or if we're already
        // colliding, then do the steering based of current position
        if (firstMinSeparation <= 0 || firstDistance < 2 * radius) {
            relativePos = firstTarget.position - character.position;
        }
        // otherwise calculate the future relative position
        else {
            relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        }

        // avoid the target
        relativePos.Normalize();
        SteeringOutput result = new SteeringOutput();
        result.linear = relativePos * maxAcceleration;
        result.angular = 0;
        return result;
    }
}
