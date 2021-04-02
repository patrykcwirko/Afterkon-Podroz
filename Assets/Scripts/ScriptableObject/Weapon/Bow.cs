using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player/Weapon/Bow")]
public class Bow : Weapon
{
    [SerializeField] private GameObject arrowPrefab;
    public override void Attack(PlayerCombat player)
    {
        GameObject projectile = Instantiate(arrowPrefab, player.transform.Find("Weapon").position, Quaternion.identity);
        Vector2 dirToTarget = new Vector2( player.transform.localScale.x, 0f);
        projectile.GetComponent<Projectile>().direction = dirToTarget;
        projectile.GetComponent<Projectile>().damage = damageAmount;
        projectile.transform.localScale = player.transform.localScale;
    }

    public override void LongAttack(PlayerCombat player)
    {
        player.transform.Find("Weapon").right = new Vector2(1,-1);
        GameObject projectile = Instantiate(arrowPrefab, player.transform.Find("Weapon").position, player.transform.Find("Weapon").rotation);
        player.transform.Find("Weapon").right = Vector2.right;
        Vector2 dirToTarget = new Vector2(player.transform.localScale.x, 0f);
        projectile.GetComponent<Projectile>().direction = dirToTarget;
        projectile.GetComponent<Projectile>().damage = damageAmount;
        projectile.transform.localScale = player.transform.localScale;
    }

    public override void Setup(PlayerCombat player)
    {
        var anim = player.transform.GetComponent<Animation>();
        player.transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = weaponSprite;
        anim.RemoveClip("shortAttack");
        anim.RemoveClip("longAttack");
        anim.AddClip(shortAttack, "shortAttack");
        anim.AddClip(longAttack, "longAttack");

    }
}
