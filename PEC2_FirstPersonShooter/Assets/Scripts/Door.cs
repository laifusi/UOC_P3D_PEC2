using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] DoorType type;
    Animator animator;
    bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen)
            return;

        Debug.Log("Enter");
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

    private void OnTriggerExit(Collider other)
    {
        if (!isOpen)
            return;

        Debug.Log("Exit");
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

public enum DoorType
{
    KeyLess, KeyX, KeyY
}
