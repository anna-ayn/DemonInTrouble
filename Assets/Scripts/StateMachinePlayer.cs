using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachinePlayer : MonoBehaviour
{
    // We're in one state at a time
    State initialState;
    State currentState;

    List<State> states = new List<State>();
    List<Transition> TstartGoToHome = new List<Transition>();
    List<Transition> TstartWalking = new List<Transition>();
    List<Transition> Tstop = new List<Transition>();
    List<Transition> TwalkToSearchGem = new List<Transition>();
    List<Transition> TarrivedHome = new List<Transition>();
    // List<Transition> TSearchTrap = new List<Transition>();
    // List<Transition> TFoundTrap = new List<Transition>();
    // List<Transition> TRecollectedScroll = new List<Transition>();
    // Dictionary<string, Transition> stateTransitions;

    public void Start()
    {
        
        // Creates Transitions
        Transition startGoToHome = new TransitionStartGoToHome(gameObject);
        Transition startWalking = new TransitionToStartWalking(gameObject);
        Transition stop = new TransitionToStop(gameObject);
        Transition walkToSearchGem = new TransitionWalkToSearchGem(gameObject);
        Transition arrivedHome = new TransitionArrivedHome(gameObject);
        // Transition searchForTrap = new TransitionSearchForTrap(gameObject);
        // Transition foundTrap = new TransitionFoundTrap(gameObject);
        // Transition recollectedScroll = new TransitionRecollectedScroll();

        //  // map: state -> transition
        // stateTransitions = new Dictionary<string, Transition>()
        // {
        //     { "WalkRandomly", startWalking },
        //     { "GoToHome", startGoToHome },
        //     { "Stop", stop},
        //     { "SearchGem", walkToSearchGem},
        //     { "ArrivedHome", arrivedHome},
        //     { "SearchForTrap", searchForTrap},
        //     { "FoundTrap", foundTrap},
        // };

        // si el jugador esta caminando aleatoriamente, puede ir a buscar una gema si esta cerca
        // o puede detenerse al encontrar un enemigo
        // TstartWalking.Add(searchForTrap);
        TstartWalking.Add(walkToSearchGem);
        TstartWalking.Add(stop);

        // si el jugador esta buscando una gema, puede detenerse al encontrar un enemigo
        // tambien puede ir a su casa si encontro la gema
        TwalkToSearchGem.Add(stop);
        // TwalkToSearchGem.Add(searchForTrap);
        TwalkToSearchGem.Add(startGoToHome);

        // si el jugador esta detenido, puede volver a caminar aleatoriamente si el enemigo se aleja
        Tstop.Add(startWalking);
        Tstop.Add(startGoToHome);

        // si el jugador va a su casa con su gema, se detiene al llegar
        TstartGoToHome.Add(stop);
        // TstartGoToHome.Add(searchForTrap);
        TstartGoToHome.Add(arrivedHome);

        // si el jugador esta en su casa, puede volver a caminar aleatoriamente
        TarrivedHome.Add(startWalking);

        // TSearchTrap.Add(foundTrap);

        // // al encontrar una trampa, se verifica si se recolecto el scroll o no
        // TFoundTrap.Add(recollectedScroll);

        // TRecollectedScroll.Add(searchForTrap);
    

        // Creates States
        StateWalk walkState = new StateWalk(gameObject, TstartWalking);
        StateStop stopState = new StateStop(gameObject, Tstop);
        StateSearchGem walkToSearchGemState = new StateSearchGem(gameObject, TwalkToSearchGem);
        StateToHome goToHomeState = new StateToHome(gameObject, TstartGoToHome);
        StateArrivedHome arrivedHomeState = new StateArrivedHome(gameObject, TarrivedHome);
        // StateSearchForTrap searchForTrapState = new StateSearchForTrap(gameObject, TSearchTrap);
        // StateFoundTrap foundTrapState = new StateFoundTrap(gameObject, TFoundTrap);

        states.Add(walkState);
        states.Add(stopState);
        states.Add(walkToSearchGemState);
        states.Add(goToHomeState);
        states.Add(arrivedHomeState);
        // states.Add(searchForTrapState);
        // states.Add(foundTrapState);

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
                    // if (triggered.getTargetState() == "SearchForTrap" && currentState.nameState != "FoundTrap") {
                    //     if (TFoundTrap.Count == 2) {
                    //         TFoundTrap.Add(stateTransitions[currentState.nameState]);
                    //     } else {
                    //         TFoundTrap[2] = stateTransitions[currentState.nameState];
                    //     }
                    //     states[6] = new StateFoundTrap(gameObject, TFoundTrap);
                    // }
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
