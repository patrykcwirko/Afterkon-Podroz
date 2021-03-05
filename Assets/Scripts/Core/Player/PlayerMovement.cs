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

        private const float DOUBLE_CLICK_TIME = .2f;

        Animator _Animator;
        Rigidbody2D _rigidbody2D;
        GameController _gameController;

        float _moveDirection;
        float _dashDirection;
        float _lastDirection = 0;
        float _currentDashTimer;
        float _lastClickTime;

        StrStates states;

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
            states.isStomp = false;
        }

        private void Setup()
        {
            _Animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void CheckPositionInWorld()
        {
            states.isGrounded = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsGround);
            states.isWall = Physics2D.OverlapCircle(wallCheck.position, jumping.checkRadius, jumping.whatIsGround);
            states.isObject = Physics2D.OverlapCircle(groundCheck.position, jumping.checkRadius, jumping.whatIsObject);
            if (states.isGrounded || states.isObject) states.canDoubleJump = true;
        }

        private void Dash()
        {
            if (!states.isDashing) return;
            Debug.Log($"Test: {transform.right * _dashDirection * jumping.dashForce}");
            _rigidbody2D.velocity = transform.right * _dashDirection * jumping.dashForce;
            _currentDashTimer -= Time.deltaTime;
            if (_currentDashTimer <= Mathf.Epsilon)
            {
                _dashDirection = 0;
                states.isDashing = false;
            }
        }

        private void Move()
        {
            if (!states.isWall)
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
                if (states.isStomp)
                {
                    states.isStomp = false;
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
            if (_moveDirection == 0) return;
            transform.localScale = new Vector2(Mathf.Sign(_moveDirection), 1f);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            else _moveDirection = context.ReadValue<float>();
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log(_moveDirection);
                if (_lastDirection == _moveDirection)
                {
                    float timeSinceLastClick = Time.time - _lastClickTime;
                    if (timeSinceLastClick <= DOUBLE_CLICK_TIME && _moveDirection != 0)
                    {
                        Debug.Log("dash");
                        _dashDirection = _moveDirection;
                        states.isDashing = true;
                        _currentDashTimer = jumping.StartDashTimer;
                        _rigidbody2D.velocity = Vector2.zero;
                    }
                }
                _lastDirection = _moveDirection;
            }
            _lastClickTime = Time.time;
        }
        public void OnJump(InputAction.CallbackContext context)
        {

            if (context.phase != InputActionPhase.Started) return;
            if (states.isGrounded || states.isObject)
            {
                Debug.Log("jump");
                Jump(context);
            }
            else if (states.canDoubleJump && _gameController.doubleJumpEvable)
            {
                Debug.Log("Double jump");
                Jump(context);
                SpawnEffect();
                states.canDoubleJump = false;
            }
        }
        public void OnStomp(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            if (!states.isGrounded && _gameController.stompEnable)
            {
                Debug.Log("Stomp");
                states.isStomp = true;
                _rigidbody2D.velocity += new Vector2(_rigidbody2D.velocity.x, -context.ReadValue<float>() * jumping.stompForce);
            }
        }
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            if (!states.isGrounded && _moveDirection != 0 && _gameController.dashEnable)
            {
                Debug.Log("dash");
                states.isDashing = true;
                _currentDashTimer = jumping.StartDashTimer;
                _rigidbody2D.velocity = Vector2.zero;
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
