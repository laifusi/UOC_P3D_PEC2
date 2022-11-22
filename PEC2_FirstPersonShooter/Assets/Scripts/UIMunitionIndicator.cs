using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMunitionIndicator : MonoBehaviour
{
    [SerializeField] MunitionType type;
    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject UIPanel;

    private void Start()
    {
        Gun.OnAmmoChange += UpdateBullets;
        GunHolder.OnGunSwitch += ActivateIndicator;
    }

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
}

public enum MunitionType
{
    Pistol, LongRangeGun
}
