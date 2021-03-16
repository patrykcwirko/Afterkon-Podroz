using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI/Action/Patrol")]
public class PatrolAction : Action
{
    [Range(-1.5f, -0.8f)]
    public float angle = -0.8f;

    private const float DISTANCE_RAYCAST = 0.8f;
    private Vector3 angles;
    private Vector3 dirMove;
    private bool movingRight  = true;
    private Transform enemyTransform;


    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    public void Patrol(StateController controller)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(controller.eyes.position, controller.eyes.TransformDirection(new Vector2(1, angle)), DISTANCE_RAYCAST);
        RaycastHit2D wallInfo = Physics2D.Raycast(controller.eyes.position, controller.eyes.TransformDirection(Vector2.right), DISTANCE_RAYCAST);
        Debug.DrawRay(controller.eyes.position, controller.eyes.TransformDirection(new Vector2(1, angle)) * DISTANCE_RAYCAST, Color.red);
        Debug.DrawRay(controller.eyes.position, controller.eyes.TransformDirection(Vector2.right) * DISTANCE_RAYCAST, Color.red);
        if (groundInfo.collider == null || wallInfo.collider) dirMove = ChangeDirection();
        if (dirMove.x > 0) controller.transform.localScale = Vector3.one;
        else controller.transform.localScale = new Vector3(-1, 1, 1);
        controller.transform.Translate(dirMove * controller.monsterData.Stats().speed * Time.deltaTime);
    }

    public Vector3 ChangeDirection()
    {
        if (movingRight)
        {
            movingRight = false;
            return Vector3.left;
        }
        else
        {
            movingRight = true;
            return Vector3.right;
        }
    }
}
