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
        Graph graph = character.GetComponent<PathFindCharacter>().graph;
        int randomIndex = Random.Range(0, graph.Nodes.Count);
        character.GetComponent<PathFindCharacter>().targetNode = graph.Nodes[randomIndex];
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