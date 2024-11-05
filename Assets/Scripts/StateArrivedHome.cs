using System.Collections.Generic;
using UnityEngine;

public class StateArrivedHome : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateArrivedHome(GameObject character, List<Transition> transitions)
    {
        this.name = "ArrivedHome";
        this.character = character;
        this.transitions = transitions;   
    }

    public override void getActions()
    {
        return;
    }
    public override void getEntryActions()
    {
        Debug.Log("Entrando a estado de Esperar en casa");
        return;
    }
    public override void getExitActions()
    {
        Debug.Log("Saliendo del estado de Esperar en casa");
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}