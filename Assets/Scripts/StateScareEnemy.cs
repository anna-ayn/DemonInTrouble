using System.Collections.Generic;
using UnityEngine;

// Estado Perseguir y Asustar al Enemigo (mascota)
public class StateScareEnemy : State
{
    GameObject character;
    PathFindCharacter pathFindCharacter; // Referencia al componente PathFindCharacter
    List<Transition> transitions = new List<Transition>();

    public StateScareEnemy(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "ScareEnemy";
        this.pathFindCharacter = character.GetComponent<PathFindCharacter>();
    }

    public override void getActions()
    {
         // Activar el script PathFindCharacter al entrar al estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.enabled = true;
            pathFindCharacter.random_target = false;
            // Asignar el enemigo como objetivo
            GameObject player = GameObject.Find("Enemy");
            pathFindCharacter.target = player;
        }
    }

    public override void getEntryActions()
    {
        // Acciones al entrar al estado
        Debug.Log("Entrando a estado Perseguir al enemigo");
    }

    public override void getExitActions()
    {
        // Acciones al salir del estado
        Debug.Log("Saliendo de estado Perseguir al enemigo");
        // Desactivar el script al salir del estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.enabled = false;
            pathFindCharacter.random_target = true;
        }
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}