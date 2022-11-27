using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Health : MonoBehaviour
{
    [SerializeField] float maxLife = 100;
    [SerializeField] float maxShield = 100;
    [Range(0, 1)][SerializeField] float shieldPercentageProtection = 0.75f;

    private float life;
    private float shield;
    private FirstPersonController fpsController;
    private Collider collider;

    public static Action<float> OnShieldChange;
    public static Action<float> OnHealthChange;
    public static Action OnDeath;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = true;

        fpsController = GetComponent<FirstPersonController>();
        fpsController.enabled = true;

        life = maxLife;
        shield = maxShield;

        GameWinner.OnGameWon += BlockCharacter;
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
        BlockCharacter();
        OnDeath?.Invoke();
    }

    private void BlockCharacter()
    {
        fpsController.SetLockCursor(false);
        fpsController.enabled = false;
        collider.enabled = false;
    }

    private void OnDestroy()
    {
        GameWinner.OnGameWon -= BlockCharacter;
    }
}
