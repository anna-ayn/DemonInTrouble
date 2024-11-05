using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    // The isTriggered method returns true if the transition can fire.
    public abstract bool isTriggered();
    // The getTargetState method reports which state to transition to.
    public abstract string getTargetState();
    // The getAction method returns a list of actions to carry out when the transition fires.
    public abstract List<Action> getActions();
}