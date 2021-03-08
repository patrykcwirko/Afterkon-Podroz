using System;
using System.Diagnostics;

namespace Player
{
    public struct StrStates
    {
        public bool isStompPushed;
        public bool isDashPushed;
        public bool isJumpPushed;
        public bool isGrounded;
        public bool isWall;
        public bool isObject;
        public bool canDoubleJump;
        public bool isMoving;
    }

}
