using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<DoorType> keyTypesInHand;

    private void Start()
    {
        keyTypesInHand = new List<DoorType>();
    }

    public void AddKey(DoorType type)
    {
        keyTypesInHand.Add(type);
    }

    public bool HasType(DoorType type)
    {
        return keyTypesInHand.Contains(type);
    }
}
