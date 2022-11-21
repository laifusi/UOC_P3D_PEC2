using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMunitionIndicator : MonoBehaviour
{
    [SerializeField] MunitionType type;
    [SerializeField] GameObject[] bullets;

    private void Start()
    {
        Shooter.OnAmmoChange += UpdateBullets;
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
}

public enum MunitionType
{
    Pistol, LongRangeGun
}
