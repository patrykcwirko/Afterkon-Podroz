using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

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

        private float _currentDashTimer = 0;
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
            Stomp();
            Movement();
            Dash();
            Jump();
            CheckPositionInWorld();
        }

        private void Setup()
        {
            _Animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameController = FindObjectOfType<GameController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                Debug.Log("Destroy");
                if (_playerInput.states.isStompPushed)
                {
                    _playerInput.states.isStompPushed = false;
                    Destroy(collision.gameObject);
                }
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
                if ( _playerInput.states.isStompPushed)
                {
                    StartCoroutine(jumping.stompShake.Shake());
                }
            } 
            if (_playerInput.states.isGrounded) _playerInput.states.isStompPushed = false;
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
            //if(_playerInput.moveDirection == 0) return;
            if (!_playerInput.states.isWall)
            {
                _rigidbody2D.velocity = new Vector2(_playerInput.moveDirection * speed, _rigidbody2D.velocity.y);
            }
            else if (!_playerInput.states.isGrounded)
            {
                _rigidbody2D.velocity = new Vector3(0f, _rigidbody2D.velocity.y);
            }
        }

        private void ChangeAnimation()
        {
            if (_playerInput.moveDirection == 0) _Animator.SetBool("isRunning", false);
            else _Animator.SetBool("isRunning", true);
        }

        private void FlipSprite()
        {
            if (_playerInput.moveDirection == 0) return;
            transform.localScale = new Vector2(Mathf.Sign(_playerInput.moveDirection), 1f);
        }

        private void Jump()
        {
            if(!_playerInput.states.isJumpPushed) return;
            if (_playerInput.states.isGrounded || _playerInput.states.isObject)
            {
                _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, jumping.jumpForce);
            }
            else if (_playerInput.states.canDoubleJump && _gameController.doubleJumpEvable)
            {
                Debug.Log("Double jump");
                _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, jumping.jumpForce);
                SpawnEffect();
                _playerInput.states.canDoubleJump = false;
            }
            _playerInput.states.isJumpPushed = false;
        }

        private void Stomp()
        {
            if(!_playerInput.states.isStompPushed) return;
            _rigidbody2D.velocity += new Vector2(0, -jumping.stompForce);
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
