using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletHoleDecal;
    [SerializeField] Camera cam;
    [SerializeField] int maxDecals = 10;
    [SerializeField] int maxNumberOfBullets = 5;
    [SerializeField] int initialAmountOfBullets = 0;
    [SerializeField] int damageAmount = 25;
    [SerializeField] MunitionType typeOfGun;
    [SerializeField] Transform aimingPoint;
    [SerializeField] float distance = 5;
    [SerializeField] float timeBetweenShoots = 0.5f;
    [SerializeField] GameObject aimingIndicator;

    private AudioSource audioSource;
    private GameObject[] totalDecals;
    private int currentDecal = 0;
    private int amountOfMunition;
    private bool activeGun;
    private float nextTimeToShoot;

    public static Action<MunitionType, int> OnAmmoChange;

    public MunitionType MunitionType => typeOfGun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalDecals = new GameObject[maxDecals];
        amountOfMunition = initialAmountOfBullets;
        OnAmmoChange?.Invoke(typeOfGun, amountOfMunition);

        Health.OnDeath += BlockGameplay;
    }

    private void BlockGameplay()
    {
        activeGun = false;
    }

    private void Update()
    {
        if (!activeGun)
            return;

        aimingIndicator.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, distance))
        {
            EnemyAIController enemy = hit.collider.GetComponentInParent<EnemyAIController>();
            aimingIndicator.SetActive(true);
            /*if(enemy != null)
            {
                aimingPoint.gameObject.SetActive(true);
                aimingPoint.position = hit.point + hit.normal * 0.01f;
                aimingPoint.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            }
            else
            {
                aimingPoint.gameObject.SetActive(false);
            }*/

            if (amountOfMunition > 0 && Input.GetMouseButtonDown(0) && nextTimeToShoot < Time.time)
            {
                nextTimeToShoot = Time.time + timeBetweenShoots;
                audioSource.Play();
                amountOfMunition--;
                OnAmmoChange?.Invoke(typeOfGun, amountOfMunition);

                if (enemy != null)
                {
                    enemy.GetHit(damageAmount);
                }
                else
                {
                    Destroy(totalDecals[currentDecal]);
                    totalDecals[currentDecal] = Instantiate(bulletHoleDecal, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal), hit.transform);
                    currentDecal = (currentDecal + 1) % maxDecals;
                }
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
