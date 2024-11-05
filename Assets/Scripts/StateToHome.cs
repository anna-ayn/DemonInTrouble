using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado Ir a su refugio/casa (player y enemigo)
public class StateToHome : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateToHome(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "GoToHome";
    }

    public override void getActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
        // Asignar la casa del personaje como objetivo
        GameObject player = GameObject.FindGameObjectsWithTag(character.tag + "Home")[0];
        character.GetComponent<PathFindCharacter>().target = player;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}