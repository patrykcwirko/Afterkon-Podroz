using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatrorm : MonoBehaviour
{
    [Header("Basic")]
    public float speed = 1;
    public bool rightStart;
    public LayerMask maskGround;
    [Header("Point To Point")]
    public bool isMovingToPoint;
    public Transform[] points;
    public bool isWaitingInPoint;
    public float waitTime;

    private const float CHECK_RADIUS = 0.1f;

    private Transform _raycastPointLeft;
    private Transform _raycastPointRight;
    private Vector3 _directionMove;
    private int positionIndex;

    void Start()
    {
        if (!isMovingToPoint)
        {
            if(rightStart) _directionMove = Vector3.right;
            else _directionMove = Vector3.left;
            _raycastPointLeft = transform.Find("WallCheckLeft");
            _raycastPointRight = transform.Find("WallCheckRight");
            return;
        }
        positionIndex = 0;
        _directionMove = (points[0].position - transform.position).normalized;
    }

    void Update()
    {
        if(isMovingToPoint)
        {
            if(transform.position == points[positionIndex].position)
            {
                if(isWaitingInPoint) StartCoroutine(Wait(waitTime));
                positionIndex++;
                if (positionIndex >= points.Length) positionIndex = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, points[positionIndex].position, speed*Time.deltaTime);
            return;
        }
        transform.Translate(_directionMove * speed * Time.deltaTime);
        bool wallCheckLeft = Physics2D.OverlapCircle(_raycastPointLeft.position, CHECK_RADIUS, maskGround);
        bool wallCheckRight = Physics2D.OverlapCircle(_raycastPointRight.position, CHECK_RADIUS, maskGround);
        if (wallCheckLeft)
        {
            _directionMove = Vector3.right;
        }
        if (wallCheckRight)
        {
            _directionMove = Vector3.left;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.collider.transform.SetParent(transform);
        }   
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.collider.transform.SetParent(null);
        }
    }
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
