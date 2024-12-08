using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girarScript : MonoBehaviour
{
    public Agent agent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.kinematic.velocity.magnitude > 0.1) {
            transform.rotation = Quaternion.LookRotation(agent.kinematic.velocity);
        }
    }
}
