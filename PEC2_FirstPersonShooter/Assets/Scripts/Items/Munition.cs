using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : Item
{
    [SerializeField] int amountOfAmmo = 2;
    [SerializeField] MunitionType type;

    protected override void PickUp(GameObject character)
    {
        Gun[] guns = character.GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns)
        {
            gun.AddAmmo(type, amountOfAmmo);
        }
    }
}
