using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineEnemy : MonoBehaviour
{
    // We're in one state at a time
    State initialState;
    State currentState;

    List<State> states = new List<State>();
    List<Transition> TstartGoToHome = new List<Transition>();
    List<Transition> TstartWalking = new List<Transition>();
    List<Transition> TPursuePlayer = new List<Transition>();
    List<Transition> TarrivedHome = new List<Transition>();

    public void Start()
    {
        
        // Creates Transitions
        Transition startGoToHome = new TransitionStartGoToHome(gameObject);
        Transition startWalking = new TransitionToStartWalking(gameObject);
        Transition arrivedHome = new TransitionArrivedHome(gameObject);
        Transition pursuePlayer = new TransitionStartPursuePlayer(gameObject);

        // si el enemigo esta caminando aleatoriamente, puede perseguir al jugador si esta cerca
        // o ir a su casa si la mascota esta cerca
        TstartWalking.Add(pursuePlayer);
        TstartWalking.Add(startGoToHome);
        
        // si el enemigo va a su casa, se detiene al llegar
        TstartGoToHome.Add(arrivedHome);
        // si el enemigo esta en su casa, puede volver a caminar aleatoriamente
        TarrivedHome.Add(startWalking);

        // si el enemigo esta persiguiendo al jugador, se va a su casa si la mascota esta cerca
        TPursuePlayer.Add(startGoToHome);

        // Creates States
        StateWalk walkState = new StateWalk(gameObject, TstartWalking);
        StateToHome goToHomeState = new StateToHome(gameObject, TstartGoToHome);
        StateArrivedHome arrivedHomeState = new StateArrivedHome(gameObject, TarrivedHome);
        StatePursuePlayer pursuePlayerState = new StatePursuePlayer(gameObject, TPursuePlayer);
        

        states.Add(walkState);
        states.Add(goToHomeState);
        states.Add(arrivedHomeState);
        states.Add(pursuePlayerState);

        initialState = walkState; // walking randomly as initialstate
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
