using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public float damageAmount = 10f;
    public Sprite weaponSprite;
    public AnimationClip shortAttack;
    public AnimationClip longAttack;

    public abstract void Setup(Player.PlayerCombat player);
    public abstract void Attack(Player.PlayerCombat player);
}
