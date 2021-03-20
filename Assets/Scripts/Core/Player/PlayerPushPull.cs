using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerPushPull : MonoBehaviour
    {
        public float pushPullDistance = 0.3f;
        public LayerMask layer;

        private PlayerInput _playerInput;
        private GameObject _crate;

        private void Start() 
        {
            _playerInput = GetComponent<PlayerInput>();    
        }

        private void Update() 
        {
            PushPull();    
        }

        private void PushPull()
        {
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), transform.localScale.x * pushPullDistance, layer);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right)* transform.localScale.x * pushPullDistance, Color.blue);
            if(hit.collider != null && hit.collider.gameObject.tag == "Interactive")
            {
                _crate = hit.collider.gameObject;
                if(_playerInput.states.isPushPull)
                {
                    _crate.GetComponent<FixedJoint2D>().enabled = true;
                    _crate.GetComponent<Crate>().beingPushed = true;
                    _crate.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                }

            }
            if(_crate != null && ! _playerInput.states.isPushPull)
            {
                _crate.GetComponent<FixedJoint2D>().enabled = false;
                _crate.GetComponent<Crate>().beingPushed = false;
            }
        }
    }
}

