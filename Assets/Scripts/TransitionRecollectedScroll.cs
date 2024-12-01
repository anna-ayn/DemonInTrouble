using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRecollectedScroll: Transition
{
    GameObject[] traps;

    public TransitionRecollectedScroll()
    {
        this.traps = GameObject.FindGameObjectsWithTag("Scroll");
    }

    public override bool isTriggered()
    {
        GameObject[] traps2 = GameObject.FindGameObjectsWithTag("Scroll");
        if (traps2.Length < traps.Length) {
            traps = GameObject.FindGameObjectsWithTag("Scroll");
            return true;
        }
        return false;
    }
    public override string getTargetState()
    {
        return "RecollectedScroll";
    }
    public override void getActions()
    {
        return;
    }
}