using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public abstract class Item : MonoBehaviour
{
    private AudioSource audioSource;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// If the player picks an item: play the sound, do the actions defined in the child classes and destroy the object
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Play();
            PickUp(other.gameObject);
            Destroy(gameObject, 1f);
        }
    }

    protected abstract void PickUp(GameObject character);
}
