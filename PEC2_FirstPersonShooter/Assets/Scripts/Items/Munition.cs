using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : Item
{
    [SerializeField] int amountOfAmmo = 2;

    protected override void PickUp(GameObject character)
    {
        character.GetComponentInChildren<Shooter>(false).AddAmmo(amountOfAmmo);
    }
}
