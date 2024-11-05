using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionStartGoToHome : Transition
{
    GameObject character;

    public TransitionStartGoToHome(GameObject character)
    {
        this.character = character;
    }

    public override bool isTriggered()
    {
        // Si el personaje es un jugador, se activa la transición si esta cerca de una gema
        if (character.tag == "Player")
        {
            GameObject[] gem = GameObject.FindGameObjectsWithTag("Gem");
            foreach (GameObject g in gem)
            {
                float distance = Vector3.Distance(g.transform.position, character.transform.position);
                if (distance < 5.0f)
                    return true;
            }
        } 
        // Si el personaje es un enemigo, se activa la transición si esta cerca de la mascota
        else if (character.tag == "Enemy")
        {
            GameObject pet = GameObject.FindGameObjectsWithTag("Pet")[0];
            float distance = Vector3.Distance(pet.transform.position, character.transform.position);
            if (distance < 10.0f)
                return true;
        }
        return false;
        
    }
    public override string getTargetState()
    {
        return "GoToHome";
    }
    public override void getActions()
    {
        return;
    }
}