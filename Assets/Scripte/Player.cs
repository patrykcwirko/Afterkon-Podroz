using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;

    Animator myAnimator;
    Rigidbody2D _rigidbody2D;


    private Vector2 _move;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        ChangeAnimation();
        FlipSprite();
        if (_move.y > 0) _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _move.y * jumpForce);
        _rigidbody2D.velocity = new Vector3(_move.x * speed, _rigidbody2D.velocity.y);
        
    }

    private void ChangeAnimation()
    {
        if (_move.x == 0) myAnimator.SetBool("isRunning", false);
        else myAnimator.SetBool("isRunning", true);
    }

    private void FlipSprite()
    {
        if (_move.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(_move.x), 1f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
        Debug.Log(_move);
    }
    
}
