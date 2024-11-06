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
        if (character.GetComponent<Agent>().kinematic.velocity.magnitude > 0.0f)
            character.GetComponent<Agent>().kinematic.velocity = Vector3.zero;
        
        if (character.tag == "Player") {
            // pintarlo de azul
            character.GetComponent<Renderer>().material.color = Color.blue;

            // habilitar componente de escudo
            character.GetComponent<ShieldController>().enabled = true;
            character.GetComponent<ShieldController>().ShowShield();
        }
        return;
    }
    public override void getEntryActions()
    {
        return;
    }
    public override void getExitActions()
    {
        // volver al color original
        if (character.tag == "Player") {
            character.GetComponent<Renderer>().material.color = Color.white;

            // deshabilitar componente de escudo
            character.GetComponent<ShieldController>().HideShield();
            
        }
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}