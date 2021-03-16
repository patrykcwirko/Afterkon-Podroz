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

