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
            // si el personaje esta cerca del enemigo, no se activa la transicion
            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) {
                GameObject enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
                float distance = Vector3.Distance(enemy.transform.position, character.transform.position);
                if (distance < 15.0f)
                    return false;
            }
            
            if (character.GetComponent<BagController>().enabled && character.GetComponent<BagController>().isShowingABag()) {
                return true;
            }
            GameObject[] gem = GameObject.FindGameObjectsWithTag("Gem");
            foreach (GameObject g in gem)
            {
                float distance = Vector3.Distance(g.transform.position, character.transform.position);
                if (distance < 10.0f) {
                    UnityEngine.Object.Destroy(g);
                    return true;
                }
            }
        } 
        // Si el personaje es un enemigo, se activa la transición si esta cerca de la mascota
        else if (character.tag == "Enemy")
        {
            GameObject pet = GameObject.FindGameObjectsWithTag("Pet")[0];
            float distance = Vector3.Distance(pet.transform.position, character.transform.position);
            if (distance < 15.0f)
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