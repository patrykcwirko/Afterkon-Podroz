using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Player
{
    public partial class PlayerMovement : MonoBehaviour
    {

        [SerializeField] float speed = 5f;
        [SerializeField] JumpingConfig jumping;


        Animator _Animator;
        Rigidbody2D _rigidbody2D;
        GameController _gameController;

        float _moveDirection;
        float _currentDashTimer;

        bool isGrounded;
        bool isWall;
        bool isObject;
        bool isStomp = false;
        bool canDoubleJump;
        bool isDashing;


        // Start is called before the first frame update
        void Start()
        {
            Setup();

        }

        private void Update()
        {
            CheckPositionInWorld();
            ChangeAnimation();
            FlipSprite();
            Move();
            Dash();

        }

        private void Setup()
        {
            _Animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void CheckPositionInWorld()
        {
            isGrounded = Physics2D.OverlapCircle(jumping.groundCheck.position, jumping.checkRadius, jumping.whatIsGround);
            isWall = Physics2D.OverlapCircle(jumping.wallCheck.position, jumping.checkRadius, jumping.whatIsGround);
            isObject = Physics2D.OverlapCircle(jumping.groundCheck.position, jumping.checkRadius, jumping.whatIsObject);
            if (isGrounded || isObject) canDoubleJump = true;
        }

        private void Dash()
        {
            if (isDashing)
            {
                _rigidbody2D.velocity = transform.right * _moveDirection * jumping.dashForce;
                _currentDashTimer -= Time.deltaTime;
                if (_currentDashTimer <= 0)
                {
                    isDashing = false;
                }
            }
        }

        private void Move()
        {
            if (!isWall)
            {
                _rigidbody2D.velocity = new Vector3(_moveDirection * speed, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity = new Vector3(0f, _rigidbody2D.velocity.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                Debug.Log("Destroy");
                if (isStomp)
                {
                    isStomp = false;
                    Destroy(collision.gameObject);
                }
            }
        }

        private void ChangeAnimation()
        {
            if (_moveDirection == 0) _Animator.SetBool("isRunning", false);
            else _Animator.SetBool("isRunning", true);
        }

        private void FlipSprite()
        {
            if (_moveDirection != 0)
            {
                transform.localScale = new Vector2(Mathf.Sign(_moveDirection), 1f);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<float>();
        }
        public void OnJump(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Started)
            {
                if (isGrounded || isObject)
                {
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
        public void OnStomp(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (!isGrounded && _gameController.stompEnable)
                {
                    Debug.Log("Stomp");
                    isStomp = true;
                    _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, -context.ReadValue<float>() * jumping.stompForce);
                }
            }
        }
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (!isGrounded && _moveDirection != 0 && _gameController.dashEnable)
                {
                    Debug.Log("dash");
                    isDashing = true;
                    _currentDashTimer = jumping.StartDashTimer;
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
        }
        private void Jump(InputAction.CallbackContext context)
        {
            _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, context.ReadValue<float>() * jumping.jumpForce);
        }

        private void SpawnEffect()
        {
            var effects = Instantiate(jumping.jumpEffect, transform.position, Quaternion.identity);
            Destroy(effects, jumping.effectLiveTime);
        }
    }

}
