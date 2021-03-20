﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public partial class PlayerInput : MonoBehaviour
    {
        public float moveDirection;
        public float dashDirection;
        public StrStates states;
        public float pushPullDistance = 0.3f;
        public LayerMask layer;

        [HideInInspector] public bool healPush;
        private const float DOUBLE_CLICK_TIME = .2f;
        private float _lastDirection = 0;
        private float _lastClickTime;
        private GameController _gameController;

        private void Start() 
        {
            _gameController = FindObjectOfType<GameController>();
        }

        private void Update() 
        {
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), transform.localScale.x * pushPullDistance, layer);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * transform.localScale.x * pushPullDistance, Color.blue);
            if (hit.collider != null && hit.collider.gameObject.tag == "Interactive")
            {
                transform.Find("ActionIcon").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("ActionIcon").gameObject.SetActive(false);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            else moveDirection = context.ReadValue<float>();
            if (context.phase == InputActionPhase.Canceled) states.isDashPushed = false;
            if (context.phase == InputActionPhase.Started)
            {
                if (moveDirection != 0 ) _lastDirection = moveDirection;
                if (_lastDirection != moveDirection) return;
                float timeSinceLastClick = Time.time - _lastClickTime;
                if (timeSinceLastClick <= DOUBLE_CLICK_TIME && moveDirection != 0)
                {
                    dashDirection = moveDirection;
                    states.isDashPushed = true;                                           
                }  
                _lastClickTime = Time.time;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            if (context.phase == InputActionPhase.Canceled) states.isJumpPushed = false;
            if (context.phase == InputActionPhase.Started)  states.isJumpPushed = true;
        }

        public void OnStomp(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            if (context.phase == InputActionPhase.Canceled) states.isStompPushed = false;
            if (context.phase == InputActionPhase.Started) 
            {
                if (!states.isGrounded && _gameController.stompEnable)
                {
                    states.isStompPushed = true;      
                }
            }
        }

        public void OnPushPull(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) return;
            if (context.phase == InputActionPhase.Canceled) states.isPushPull = false;
            if (context.phase == InputActionPhase.Started) states.isPushPull = true;
        }

        public void OnHeal(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                healPush = true;
            } 
        }
    }

}
