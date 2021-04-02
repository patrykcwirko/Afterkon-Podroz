using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public partial class PlayerMovement : MonoBehaviour
    {

        [SerializeField] float speed = 5f;
        [SerializeField] JumpingConfig jumping;
        public Transform groundCheck;
        public Transform wallCheck;

        Animator _Animator;

        Rigidbody2D _rigidbody2D;
        GameController _gameController;
        PlayerInput _playerInput;

        private float _dashTimeLeft;
        private float _lastImageXpos;
        private float _lastDash = -100f;
        private bool _isDashing;

        void Start()
        {
            Setup();
        }

        private void Update()
        {
            ChangeAnimation();
            FlipSprite();
            Movement();
            Dash();
            CheckPositionInWorld();
        }

        private void Setup()
        {
            _Animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameController = FindObjectOfType<GameController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        public void Jump(InputAction.CallbackContext obj)
        {
            if (PauzeController.gameIsPaused) return;
            if (obj.phase != InputActionPhase.Started) return;
            if (_playerInput.states.isGrounded || _playerInput.states.isObject)
            {
                _rigidbody2D.velocity = Vector2.up * jumping.jumpForce;
                _playerInput.states.canStomp = true;
            }
            else if (_playerInput.states.canDoubleJump && _gameController.doubleJumpEvable)
            {
                _rigidbody2D.velocity = Vector2.up * jumping.jumpForce;
                SpawnEffect();
                _playerInput.states.canDoubleJump = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Destructible")
            {
                Debug.Log(_playerInput.states.stomp);
                if (_playerInput.states.stomp)
                {
                    _playerInput.states.stomp = false;
                    Destroy(collision.gameObject);
                    StartCoroutine(jumping.stompShake.Shake());
                }
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                if (_playerInput.states.stomp && _playerInput.states.canStomp)
                {
                    StartCoroutine(jumping.stompShake.Shake());
                    _playerInput.states.stomp = false;
                }
                _playerInput.states.canStomp = false;
            }
        }

        private void CheckPositionInWorld()
        {
            _playerInput.states.isGrounded = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsGround);
            _playerInput.states.isWall = Physics2D.OverlapCircle(wallCheck.position, jumping.checkRadius, jumping.whatIsGround);
            _playerInput.states.isObject = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsObject);
            if (_playerInput.states.isGrounded || _playerInput.states.isObject)
            {
                _playerInput.states.canDoubleJump = true;
                _playerInput.states.stomp = false;
            } 
        }

        private void Dash()
        {
            if(_playerInput.states.isDashPushed)
            {
                if(Time.time >= (_lastDash + jumping.dashCoolDown))
                {
                    _isDashing = true;
                    _dashTimeLeft = jumping.dashTime;
                    _lastDash = Time.time;

                    PlayerAfterImagePool.Instance.GetFromPool();
                    _lastImageXpos = transform.position.x;
                }
            }
            CheckDash();
        }

        private void CheckDash()
        {
            if (_isDashing)
            {
                if(_dashTimeLeft > 0)
                {
                    _rigidbody2D.velocity = transform.right * _playerInput.dashDirection * jumping.dashForce;
                    _dashTimeLeft -= Time.deltaTime;

                    if(Mathf.Abs(transform.position.x - _lastImageXpos) > jumping.distanceBetweenImages)
                    {
                        PlayerAfterImagePool.Instance.GetFromPool();
                        _lastImageXpos = transform.position.x;
                    }
                }

                if (_dashTimeLeft <= 0 || _playerInput.states.isWall)
                {
                    _isDashing = false;
                }
            }
        }

        private void Movement()
        {   
            if (!_playerInput.states.isWall)
            {
                _rigidbody2D.velocity = new Vector2(_playerInput.moveDirection * speed, _rigidbody2D.velocity.y);
            }
            else if (!_playerInput.states.isGrounded)
            {
                _rigidbody2D.velocity = new Vector3(0f, _rigidbody2D.velocity.y);
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(_rigidbody2D.velocity) * 0.25f, Color.green);
        }

        private void ChangeAnimation()
        {
            if (_playerInput.moveDirection == 0) _Animator.SetBool("isRunning", false);
            else _Animator.SetBool("isRunning", true);
        }

        private void FlipSprite()
        {
            if (_playerInput.moveDirection == 0) return;
            transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x) * _playerInput.moveDirection, 1f);
        }

        public void Stomp(InputAction.CallbackContext obj)
        {
            if (PauzeController.gameIsPaused) return;
            if (obj.phase != InputActionPhase.Started) return;
            _playerInput.states.downPush = true;
            if (!_playerInput.states.canStomp) return;
            _rigidbody2D.velocity += new Vector2(0, -jumping.stompForce);
            _playerInput.states.stomp = true;
        }

        private void SpawnEffect()
        {
            var effects = Instantiate(jumping.jumpEffect, transform.position, Quaternion.identity);
            Destroy(effects, jumping.effectLiveTime);
        }

        public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Vector3 obj)
        {
            float timer = 0;
            while(KnockbackDuration > timer)
            {
                timer += Time.deltaTime;
                Vector2 direction = (obj - this.transform.position).normalized;
                _rigidbody2D.AddForce(-direction * KnockbackPower);
            }

            yield return 0;
        }
    }

}
