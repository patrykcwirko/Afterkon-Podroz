using System;
using System.Diagnostics;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/States")]
    public class StrStates : ScriptableObject
    {
        [HideInInspector] public bool isStompPushed;
        [HideInInspector] public bool isDashPushed;
        [HideInInspector] public bool isJumpPushed;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public bool isWall;
        [HideInInspector] public bool isObject;
        [HideInInspector] public bool canDoubleJump;
        [HideInInspector] public bool canStomp;
        [HideInInspector] public bool stomp;
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool interactable;
        [HideInInspector] public bool downPush;
        [HideInInspector] public bool pausePush;
    }

}
