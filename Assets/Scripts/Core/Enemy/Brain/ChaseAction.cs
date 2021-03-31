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
        Vector2 dirMove = (controller.transform.position - controller.chaseTarget.position).normalized;
        controller.GetComponent<SpriteRenderer>().sprite = controller.monsterData.AttackSprite();
        if (dirMove.x < 0) controller.transform.localScale = Vector3.one;
        else controller.transform.localScale = new Vector3(-1, 1, 1);
        controller.transform.Translate(-dirMove * controller.monsterData.Stats().speed * Time.deltaTime);
    }
}
