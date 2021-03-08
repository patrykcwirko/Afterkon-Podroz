using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Trap
{
    public class Laser : MonoBehaviour, ITrap
    {
        [SerializeField] Transform laser;
        [SerializeField] float size;
        [SerializeField] float knockbackPower = 1f;
        [SerializeField] float knockbackDuration = 10f;
        [SerializeField] int tickBetweenChange = 10;
        [SerializeField] float timeOff = 1;

        private BoxCollider2D _laseCollider;
        private int _lastTimeActivate = 0;

        float ITrap.size { get => size; set => size = value; }
        float ITrap.KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
        float ITrap.KnockbackDuration { get => knockbackDuration; set => knockbackDuration = value; }

        void Start()
        {
            _laseCollider = GetComponent<BoxCollider2D>();
            ResizeTrap();
            TimeTickSystem.Create();
            TimeTickSystem.onTick += LaserOnTick;
        }

        private void LaserOnTick(object sender, TimeTickSystem.onTickEventArgs e)
        {
            if (e.tick >= _lastTimeActivate + tickBetweenChange + timeOff)
            {
                _lastTimeActivate = e.tick;
                TurnOff();
                Invoke("TurnOn", timeOff);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine( other.gameObject.GetComponent<Player.PlayerMovement>().Knockback(knockbackDuration, knockbackPower, transform.position) );
                other.gameObject.GetComponent<Player.PlayerMovement>().hearts.GetHeartSystem().Damage(10f);
            }
        }

        public void TurnOff()
        {
            this.gameObject.SetActive(false);
        }

        public void TurnOn()
        {
            this.gameObject.SetActive(true);
        }

        [ContextMenu("ResizeLaser")]
        public void ResizeTrap()
        {
            laser.localScale = new Vector2(laser.localScale.x, size);
            _laseCollider.size = new Vector2(1, size);
        }
    }
}

