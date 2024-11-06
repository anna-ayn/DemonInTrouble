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
        character.GetComponent<Renderer>().material.color = Color.white;
        if (character.tag == "Player")
        {
            // ocultar el bolso del personaje
            character.GetComponent<BagController>().Hidebag();
            character.GetComponent<BagController>().enabled = false;
        } 
        return;
    }
    public override void getEntryActions()
    {
        return;
    }
    public override void getExitActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = true;
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}