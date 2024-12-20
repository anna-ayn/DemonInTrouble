using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado Caminar (player y enemigo)
public class StateWalk : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateWalk(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "WalkRandomly";
    }

    public override void getActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = true;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}