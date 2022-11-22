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

    private void Start()
    {
        foreach (GameObject gun in gunsGO)
        {
            gun.SetActive(false);
        }

        SwitchGun();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SwitchGun();
        }
    }

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
