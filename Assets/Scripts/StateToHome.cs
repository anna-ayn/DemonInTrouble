using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado Ir a su refugio/casa (player y enemigo)
public class StateToHome : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateToHome(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "GoToHome";
    }

    public override void getActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
        // Asignar la casa del personaje como objetivo
        GameObject home = GameObject.FindGameObjectsWithTag(character.tag + "Home")[0];
        Graph graph = character.GetComponent<PathFindCharacter>().graph;
        character.GetComponent<PathFindCharacter>().targetNode = graph.FindCube(home.transform.position);
        
        if (character.tag == "Player")
        {
            // visualizar el bolso del personaje
            character.GetComponent<BagController>().enabled = true;
            character.GetComponent<BagController>().Showbag();
        } else if (character.tag == "Enemy") {
            // pintarlo de color rojo
            character.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        if (character.tag == "Enemy") {
            character.GetComponent<Renderer>().material.color = Color.white;
        }
        character.GetComponent<PathFindCharacter>().random_target = true;
        
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}