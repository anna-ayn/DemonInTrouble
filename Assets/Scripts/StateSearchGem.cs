using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado buscar la gema (player)
public class StateSearchGem: State
{
    GameObject character;
    GameObject gem;
    List<Transition> transitions = new List<Transition>();

    public StateSearchGem(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "SearchGem";
        this.gem = null;
    }

    public override void getActions()
    {
        if (gem == null) {
            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            character.GetComponent<PathFindCharacter>().random_target = false;
            // buscar la primera gema cercana a 30m de el
            for (int i = 0; i < gems.Length; i++)
            {
                float distance = Vector3.Distance(gems[i].transform.position, character.transform.position);
                if (distance < 30.0f)
                {
                    gem = gems[i];
                    character.GetComponent<PathFindCharacter>().target = gems[i];
                    return;
                }
            }
        }
        character.GetComponent<PathFindCharacter>().target = gem;
        return;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        gem = null;
        return;
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}