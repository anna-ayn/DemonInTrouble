using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSearchForTrap : Transition
{
    GameObject character;

    public TransitionSearchForTrap(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        List<GameObject> traps = new List<GameObject>(GameObject.FindGameObjectsWithTag("Scroll"));

        // buscar la primera trampa cercana al personaje 
        foreach (GameObject trap in traps)
        {
            float distance = Vector3.Distance(trap.transform.position, character.transform.position);
            if (distance < 30.0f)
                return true;
        }
        return false;
    }
    public override string getTargetState()
    {
        return "SearchForTrap";
    }
    public override void getActions()
    {
        return;
    }
}