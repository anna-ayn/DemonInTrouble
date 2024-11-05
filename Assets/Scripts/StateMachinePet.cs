using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachinePet : MonoBehaviour
{
    // We're in one state at a time
    State initialState;
    State currentState;

    List<State> states = new List<State>();
    List<Transition> TPursuePlayer = new List<Transition>();
    List<Transition> TStop = new List<Transition>();
    List<Transition> TScareEnemy = new List<Transition>();

    public void Start()
    {
        
        // Creates Transitions
        Transition pursuePlayer = new TransitionStartPursuePlayer(gameObject);
        Transition stop = new TransitionToStop(gameObject);
        Transition scareEnemy = new TransitionScareEnemy(gameObject);

        // si la mascota esta detenido, puede asustar al enemigo si esta cerca del jugador
        // o puede perseguir al jugador si esta lejos
        TStop.Add(scareEnemy);
        TStop.Add(pursuePlayer);
        
        // si la mascota esta persiguiendo al jugador, se detiene al llegar
        TPursuePlayer.Add(stop);

        // si la mascota esta asustando al enemigo, se detiene al asustarlo
        TScareEnemy.Add(stop);

        // Creates States
        StateStop stopState = new StateStop(gameObject, TStop);
        StateScareEnemy scareEnemyState = new StateScareEnemy(gameObject, TScareEnemy);
        StatePursuePlayer pursuePlayerState = new StatePursuePlayer(gameObject, TPursuePlayer);
        
        states.Add(stopState);
        states.Add(scareEnemyState);
        states.Add(pursuePlayerState);

        initialState = stopState; // stop as initialstate
        currentState = initialState;
    }

    // Checks and applies transitions, returning a list of actions.
    public void Update()
    {
        // Assume no transition is triggered.
        Transition triggered = null;

        // Check through each transition and store the first 
        // one that triggers.

        foreach(Transition transition in currentState.getTransitions())
        {
            if (transition.isTriggered())
                {
                    triggered = transition;
                    break;        
                }
        }

        // Check if we have a transition to fire.
        if (triggered != null)
        {
            // Find the target state.
            string targetState = triggered.getTargetState();

            Debug.Log("Transition triggered for " + gameObject.name + ": " + triggered.getTargetState());

            // Add the exit action of the old state, the
            // transition action and the entry for the new state
            currentState.getExitActions();
            foreach(State state in states)
            {
                if (Object.Equals(state.name, targetState))
                    {
                        currentState = state;
                        break;
                    }
            }
            // Complete the transition and return the action list.
            triggered.getActions();
        }
        else
            currentState.getActions();
    }
}
