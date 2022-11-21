using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : Item
{
    [SerializeField] int amountOfAmmo = 2;
    [SerializeField] MunitionType type;

    protected override void PickUp(GameObject character)
    {
        Shooter[] guns = character.GetComponentsInChildren<Shooter>();
        foreach (Shooter gun in guns)
        {
            gun.AddAmmo(type, amountOfAmmo);
        }
    }
}
