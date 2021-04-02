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
        player.transform.GetComponent<Animation>().Play("shortAttack");

        if (isEnemy) isEnemy.gameObject.GetComponent<Enemy.EnemyController>().Damage(damageAmount);
    }
    public override void LongAttack(Player.PlayerCombat player)
    {
        var isEnemy = Physics2D.OverlapCircle(player.transform.Find("Weapon").position, 0.5f, enemyLayer);
        player.transform.GetComponent<Animation>().Play("longAttack");

        if (isEnemy) isEnemy.gameObject.GetComponent<Enemy.EnemyController>().Damage(damageAmount);
    }

    public override void Setup(PlayerCombat player)
    {
        var anim = player.transform.GetComponent<Animation>();
        player.transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = weaponSprite;
        if(anim.GetClipCount() > 0)
        {
            anim.RemoveClip("shortAttack");
            anim.RemoveClip("longAttack");
        }
        anim.AddClip(shortAttack, "shortAttack");
        anim.AddClip(longAttack, "longAttack");
    }
}
