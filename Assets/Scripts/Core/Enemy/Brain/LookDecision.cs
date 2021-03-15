using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Decision/Look")]
public class LookDecision : Decision
{

    [SerializeField] private float lookDistance = 1f;
    [Range(0,360)]
    [SerializeField] private float angleLook;
    [SerializeField] private LayerMask playerLayer;
    
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Collider2D hits = Physics2D.OverlapCircle(controller.eyes.position, lookDistance, playerLayer);
        if(hits == null) return false;
        Vector2 dirToTarget = (hits.transform.position - controller.eyes.position).normalized;
        if ( Vector3.Angle(controller.eyes.right, dirToTarget) < angleLook/2 )
        {
            controller.chaseTarget = hits.transform;
            return true;
        }
        return false;
    }
}
