using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Action/AttackDistance")]
public class AttackAction : Action
{
    public GameObject projectilePrefab;

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        if(controller.CheckIfCountDownElapsed(controller.monsterData.attackRateInTick))
        {
            GameObject projectile = Instantiate(projectilePrefab, controller.eyes.position, Quaternion.identity);
            Vector2 dirToTarget = (controller.chaseTarget.position - projectile.transform.position).normalized;
            if(dirToTarget.x < 0) projectile.transform.localScale = new Vector2(Mathf.Sign(dirToTarget.x), 1f); 
            projectile.GetComponent<Projectile>().direction = dirToTarget;
            controller.ResetTimer();
        }
    }
}
