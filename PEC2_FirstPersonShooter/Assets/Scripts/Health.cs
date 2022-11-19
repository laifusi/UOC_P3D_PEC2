using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxLife = 100;
    [SerializeField] float maxShield = 100;
    [Range(0, 1)][SerializeField] float shieldPercentageProtection = 0.75f;

    private float life;
    private float shield;

    public static Action<float> OnShieldChange;
    public static Action<float> OnHealthChange;

    private void Start()
    {
        life = maxLife;
        shield = maxShield;
    }

    public void GetHurt(float damage)
    {
        if(shield <= 0)
        {
            life -= damage;
        }
        else
        {
            shield -= damage * shieldPercentageProtection;
            if (shield < 0)
                shield = 0;
            life -= damage * (1 - shieldPercentageProtection);
        }

        OnShieldChange?.Invoke(shield);
        OnHealthChange?.Invoke(life);

        if(life <= 0)
        {
            Die();
        }
    }

    public void Heal(float healingValue)
    {
        life += healingValue;
        if (life > 100)
            life = 100;
        OnHealthChange?.Invoke(life);
    }

    public void RepairShield(float repairValue)
    {
        shield += repairValue;
        if (shield > 100)
            shield = 100;
        OnShieldChange?.Invoke(shield);
    }

    private void Die()
    {
        Debug.Log("DIE");
    }
}
