using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private AnimationClip shortAttack;
        public HeartsHealthVisual hearts;
        public PotionSystemVisual potionSystem;
        public float invisibilityAfterHurt = 2f;

        private PlayerInput _playerInput;
        private Animator _animator;
        private Animation _anim;
        
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _anim = GetComponent<Animation>();
            _anim.AddClip(shortAttack, "shortAttack");
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
            if (!_anim.IsPlaying("shortAttack"))  transform.Find("Weapon").gameObject.SetActive(false);
        }

        public void TriggerHurt()
        {
            StartCoroutine(HurtBlinker(invisibilityAfterHurt));
        }

        public void ShortAttack(InputAction.CallbackContext context)
        {
            transform.Find("Weapon").gameObject.SetActive(true);
            _anim.Play("shortAttack");
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
