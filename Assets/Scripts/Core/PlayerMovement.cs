using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    public class Jumping 
    {
        public float jumpForce = 5f;
        public float stompForce = 5f;
        public float dashForce = 5f;
        public Transform groundCheck;
        public Transform wallCheck;
        public float checkRadius;
        public LayerMask whatIsGround;
        public GameObject jumpEffect;
        public float effectLiveTime = 0.3f;
    }


    [SerializeField] float speed = 5f;
    [SerializeField] Jumping jumping;


    Animator _Animator;
    Rigidbody2D _rigidbody2D;
    GameController _gameController;

    float _move;
    int _extraJump;

    bool isGrounded;
    bool isWall;
    bool isObject;
    bool isStomp = false;
    bool canDoubleJump;


    // Start is called before the first frame update
    void Start()
    {
        _extraJump = 0;
        _Animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType<GameController>();

    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(jumping.groundCheck.position, jumping.checkRadius, jumping.whatIsGround);
        isWall = Physics2D.OverlapCircle(jumping.wallCheck.position, jumping.checkRadius, jumping.whatIsGround);
        isObject = Physics2D.OverlapCircle(jumping.groundCheck.position, jumping.checkRadius, LayerMask.GetMask("Object"));

        if (isGrounded || isObject) canDoubleJump = true;

        ChangeAnimation();
        FlipSprite();
        Move();

    }

    private void Move()
    {
        if (!isWall) 
        { 
            _rigidbody2D.velocity = new Vector3(_move * speed, _rigidbody2D.velocity.y); 
        }
        else 
        {
            _rigidbody2D.velocity = new Vector3(0f, _rigidbody2D.velocity.y); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (isStomp)
            {
                isStomp = false;
                Destroy(collision.gameObject);
            }
        }
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

        if (context.phase == InputActionPhase.Started)
        {
            if (isGrounded || isObject)
            {
                _extraJump = 1;
                Debug.Log("jump");
                Jump(context);
            }
            else if (canDoubleJump && _gameController.doubleJumpEvable)
            {
                Debug.Log("Double jump");
                Jump(context);
                SpawnEffect();
                canDoubleJump = false;
            }
        }
    }

    private void SpawnEffect()
    {
        var effects = Instantiate(jumping.jumpEffect, transform.position, Quaternion.identity);
        Destroy(effects, jumping.effectLiveTime);
    }

    public void OnStomp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!isGrounded && _gameController.stompEvable)
            {
                Debug.Log("Stomp");
                isStomp = true;
                _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, -context.ReadValue<float>() * jumping.stompForce);
            }
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, context.ReadValue<float>() * jumping.jumpForce);
        _extraJump++;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            transform.position += new Vector3(_move * jumping.dashForce, 0f);
        }
    }
}
