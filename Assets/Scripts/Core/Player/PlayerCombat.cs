using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCombat : MonoBehaviour, IEntityController
    {
        [SerializeField] public Weapon sword;
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
            hearts = FindObjectOfType<HeartsHealthVisual>();
            potionSystem = FindObjectOfType<PotionSystemVisual>();
            sword.Setup(this);
            _anim.AddClip(sword.shortAttack, "shortAttack");
            _anim.AddClip(sword.longAttack, "longAttack");
        }

        void Update()
        {
            if (_playerInput.healPush)
            {
                potionSystem.UsePotion();
                hearts.GetHeartSystem().Heal(20);
                _playerInput.healPush = false;
            }
            if (!_anim.isPlaying)  transform.Find("Weapon").gameObject.SetActive(false);
        }

        public void TriggerHurt()
        {
            StartCoroutine(HurtBlinker(invisibilityAfterHurt));
        }

        public void ShortAttack(InputAction.CallbackContext context)
        {
            if (PauzeController.gameIsPaused) return;
            if (context.phase != InputActionPhase.Started) return;
            transform.Find("Weapon").gameObject.SetActive(true);
            sword.Attack(this);
        }
        
        public void LongAttack(InputAction.CallbackContext context)
        {
            if (PauzeController.gameIsPaused) return;
            if (context.phase != InputActionPhase.Started) return;
            transform.Find("Weapon").gameObject.SetActive(true);
            sword.LongAttack(this);
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

        public void TakeDamge(float damage)
        {
            hearts.GetHeartSystem().Damage(damage);
            TriggerHurt();
        }
    }
}
