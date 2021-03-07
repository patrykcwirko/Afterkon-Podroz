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

        float _currentDashTimer;

        void Start()
        {
            Setup();
        }

        private void Update()
        {
            ChangeAnimation();
            FlipSprite();
            Stomp();
            Move();
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

        private void CheckPositionInWorld()
        {
            _playerInput.states.isGrounded = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsGround);
            _playerInput.states.isWall = Physics2D.OverlapCircle(wallCheck.position, jumping.checkRadius, jumping.whatIsGround);
            _playerInput.states.isObject = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsObject);
            if (_playerInput.states.isGrounded || _playerInput.states.isObject)
            {
                _playerInput.states.canDoubleJump = true;
                if ( _playerInput.states.isStomp)
                {
                    StartCoroutine(jumping.stompShake.Shake());
                }
            } 
            if (_playerInput.states.isGrounded) _playerInput.states.isStomp = false;
        }

        private void Dash()
        {
            if (!_playerInput.states.isDashing) return;
            _currentDashTimer = jumping.StartDashTimer;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.velocity = transform.right * _playerInput.dashDirection * jumping.dashForce;
            _currentDashTimer -= Time.deltaTime;
            if (_currentDashTimer <= Mathf.Epsilon)
            {
                _playerInput.dashDirection = 0;
                _playerInput.states.isDashing = false;
            }
        }

        private void Move()
        {   
            if(_playerInput.moveDirection == 0) return;
            if (!_playerInput.states.isWall)
            {
                Debug.Log("Move");
                _rigidbody2D.velocity = new Vector2(_playerInput.moveDirection * speed, _rigidbody2D.velocity.y);
            }
            else if (!_playerInput.states.isGrounded)
            {
                _rigidbody2D.velocity = new Vector3(0f, _rigidbody2D.velocity.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                Debug.Log("Destroy");
                if (_playerInput.states.isStomp)
                {
                    _playerInput.states.isStomp = false;
                    Destroy(collision.gameObject);
                }
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
            if(!_playerInput.states.isJumping) return;
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
            _playerInput.states.isJumping = false;
        }

        private void Stomp()
        {
            if(_playerInput.states.isStomp)
            {
                _rigidbody2D.velocity += new Vector2(0, -jumping.stompForce);
            }
        }
        private void SpawnEffect()
        {
            var effects = Instantiate(jumping.jumpEffect, transform.position, Quaternion.identity);
            Destroy(effects, jumping.effectLiveTime);
        }

        public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Transform obj){
            float timer = 0;
            Debug.Log("Hit");
            while(KnockbackDuration > timer)
            {
                timer += Time.deltaTime;
                Vector2 direction = (obj.transform.position - this.transform.position).normalized;
                _rigidbody2D.AddForce(-direction * KnockbackPower);
            }

            yield return 0;
        }
    }

}
