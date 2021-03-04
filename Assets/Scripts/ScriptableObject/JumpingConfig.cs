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

        [Header("Dash")]
        public float dashForce = 5f;
        public float StartDashTimer = 5f;

        [Header("Check contact")]
        public float checkRadius;
        public LayerMask whatIsGround;
        public LayerMask whatIsObject;

        [Header("Double jump efects")]
        public GameObject jumpEffect;
        public float effectLiveTime = 0.3f;
    }
}
