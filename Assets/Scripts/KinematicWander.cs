using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWander : MonoBehaviour
{
    public Agent character;
    public float maxSpeed;
    
    // The maximum rotation speed we'd like, probably should be
    // smaller than the maximum possible, for a leisurely change
    // in direction
    public float maxRotation;

    private void Awake()
    {
        character.Initialize();
    }

    // get a random number between -1 and 1
    float randomBinomial()
    {
        return Random.Range(-1.0f, 1.0f);
    }

    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get velocity from vector form of orientation
        float angle = character.kinematic.orientation;
        Vector3 vec = new Vector3(0.0f, 0.0f, 0.0f);
        vec.x = - (float) -Mathf.Sin(angle);
        vec.y = (float) Mathf.Cos(angle);
        result.velocity = maxSpeed * vec;

        // Change orientation randomly
        result.rotation = randomBinomial() * maxRotation;
        
        return result;
    }

    void Update()
    {
        KinematicSteeringOutput steering = getSteering();
        character.kinematic.velocity = steering.velocity;
        character.kinematic.rotation = steering.rotation;
        character.doUpdate();
    }
}