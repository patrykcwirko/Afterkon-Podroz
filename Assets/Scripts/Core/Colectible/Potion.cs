using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] PotionSystemVisual potionSystem;

    private void OnTriggerEnter2D(Collider2D other) {
        Player.PlayerCombat player = other.GetComponent<Player.PlayerCombat>();
        if(player == null) return;
        potionSystem.AddPotion();
        Destroy(gameObject);
    }
}
