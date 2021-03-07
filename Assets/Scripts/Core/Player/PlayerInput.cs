using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public partial class PlayerInput : MonoBehaviour
    {
        public float moveDirection;
        public float dashDirection;
        public StrStates states;

        private const float DOUBLE_CLICK_TIME = .2f;
        private float _lastDirection = 0;
        private float _lastClickTime;
        private GameController _gameController;

        private void Start() 
        {
            _gameController = FindObjectOfType<GameController>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            else moveDirection = context.ReadValue<float>();
            if (context.phase == InputActionPhase.Started)
            {
                if (moveDirection != 0 ) _lastDirection = moveDirection;
                if (_lastDirection != moveDirection) return;
                float timeSinceLastClick = Time.time - _lastClickTime;
                if (timeSinceLastClick <= DOUBLE_CLICK_TIME && moveDirection != 0)
                {
                    Debug.Log("dash");
                    dashDirection = moveDirection;
                    states.isDashing = true;                                           
                }  
            }
            _lastClickTime = Time.time;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            Debug.Log("jump Input");
            states.isJumping = true;
        }

        public void OnStomp(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;
            if (!states.isGrounded && _gameController.stompEnable)
            {
                Debug.Log("Stomp");
                states.isStomp = true;      
            }
        }
    }

}
