using System;
using System.Diagnostics;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/States")]
    public class StrStates : ScriptableObject
    {
        public bool isStompPushed;
        public bool isDashPushed;
        public bool isJumpPushed;
        public bool isGrounded;
        public bool isWall;
        public bool isObject;
        public bool canDoubleJump;
        public bool canStomp;
        public bool stomp;
        public bool isMoving;
        public bool interactable;
        public bool downPush;
        public bool pausePush;
    }

}
