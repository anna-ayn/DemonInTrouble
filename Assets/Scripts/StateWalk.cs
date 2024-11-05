using System.Collections.Generic;
using UnityEngine;

// Estado Caminar (player y enemigo)
public class StateWalk : State
{
    GameObject character;
    PathFindCharacter pathFindCharacter; // Referencia al componente PathFindCharacter
    List<Transition> transitions = new List<Transition>();

    public StateWalk(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "WalkRandomly";
        this.pathFindCharacter = character.GetComponent<PathFindCharacter>();
    }

    public override void getActions()
    {
         // Activar el script PathFindCharacter al entrar al estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.enabled = true;
            pathFindCharacter.random_target = true;
        }
    }

    public override void getEntryActions()
    {
        // Acciones al entrar al estado
        Debug.Log("Entrando a estado Caminar");
    }

    public override void getExitActions()
    {
        // Acciones al salir del estado
        Debug.Log("Saliendo de estado Caminar");
        // Desactivar el script al salir del estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.random_target = false;
        }
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}