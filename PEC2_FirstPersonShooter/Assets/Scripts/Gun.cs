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
    [SerializeField] int damageAmount = 25;
    [SerializeField] MunitionType typeOfGun;
    [SerializeField] Transform aimingPoint;

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
        totalDecals = new GameObject[maxDecals];
        amountOfMunition = maxNumberOfBullets;

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

        RaycastHit hit;
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            EnemyAIController enemy = hit.collider.GetComponentInParent<EnemyAIController>();
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

            if (amountOfMunition > 0 && Input.GetMouseButtonDown(0))
            {
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
