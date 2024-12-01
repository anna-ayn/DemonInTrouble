using System.Collections.Generic;
using UnityEngine;

// Estado Perseguir y Asustar al Enemigo (mascota)
public class StateScareEnemy : State
{
    GameObject character;
    List<Transition> transitions = new List<Transition>();

    public StateScareEnemy(GameObject character, List<Transition> transitions)
    {
        this.character = character;
        this.transitions = transitions;
        this.name = "ScareEnemy";
    }

    public override void getActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
        // Asignar el enemigo como objetivo
        GameObject enemy = GameObject.Find("Enemy");
        Graph graph = character.GetComponent<PathFindCharacter>().graph;
        character.GetComponent<PathFindCharacter>().targetNode = graph.FindCube(enemy.transform.position);
    }

    public override void getEntryActions()
    {
    }

    public override void getExitActions()
    {
        character.GetComponent<PathFindCharacter>().random_target = false;
        Animator animator = character.GetComponent<Animator>();
        animator.SetBool("Attack", false);
    }

    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}