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
            if (distance > 20.0f)
                return true;
        } else if (typeCharacter == "Player") {
            // Si el personaje tiene el bolso con la gema, no se activa la transición
            if (character.GetComponent<BagController>().enabled && character.GetComponent<BagController>().isShowingABag()) {
                return false;
            }
            // Si el personaje es el jugador, se activa la transición si el enemigo no esta cerca de 30m
            // o si el enemigo esta en su casa
            GameObject enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
            distance = Vector3.Distance(enemy.transform.position, character.transform.position);

            GameObject enemyHome = GameObject.FindGameObjectsWithTag("EnemyHome")[0];   
            float distanceHome = Vector3.Distance(enemyHome.transform.position, enemy.transform.position);
            if (distance > 20.0f || distanceHome < 15.0f)
                return true;
        }       
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