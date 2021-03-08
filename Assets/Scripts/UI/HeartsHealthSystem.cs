using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthSystem
{
    public event EventHandler onDamaged;
    public event EventHandler onHealed;
    public event EventHandler onDead;

    public const float MAX_HEARTH_VALUE = 1f;
    private List<Heart> heartList;

    public HeartsHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();
        for (int i = 0; i < heartAmount; i++)
        {
            Heart heart = new Heart(1);
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }

    public void Damage(float damageAmount)
    {
        for (int i = heartList.Count -1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            if(damageAmount > heart.GetValue())
            {
                damageAmount -= heart.GetValue();
                heart.TakeDamage(heart.GetValue());
            } 
            else
            {
                heart.TakeDamage(damageAmount);
                break;
            }
        }

        onDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead()) onDead?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmount)
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            float missingHP = MAX_HEARTH_VALUE - heart.GetValue();
            if(healAmount > missingHP)
            {
                healAmount -= missingHP;
                heart.Heal(missingHP);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }
        }
        onHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return heartList[0].GetValue() == 0;
    }

    public class Heart
    {
        private float value;

        public Heart(float value)
        {
            this.value = value;
        }

        public float GetValue()
        {
            return value;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }

        public void TakeDamage(float damageAmount)
        {
            if (damageAmount >= value)
            {
                value = 0;
            }
            else
            {
                value -= damageAmount;
            }
        }

        public void Heal(float healAmount)
        {
            if(value + healAmount > MAX_HEARTH_VALUE)
            {
                value = MAX_HEARTH_VALUE;
            }
            else
            {
                value += healAmount;
            }
        }
    }
}
