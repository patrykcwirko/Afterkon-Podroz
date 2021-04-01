using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Action/AttackDistance")]
public class AttackAction : Action
{
    public GameObject projectilePrefab;

    public override void Act(Enemy.EnemyController controller)
    {
        Attack(controller);
    }

    private void Attack(Enemy.EnemyController controller)
    {
        if(controller.CheckIfCountDownElapsed(controller.monsterData.attackRateInTick))
        {
            Vector2 dirToTarget = controller.chaseTarget.position - controller.eyes.position;
            controller.eyes.right = dirToTarget;
            GameObject projectile = Instantiate(projectilePrefab, controller.eyes.position, controller.eyes.rotation);
            projectile.GetComponent<Projectile>().direction = new Vector2( Mathf.Abs( dirToTarget.normalized.x),0f);
            controller.ResetTimer();
        }
    }
}
