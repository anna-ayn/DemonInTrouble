using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArrivedHome : Transition
{
    GameObject character;

    public TransitionArrivedHome(GameObject character)
    {
        this.character = character;
    }

    public override bool isTriggered()
    {
        GameObject home = GameObject.FindGameObjectsWithTag(character.tag + "Home")[0];
        float distance = Vector3.Distance(home.transform.position, character.transform.position);
        if (distance < 15.0f)
        {
            return true;
        }
        return false;
    }
    public override string getTargetState()
    {
        return "ArrivedHome";
    }
    public override void getActions()
    {
        return;
    }
}