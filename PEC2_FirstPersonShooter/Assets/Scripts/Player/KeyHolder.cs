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

    /// <summary>
    /// Add a new type of key
    /// </summary>
    /// <param name="type">Type of Key picked up</param>
    public void AddKey(DoorType type)
    {
        keyTypesInHand.Add(type);
    }

    /// <summary>
    /// Check if a type of Key has been picked up
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool HasType(DoorType type)
    {
        return keyTypesInHand.Contains(type);
    }
}
