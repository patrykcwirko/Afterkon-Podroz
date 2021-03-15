using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Action/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        Vector2 distToTarget = (controller.transform.position - controller.chaseTarget.position).normalized;
        controller.transform.Translate(-distToTarget * controller.monsterData.Stats().speed * Time.deltaTime);
    }
}
