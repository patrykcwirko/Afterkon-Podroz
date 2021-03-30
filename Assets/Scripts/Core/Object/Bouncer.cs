using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float bounceForce = 100f;
    [SerializeField] private AnimationClip bounceClip;

    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        _anim.AddClip(bounceClip, "BounceAnim");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var playerInput = other.GetComponent<Player.PlayerInput>();
            if(playerInput.states.stomp)
            {
                _anim.Play("BounceAnim", PlayMode.StopAll);
                other.GetComponent<Rigidbody2D>().velocity = Vector2.up * bounceForce;
            }
        }
    }
}
