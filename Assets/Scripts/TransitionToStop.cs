using System.Collections.Generic;
using UnityEngine;

public class TransitionToStop : Transition
{
    GameObject character;

    public TransitionToStop(GameObject character)
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
            if (distance < 15.0f)
                return true;
        } else if (typeCharacter == "Pet") { // Si el personaje es la mascota se activa la transicion si el jugador esta cerca de el
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0]; 
            distance = Vector3.Distance(player.transform.position, character.transform.position);
            if (distance < 40.0f)
                return true;
        }
        return false;
    }
    public override string getTargetState()
    {
        return "Stop";
    }
    public override void getActions()
    {
        return;
    }
}