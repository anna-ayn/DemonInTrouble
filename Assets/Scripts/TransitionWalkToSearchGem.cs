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

        // calcular la distancia entre el personaje y las gemas
        float min_distance = float.MaxValue;
        foreach (GameObject gem in gems)
        {
            float distance = Vector3.Distance(gem.transform.position, character.transform.position);
            if (distance < min_distance)
            {
                min_distance = distance;
            }
        }

        // si la distancia minima es menor a 50 metros, se activa la transición
        if (min_distance < 50.0f)
        {
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