using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;

    [Header("Jumping")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] int extraJump;

    Animator _Animator;
    Rigidbody2D _rigidbody2D;

    const float CALLBACK_COUNT = 2f;
    float _move;
    int _extraJump;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _extraJump = 0;
        _Animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        ChangeAnimation();
        FlipSprite();
        _rigidbody2D.velocity = new Vector3(_move * speed, _rigidbody2D.velocity.y);

    }

    private void ChangeAnimation()
    {
        if (_move == 0) _Animator.SetBool("isRunning", false);
        else _Animator.SetBool("isRunning", true);
    }

    private void FlipSprite()
    {
        if (_move != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(_move), 1f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<float>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded) { _extraJump = 0; }

        if ((_extraJump+1) / CALLBACK_COUNT <= 1)
        {
            Debug.Log($"jump { _extraJump }");
            _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, context.ReadValue<float>() * jumpForce);
            _extraJump++;
        }
        else if (_extraJump < extraJump * CALLBACK_COUNT)
        {
            Debug.Log("Another jump}");
            _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, context.ReadValue<float>() * jumpForce);
            _extraJump++;
        }
    }
    
}
