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

        private PlayerInput _playerInput;
        
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
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
    }
}
