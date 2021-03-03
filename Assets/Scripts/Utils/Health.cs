using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Health : MonoBehaviour
{
    public event EventHandler onHealthChanged;

    private float health;

    private float MaxHealth;

    public Health(float MaxHeatlh)
    {
        this.MaxHealth = MaxHeatlh;
        health = MaxHeatlh;
    }

    public float GetHeatlh() { return health; }

    public float GetHeatlhPercent() { return health / MaxHealth; }

    public bool IsZeroHealth() { return health <= 0; }

    public void DealDamage(float amont)
    {
        health -= amont;
        if(health <= 0) { health = 0f; }
        if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(float amont)
    {
        health += amont;
        if (health > MaxHealth) health = MaxHealth;
        if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
    }
}
