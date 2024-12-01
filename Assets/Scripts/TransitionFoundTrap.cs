using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFoundTrap: Transition
{
    GameObject character;

    public TransitionFoundTrap(GameObject character)
    {
        this.character = character;
    }

    public override bool isTriggered()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Scroll");
        for (int i = 0; i < traps.Length; i++)
        {
            float distance = Vector3.Distance(traps[i].transform.position, character.transform.position);
            Debug.Log("Distance: " + distance);
            if (distance <= 5.0f)
            {
                return true;
            }
        }
        return false;
    }
    public override string getTargetState()
    {
        return "FoundTrap";
    }
    public override void getActions()
    {
        return;
    }
}