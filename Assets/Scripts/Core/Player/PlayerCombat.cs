using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public HeartsHealthVisual hearts;
        public PotionSystemVisual potionSystem;
        public float invisibilityAfterHurt = 2f;

        private PlayerInput _playerInput;
        private Animator _animator;
        
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_playerInput.healPush)
            {
                Debug.Log("Heal");
                potionSystem.UsePotion();
                hearts.GetHeartSystem().Heal(20);
                _playerInput.healPush = false;
            }
        }

        public void TriggerHurt()
        {
            StartCoroutine(HurtBlinker(invisibilityAfterHurt));
        }

        IEnumerator HurtBlinker(float hurtTime)
        {
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            int projectileLayer = LayerMask.NameToLayer("Projectile");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
            Physics2D.IgnoreLayerCollision(projectileLayer, playerLayer);

            _animator.SetLayerWeight(1,1);

            yield return new WaitForSeconds(hurtTime);

            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            Physics2D.IgnoreLayerCollision(projectileLayer, playerLayer, false);
            _animator.SetLayerWeight(1, 0);
        }
    }
}
