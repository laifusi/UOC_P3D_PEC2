using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField] DoorType type;

    protected override void PickUp(GameObject character)
    {
        character.GetComponent<KeyHolder>().AddKey(type);
    }
}
