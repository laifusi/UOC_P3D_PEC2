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
            life -= damage * (1 - shieldPercentageProtection);
        }

        if(life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("DIE");
    }
}
