using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] DoorType type;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController character = other.GetComponent<CharacterController>();
        if (character != null)
        {
            if(type == DoorType.KeyLess /*|| Check if character has key*/)
            animator.SetTrigger("Switch");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController character = other.GetComponent<CharacterController>();
        if (character != null)
        {
            animator.SetTrigger("Switch");
        }
    }
}

public enum DoorType
{
    KeyLess, KeyX, KeyY
}
