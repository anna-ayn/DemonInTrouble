using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionStartPursuePlayer : Transition
{
    GameObject character;

    public TransitionStartPursuePlayer(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        // calcular la distancia entre el personaje y el jugador
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        float distance = Vector3.Distance(player.transform.position, character.transform.position);

        if (character.name == "Enemy" && distance < 50.0f)
            return true;
        else if (character.name == "Pet" && distance > 30.0f)
            return true;
        return false;
    }
    public override string getTargetState()
    {
        return "PursuePlayer";
    }
    public override void getActions()
    {
        return;
    }
}