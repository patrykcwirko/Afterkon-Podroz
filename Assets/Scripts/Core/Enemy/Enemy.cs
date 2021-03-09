using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IMonster
    {
        public MonsterData monster;
        public IBrain brain;


        private bool movingRight = true;
        private Transform raycastPoint;


        void Start()
        {
            raycastPoint = transform.Find("RayCastPoint");
        }

        void Update()
        {
            transform.Translate(Vector2.right * monster.Stats().speed * Time.deltaTime);
            transform.eulerAngles = brain.Think(raycastPoint);
        }

    }
}

