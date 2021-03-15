using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public MonsterData monsterData;
    public State currentState;
    public Transform eyes;
    public State remainState;

    [HideInInspector] public Transform chaseTarget;

    private void Update() {
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos() 
    {
        if ( currentState != null && eyes != null )
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawSphere(eyes.position, monsterData.lookSphereCastRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
        }
    }
    //? Pobranie komponettów od przeciwnnika
}
