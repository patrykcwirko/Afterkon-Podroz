using System;
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
    [HideInInspector] public int stateTimeElapsed;
    [HideInInspector] public Vector3 dirMove;

    private void Start() {
        dirMove = Vector3.right;
        TimeTickSystem.onTick += TimeOnTick;
    }


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

    private void TimeOnTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        stateTimeElapsed++;
    }

    public bool CheckIfCountDownElapsed(int duration)
    {
        return (stateTimeElapsed >= duration);
    }

    public void ResetTimer()
    {
        stateTimeElapsed = 0;
    }
    //? Pobranie komponettów od przeciwnnika
}
