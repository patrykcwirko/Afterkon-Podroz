using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] public float damage = 25f;

    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) Destroy(gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            Player.PlayerCombat player = other.gameObject.GetComponent<Player.PlayerCombat>();
            player.hearts.GetHeartSystem().Damage(damage);
            player.TriggerHurt();
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy.EnemyController enemy = other.gameObject.GetComponent<Enemy.EnemyController>();
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }
}
