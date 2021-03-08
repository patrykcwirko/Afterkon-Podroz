using System;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "newJumpConfig", menuName = "Player/Jumping Configuration")]
    public class JumpingConfig: ScriptableObject
    {
        [Header("Jump")]
        public float jumpForce = 5f;

        [Header("Stomp")]
        public float stompForce = 5f;
        public CameraShake stompShake;

        [Header("Dash")]
        public float dashForce = 5f;
        public float dashTime = 5f;
        public float distanceBetweenImages = 0.1f;
        public float dashCoolDown;

        [Header("Check contact")]
        public float checkRadius;
        public LayerMask whatIsGround;
        public LayerMask whatIsObject;

        [Header("Double jump efects")]
        public GameObject jumpEffect;
        public float effectLiveTime = 0.3f;
    }
}
