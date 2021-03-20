using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Action/Chase")]
public class ChaseAction : Action
{
    public override void Act(Enemy.EnemyController controller)
    {
        Chase(controller);
    }

    private void Chase(Enemy.EnemyController controller)
    {
        Vector2 distToTarget = (controller.transform.position - controller.chaseTarget.position).normalized;
        controller.transform.Translate(-distToTarget * controller.monsterData.Stats().speed * Time.deltaTime);
    }
}
