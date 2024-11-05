using System.Collections.Generic;
using UnityEngine;

// Estado Perseguir al Jugador (enemigo y mascota)
public class StatePursuePlayer : State
{
    GameObject character;
    PathFindCharacter pathFindCharacter; // Referencia al componente PathFindCharacter
    List<Transition> transitions = new List<Transition>();

    public StatePursuePlayer(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "PursuePlayer";
        this.pathFindCharacter = character.GetComponent<PathFindCharacter>();
    }

    public override void getActions()
    {
         // Activar el script PathFindCharacter al entrar al estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.enabled = true;
            pathFindCharacter.random_target = false;
            // Asignar el jugador como objetivo
            GameObject player = GameObject.Find("Player");
            pathFindCharacter.target = player;
        }
    }

    public override void getEntryActions()
    {
        // Acciones al entrar al estado
        Debug.Log("Entrando a estado Perseguir al jugador");
    }

    public override void getExitActions()
    {
        // Acciones al salir del estado
        Debug.Log("Saliendo de estado Perseguir al jugador");
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