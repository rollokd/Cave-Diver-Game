using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool rocket;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject rocketPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
        if(rocket && Input.GetButtonDown("Fire2")){
            ShootRocket();
        }
    }

    void Shoot()
    {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    
    void ShootRocket()
    {
        //shooting logic
        Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
    }
}
