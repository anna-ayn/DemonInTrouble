using System.Collections.Generic;
using UnityEngine;

public class TransitionAlert : Transition
{
    GameObject character;

    public TransitionAlert(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        string typeCharacter = character.tag;
        float distance = 0;
        // Si el personaje es un jugador se activa la transicion si el enemigo esta cerca de el
        if (typeCharacter == "Player") {
            GameObject enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
            distance = Vector3.Distance(enemy.transform.position, character.transform.position);
            if (distance < 45.0f)
                return true;
        } 
        return false;
    }
    public override string getTargetState()
    {
        return "Alert";
    }
    public override void getActions()
    {
        return;
    }
}