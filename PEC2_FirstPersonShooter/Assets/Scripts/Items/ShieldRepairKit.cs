using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRepairKit : Item
{
    [SerializeField] float repairValue = 20;

    protected override void PickUp(GameObject character)
    {
        character.GetComponent<Health>().RepairShield(repairValue);
    }
}
