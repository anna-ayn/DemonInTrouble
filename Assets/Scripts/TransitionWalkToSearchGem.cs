using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWalkToSearchGem : Transition
{
    GameObject character;
    List<GameObject> gems;

    public TransitionWalkToSearchGem(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        gems = new List<GameObject>(GameObject.FindGameObjectsWithTag("Gem"));

        // buscar la primera gema cercana al personaje 
        foreach (GameObject gem in gems)
        {
            float distance = Vector3.Distance(gem.transform.position, character.transform.position);
            if (distance < 30.0f)
                return true;
        }
        return false;
    }
    public override string getTargetState()
    {
        return "SearchGem";
    }
    public override void getActions()
    {
        return;
    }
}