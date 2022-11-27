using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] GameObject[] gunsGO;
    [SerializeField] Gun[] guns;

    private int currentGun;

    public static Action<MunitionType> OnGunSwitch;

    /// <summary>
    /// Start method to deactivate all the guns and activate the first one
    /// </summary>
    private void Start()
    {
        foreach (GameObject gun in gunsGO)
        {
            gun.SetActive(false);
        }

        SwitchGun();
    }

    /// <summary>
    /// Update method to check if the player changes guns
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SwitchGun();
        }
    }

    /// <summary>
    /// Method to do the switch: we deactivate the current gun and activate the next one
    /// </summary>
    public void SwitchGun()
    {
        gunsGO[currentGun].SetActive(false);
        guns[currentGun].ActivateGun(false);

        currentGun = (currentGun + 1) % gunsGO.Length;

        gunsGO[currentGun].SetActive(true);
        guns[currentGun].ActivateGun(true);

        OnGunSwitch?.Invoke(guns[currentGun].MunitionType);
    }
}
