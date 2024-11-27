using System.Collections.Generic;
using UnityEngine;

public class StateAlert : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateAlert(GameObject character, List<Transition> transitions)
    {
        this.name = "Alert";
        this.character = character;
        this.transitions = transitions;   
    }

    public override void getActions()
    {   
        if (character.tag == "Player") {
            // habilitar componente de exclamaciones
            character.GetComponent<ExclamationsController>().enabled = true;
            character.GetComponent<ExclamationsController>().ShowExclamations();
        }
        return;
    }
    public override void getEntryActions()
    {
        return;
    }
    public override void getExitActions()
    {
        if (character.tag == "Player") {
            // deshabilitar componente de exclamaciones
            character.GetComponent<ExclamationsController>().enabled = false;
            character.GetComponent<ExclamationsController>().HideExclamations();
        } 
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}