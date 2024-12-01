using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado buscar la gema (player)
public class StateSearchGem: State
{
    GameObject character;
    int gem_id;
    List<Transition> transitions = new List<Transition>();

    public StateSearchGem(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "SearchGem";
        this.gem_id = -1;
    }

    public override void getActions()
    {
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        if (gem_id == -1) {
            character.GetComponent<PathFindCharacter>().random_target = false;
            // buscar la primera gema cercana a 30m de el
            for (int i = 0; i < gems.Length; i++)
            {
                float distance = Vector3.Distance(gems[i].transform.position, character.transform.position);
                if (distance < 50.0f)
                {
                    gem_id = i;
                    character.GetComponent<PathFindCharacter>().target = gems[i];
                    return;
                }
            }
        }
        character.GetComponent<PathFindCharacter>().target = gems[gem_id];
        return;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        gem_id = -1;
        return;
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}