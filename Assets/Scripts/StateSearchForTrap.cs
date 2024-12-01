using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForTrap: State
{
    GameObject character;
    int trap_id;
    List<Transition> transitions = new List<Transition>();

    public StateSearchForTrap(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "SearchForTrap";
        this.trap_id = -1;
    }

    public override void getActions()
    {
        Graph graph = character.GetComponent<PathFindCharacter>().graph;
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Scroll");
        if (trap_id == -1) {
            character.GetComponent<PathFindCharacter>().random_target = false;
            // buscar la primera trampa cercana a 30m de el
            for (int i = 0; i < traps.Length; i++)
            {
                float distance = Vector3.Distance(traps[i].transform.position, character.transform.position);
                if (distance < 30.0f)
                {
                    trap_id = i;
                    character.GetComponent<PathFindCharacter>().targetNode = graph.FindCube(traps[i].transform.position);
                    return;
                }
            }
        }
        character.GetComponent<PathFindCharacter>().targetNode = graph.FindCube(traps[trap_id].transform.position);
        return;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        trap_id = -1;
        return;
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}