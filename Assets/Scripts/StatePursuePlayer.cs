using System.Collections.Generic;
using UnityEngine;

// Estado Perseguir al Jugador (enemigo y mascota)
public class StatePursuePlayer : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StatePursuePlayer(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "PursuePlayer";
    }

    public override void getActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
        GameObject player = GameObject.Find("Player");
        Graph graph = character.GetComponent<PathFindCharacter>().graph;
        character.GetComponent<PathFindCharacter>().targetNode = graph.FindCube(player.transform.position);
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