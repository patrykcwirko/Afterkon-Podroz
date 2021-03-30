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

    public override void Setup(PlayerCombat player)
    {
        player.transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = weaponSprite;
        player.transform.Find("Weapon").Rotate(new Vector3(0,0,-45f));

    }
}
