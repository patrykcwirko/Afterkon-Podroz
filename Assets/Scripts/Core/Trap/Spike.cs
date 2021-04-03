using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Trap
{
    public class Spike : MonoBehaviour
    {
        public float knockbackDuration = 0.2f;
        public float knockbackPower = 10f;
        public float damage = 10f;
        public bool isMoving;
        [SerializeField] private AnimationClip clip;
        [SerializeField] private int tickBetweenChange = 30;
        [SerializeField] private int timeOff = 2;

        private int _lastTimeActivate;
        private int _timeBetweenChange;
        private Animation _animation;

        void Awake()
        {
            _animation = GetComponent<Animation>();
            if(isMoving) _animation.AddClip(clip, clip.name);
            _timeBetweenChange = tickBetweenChange;
            TimeTickSystem.onTick += MoveOnTick;
        }

        private void MoveOnTick(object sender, TimeTickSystem.onTickEventArgs e)
        {
            if (e.tick >= _lastTimeActivate + _timeBetweenChange + timeOff)
            {
                _lastTimeActivate = e.tick;
                if(isMoving) _animation.Play(clip.name, PlayMode.StopAll);
                _timeBetweenChange = tickBetweenChange;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(other.gameObject.GetComponent<Player.PlayerMovement>().Knockback(knockbackDuration, knockbackPower, transform.position));
                other.gameObject.GetComponent<Player.PlayerCombat>().hearts.GetHeartSystem().Damage(damage);
            }
        }
    }

}

