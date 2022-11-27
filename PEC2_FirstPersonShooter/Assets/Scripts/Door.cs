using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] DoorType type; // Type of Door
    Animator animator;              // Animator component
    bool isOpen;                    // bool to define if the door is open or closed

    /// <summary>
    /// Awake method: cache the Animator
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter: If the player entered the trigger and has the key (or it isn't needed), we open the door
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isOpen)
            return;

        KeyHolder character = other.GetComponent<KeyHolder>();
        if (character != null)
        {
            if(type == DoorType.KeyLess || character.HasType(type))
            {
                animator.SetTrigger("Switch");
                isOpen = true;
            }
        }
    }

    /// <summary>
    /// OnTriggerExit: If the player exited the trigger and has the key (or it isn't needed), we close the door
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (!isOpen)
            return;

        KeyHolder character = other.GetComponent<KeyHolder>();
        if (character != null)
        {
            if (type == DoorType.KeyLess || character.HasType(type))
            {
                animator.SetTrigger("Switch");
                isOpen = false;
            }
        }
    }
}

/// <summary>
/// Enum to the define the type of doors and keys
/// </summary>
public enum DoorType
{
    KeyLess, RedKey, BlueKey, GreenKey, BlackKey
}
