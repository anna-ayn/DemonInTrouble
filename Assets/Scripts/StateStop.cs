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
        character.GetComponent<Agent>().kinematic.velocity = Vector3.zero;

        // destroy lines
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line" + character.tag);
        for (int i = 0; i < lines.Length; i++) {
            Object.Destroy(lines[i]);
        }

        if (character.tag == "Pet") return;
        GameObject[] linesOpt = GameObject.FindGameObjectsWithTag("LineOpt" + character.tag);        


        for (int i = 0; i < linesOpt.Length; i++) {
            Object.Destroy(linesOpt[i]);
        }
        
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
        character.GetComponent<Renderer>().material.color = Color.white;
        if (character.tag == "Player") {
            // deshabilitar componente de escudo 

            character.GetComponent<ShieldController>().enabled = false;
            character.GetComponent<ShieldController>().HideShield();
        } 
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}