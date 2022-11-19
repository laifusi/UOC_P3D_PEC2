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
