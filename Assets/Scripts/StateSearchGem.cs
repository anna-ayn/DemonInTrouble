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
    }

    public override void getActions()
    {
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        character.GetComponent<PathFindCharacter>().random_target = false;
        // buscar la gema mas cercana
        float min_distance = float.MaxValue;
        int idx = -1;
        for (int i = 0; i < gems.Length; i++)
        {
            float distance = Vector3.Distance(gems[i].transform.position, character.transform.position);
            if (distance < min_distance)
            {
                min_distance = distance;
                idx = i;
            }
        }
        if (idx != -1) {
            if (min_distance < 50.0f)
                gem = gems[idx]; // update gem
            
            character.GetComponent<PathFindCharacter>().target = gem;
        }
        return;
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        UnityEngine.Object.Destroy(gem);
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}