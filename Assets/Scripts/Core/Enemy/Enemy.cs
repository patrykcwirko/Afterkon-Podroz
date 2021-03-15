﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IMonster
    {
        public MonsterData monster;
        public Brain brain;
        public float knockbackDuration = 2;
        public float knockbackPower = 20;


        private bool movingRight = true;
        private Transform raycastPoint;
        private Vector3 _dir;

        void Start()
        {
            raycastPoint = transform.Find("RayCastPoint");
        }

        void Update()
        {
            _dir = brain.Think(raycastPoint);

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(other.gameObject.GetComponent<Player.PlayerMovement>().Knockback(knockbackDuration, knockbackPower, transform.position));
                other.gameObject.GetComponent<Player.PlayerCombat>().hearts.GetHeartSystem().Damage(10f);
            }
        }

    }
}

