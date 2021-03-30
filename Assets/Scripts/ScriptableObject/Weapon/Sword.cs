using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapon/Sword")]
public class Sword : Weapon
{
    public LayerMask enemyLayer;
    public override void Attack(Player.PlayerCombat player)
    {
        var isEnemy = Physics2D.OverlapCircle(player.transform.Find("Weapon").position, 0.5f, enemyLayer);

        if (isEnemy) isEnemy.gameObject.GetComponent<Enemy.EnemyController>().Damage(damageAmount);
    }

    public override void Setup(PlayerCombat player)
    {
        player.transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = weaponSprite;
    }
}
