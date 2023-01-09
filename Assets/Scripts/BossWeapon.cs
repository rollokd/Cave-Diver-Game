using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public bool rocket;

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject rocketPrefab;
    [SerializeField]
    private float burstDelay;
    [SerializeField]
    private float subBurstDelay;
    [SerializeField]
    private int burstCount;

    private float burstTimer;
    private float subBurstTimer;
    private int shots;

    public void SetRocket(bool boolean)
    {
        if (boolean)
        {
            bulletPrefab = rocketPrefab;
        }
        rocket = boolean;
    }

    void Update()
    { 
        burstTimer += Time.deltaTime;

        if (burstTimer > burstDelay)
        {
            subBurstTimer += Time.deltaTime;

            if (subBurstTimer > subBurstDelay)
            {
                Shoot();
                shots++;
                subBurstTimer = 0;
            }

            if (shots >= burstCount)
            {
                burstTimer = 0;
                shots = 0;
            }
        }
    }

    void Shoot()
    {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
