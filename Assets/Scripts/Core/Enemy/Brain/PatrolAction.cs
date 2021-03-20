using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Action/Patrol")]
public class PatrolAction : Action
{
    [Range(-1.5f, -0.8f)]
    public float angle = -0.8f;
    public LayerMask layerHit;

    private const float DISTANCE_RAYCAST = 0.8f;
    private bool movingRight  = true;


    public override void Act(Enemy.EnemyController controller)
    {
        Patrol(controller);
    }

    public void Patrol(Enemy.EnemyController controller)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(controller.eyes.position, controller.eyes.TransformDirection(new Vector2(1, angle)), DISTANCE_RAYCAST, layerHit);
        RaycastHit2D wallInfo = Physics2D.Raycast(controller.eyes.position, controller.eyes.TransformDirection(Vector2.right), DISTANCE_RAYCAST, layerHit);
        Debug.DrawRay(controller.eyes.position, controller.eyes.TransformDirection(new Vector2(1, angle)) * DISTANCE_RAYCAST, Color.red);
        Debug.DrawRay(controller.eyes.position, controller.eyes.TransformDirection(Vector2.right) * DISTANCE_RAYCAST, Color.red);
        if (groundInfo.collider == null || wallInfo.collider)  controller.dirMove = ChangeDirection(controller.dirMove);
        if (controller.dirMove.x > 0) controller.transform.localScale = Vector3.one;
        else controller.transform.localScale = new Vector3(-1, 1, 1);
        controller.transform.Translate(controller.dirMove * controller.monsterData.Stats().speed * Time.deltaTime);
    }

    public Vector3 ChangeDirection(Vector3 dirMoveOld)
    {
        return new Vector3(-dirMoveOld.x, dirMoveOld.y);
    }
}
