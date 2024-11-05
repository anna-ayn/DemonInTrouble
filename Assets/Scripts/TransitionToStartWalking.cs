using System.Collections.Generic;
using UnityEngine;

public class TransitionToStartWalking : Transition
{
    GameObject character;

    public TransitionToStartWalking(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        string typeCharacter = character.tag;
        float distance = 0;
        // Si el personaje es un enemigo, se activa la transición si la mascota no esta cerca de 30m
        if (typeCharacter == "Enemy")
        {
            GameObject pet = GameObject.FindGameObjectsWithTag("Pet")[0];
            distance = Vector3.Distance(pet.transform.position, character.transform.position);
        } else if (typeCharacter == "Player") {
            // Si el personaje es el jugador, se activa la transición si el enemigo no esta cerca de 30m
            GameObject enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
            distance = Vector3.Distance(enemy.transform.position, character.transform.position);
        }
        if (distance > 30.0f)
            return true;
        else
            return false;
    }
    public override string getTargetState()
    {
        return "WalkRandomly";
    }
    public override void getActions()
    {
        return;
    }
}