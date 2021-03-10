using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatrorm : MonoBehaviour
{
    public float speed = 1;
    public LayerMask mask;

    private const float CHECK_RADIUS = 0.1f;

    private Transform _raycastPointLeft;
    private Transform _raycastPointRight;
    private Vector3 _directionMove;

    // Start is called before the first frame update
    void Start()
    {
        _directionMove = Vector3.right;
        _raycastPointLeft = transform.Find("WallCheckLeft");
        _raycastPointRight = transform.Find("WallCheckRight");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_directionMove * speed);
        bool wallCheckLeft = Physics2D.OverlapCircle(_raycastPointLeft.position, CHECK_RADIUS, mask);
        bool wallCheckRight = Physics2D.OverlapCircle(_raycastPointRight.position, CHECK_RADIUS, mask);
        if(wallCheckLeft)
        {
            _directionMove = Vector3.right;
        }
        if(wallCheckRight)
        {
            _directionMove = Vector3.left;
        }
    }
}
