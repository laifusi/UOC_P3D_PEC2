using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMunitionIndicator : MonoBehaviour
{
    [SerializeField] MunitionType type;
    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject UIPanel;

    /// <summary>
    /// Method to start listening for gun change events
    /// </summary>
    private void Start()
    {
        Gun.OnAmmoChange += UpdateBullets;
        GunHolder.OnGunSwitch += ActivateIndicator;
    }

    /// <summary>
    /// Method to change the amount of bullets
    /// If the type of munition is correct: activate the total number of bullets had and deactivate the ones used
    /// </summary>
    /// <param name="typeShot"></param>
    /// <param name="totalBullets"></param>
    private void UpdateBullets(MunitionType typeShot, int totalBullets)
    {
        if(typeShot == type)
        {
            for(int i = 0; i < bullets.Length; i++)
            {
                bullets[i].SetActive(i < totalBullets);
            }
        }
    }

    /// <summary>
    /// If the type of gun in hand corresponds to this Indicator, activate its Panel
    /// </summary>
    /// <param name="typeActive"></param>
    private void ActivateIndicator(MunitionType typeActive)
    {
        if(typeActive == type)
        {
            UIPanel.SetActive(true);
        }
        else
        {
            UIPanel.SetActive(false);
        }
    }

    /// <summary>
    /// OnDestroy: stop listening to events
    /// </summary>
    private void OnDestroy()
    {
        Gun.OnAmmoChange -= UpdateBullets;
        GunHolder.OnGunSwitch -= ActivateIndicator;
    }
}

/// <summary>
/// Enum for the type of Gun and Munition
/// </summary>
public enum MunitionType
{
    Pistol, LongRangeGun
}
