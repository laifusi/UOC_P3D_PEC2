using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletHoleDecal;
    [SerializeField] Camera cam;
    [SerializeField] int maxBulletHoles = 10;
    [SerializeField] int maxNumberOfBullets = 5;
    [SerializeField] MunitionType typeOfGun;

    private AudioSource audioSource;
    private GameObject[] totalDecals;
    private int currentDecal = 0;
    private int amountOfMunition;
    private bool activeGun;

    public static Action<MunitionType, int> OnAmmoChange;

    public MunitionType MunitionType => typeOfGun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalDecals = new GameObject[maxBulletHoles];
        amountOfMunition = maxNumberOfBullets;
    }

    private void Update()
    {
        if (!activeGun)
            return;

        if(amountOfMunition > 0 && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                Destroy(totalDecals[currentDecal]);
                totalDecals[currentDecal] = Instantiate(bulletHoleDecal, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal), hit.transform);
                currentDecal = (currentDecal + 1) % maxBulletHoles;
                audioSource.Play();
                amountOfMunition--;
                OnAmmoChange?.Invoke(typeOfGun, amountOfMunition);

                //ADD HITTING ENEMY
            }
        }
    }

    public void AddAmmo(MunitionType typeOfAmmo, int amount)
    {
        if (typeOfAmmo != typeOfGun)
            return;

        amountOfMunition += amount;
        if (amountOfMunition > maxNumberOfBullets)
            amountOfMunition = maxNumberOfBullets;
        OnAmmoChange?.Invoke(typeOfGun, amountOfMunition);
    }

    public void ActivateGun(bool isActive)
    {
        activeGun = isActive;
    }
}
