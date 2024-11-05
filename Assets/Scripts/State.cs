using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string name;
    public abstract void getActions();
    public abstract void getEntryActions();
    public abstract void getExitActions();
    public abstract List<Transition> getTransitions();
}