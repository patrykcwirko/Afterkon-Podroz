using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Brains/PatrolBrain")]
public class Patrol : IBrain
{
    [Range(-1, 1)]
    public float angle = -0.8f;

    private const float DISTANCE_RAYCAST = 1f;
    private Vector3 angles;
    private Vector3 dirMove;
    private bool movingRight  = true;
    private Transform enemyTransform;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 Think(Transform raycastPoint)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(raycastPoint.position, raycastPoint.TransformDirection(new Vector2(1, angle)), DISTANCE_RAYCAST);
        RaycastHit2D wallInfo = Physics2D.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector2.right), DISTANCE_RAYCAST, LayerMask.NameToLayer("Player"));
        Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(new Vector2(1, angle)) * DISTANCE_RAYCAST, Color.red);
        if (groundInfo.collider == null || wallInfo.collider)
        {
            if (movingRight)
            {
                dirMove = Vector3.left;
                movingRight = false;
            }
            else
            {
                dirMove = Vector3.right;
                movingRight = true;
            }
        }
        return dirMove;
    }
}
