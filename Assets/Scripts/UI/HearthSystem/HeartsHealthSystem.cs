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
    public const float HP_PER_HEART = 20f; 
    private List<Heart> heartList;
    private PlayerInfo info;
    private bool isSetting;

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

    public void SetHealth(PlayerInfo amount)
    {
        if (!info) info = amount;
        if (heartList.Count * HP_PER_HEART == info.health) return;
        if(heartList.Count*HP_PER_HEART < info.health)
        {
            isSetting = true;
            Heal(info.health - heartList.Count*HP_PER_HEART);
        }
        else
        {
            isSetting = true;
            Damage(heartList.Count*HP_PER_HEART - info.health);
        }
        isSetting = false;
    }

    public void Damage(float damageAmount)
    {
        if(!isSetting) info.health -= damageAmount;
        float damage = damageAmount / HP_PER_HEART;
        for (int i = heartList.Count -1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            if(damage > heart.GetValue())
            {
                damage -= heart.GetValue();
                heart.TakeDamage(heart.GetValue());
            } 
            else
            {
                heart.TakeDamage(damage);
                break;
            }
        }

        onDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead()) onDead?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float healAmount)
    {
        if (!isSetting) info.health += healAmount;
        float heal = healAmount / HP_PER_HEART;
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            float missingHP = MAX_HEARTH_VALUE - heart.GetValue();
            if(heal > missingHP)
            {
                heal -= missingHP;
                heart.Heal(missingHP);
            }
            else
            {
                heart.Heal(heal);
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
