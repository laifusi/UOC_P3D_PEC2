using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Item
{
    [SerializeField] float healthAddition = 20;

    protected override void PickUp(GameObject character)
    {
        character.GetComponent<Health>().Heal(healthAddition);
    }
}
