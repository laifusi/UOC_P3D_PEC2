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

    /// <summary>
    /// Start method to instantiate variables and listen for events
    /// </summary>
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

    /// <summary>
    /// Method to get damaged
    /// If there's no shield, we get full damage
    /// If there's shield, we get a percentage of the damage
    /// If life is 0 or lower, we die
    /// </summary>
    /// <param name="damage">Amount of damage received</param>
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

    /// <summary>
    /// Method to increase health
    /// </summary>
    /// <param name="healingValue">Amount of health increase</param>
    public void Heal(float healingValue)
    {
        life += healingValue;
        if (life > 100)
            life = 100;
        OnHealthChange?.Invoke(life);
    }

    /// <summary>
    /// Method to repair shield
    /// </summary>
    /// <param name="repairValue">Amount of repair</param>
    public void RepairShield(float repairValue)
    {
        shield += repairValue;
        if (shield > 100)
            shield = 100;
        OnShieldChange?.Invoke(shield);
    }

    /// <summary>
    /// Die method: We block the character and fire the death event
    /// </summary>
    private void Die()
    {
        BlockCharacter();
        OnDeath?.Invoke();
    }

    /// <summary>
    /// If the player wins of loses, we unlock the cursor and deactivate the FPSController and collider
    /// </summary>
    private void BlockCharacter()
    {
        fpsController.SetLockCursor(false);
        fpsController.enabled = false;
        collider.enabled = false;
    }

    /// <summary>
    /// OnDestroy we stop listening to events
    /// </summary>
    private void OnDestroy()
    {
        GameWinner.OnGameWon -= BlockCharacter;
    }
}
