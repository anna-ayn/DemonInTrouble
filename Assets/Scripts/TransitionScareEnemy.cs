using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScareEnemy  : Transition
{
    GameObject character;

    public TransitionScareEnemy (GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        // calcular la distancia entre el enemigo y el jugador
        GameObject enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

        Debug.Log("Distance between enemy and player: " + distance);
        if (distance < 15.0f)
            return true;
        return false;
    }
    public override string getTargetState()
    {
        return "ScareEnemy";
    }
    public override void getActions()
    {
        return;
    }
}