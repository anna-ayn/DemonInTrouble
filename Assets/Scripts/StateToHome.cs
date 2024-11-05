using System.Collections.Generic;
using UnityEngine;

// Estado Ir a su refugio/casa (player y enemigo)
public class StateToHome : State
{
    GameObject character;
    PathFindCharacter pathFindCharacter; // Referencia al componente PathFindCharacter
    List<Transition> transitions = new List<Transition>();

    public StateToHome(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "GoToHome";
        this.pathFindCharacter = character.GetComponent<PathFindCharacter>();
    }

    public override void getActions()
    {
        // Activar el script PathFindCharacter al entrar al estado
        if (pathFindCharacter != null)
        {
            pathFindCharacter.enabled = true;
            pathFindCharacter.random_target = false;
            // Asignar la casa del personaje como objetivo
            string s = character.tag;
            GameObject player = GameObject.FindGameObjectsWithTag(s + "Home")[0];
            pathFindCharacter.target = player;
        }
    }

    public override void getEntryActions()
    {
        // Acciones al entrar al estado
        Debug.Log("Entrando a estado Regresar a Casa");
    }

    public override void getExitActions()
    {
        // Acciones al salir del estado
        Debug.Log("Saliendo de estado Regresar a Casa");
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