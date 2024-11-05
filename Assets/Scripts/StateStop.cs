using System.Collections.Generic;
using UnityEngine;

public class StateStop : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateStop(GameObject character, List<Transition> transitions)
    {
        this.name = "Stop";
        this.character = character;
        this.transitions = transitions;   
    }

    public override void getActions()
    {
        return;
    }
    public override void getEntryActions()
    {
        Debug.Log("Entrando a estado de Espera");
        return;
    }
    public override void getExitActions()
    {
        Debug.Log("Saliendo del estado de Espera");
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}