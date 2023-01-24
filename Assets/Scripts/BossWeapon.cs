using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [HideInInspector]
    public bool rocket;

    [SerializeField]
    private Transform firePoint;

    [Header("Bullets")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject rocketPrefab;

    [Header("Burst")]
    [SerializeField]
    private float burstDelay;
    [SerializeField]
    private float subBurstDelay;
    [SerializeField]
    private int burstCount;

    private float burstTimer;
    private float subBurstTimer;
    private int shots;

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

    public void SetRocket(bool boolean)
    {
        if (boolean)
            bulletPrefab = rocketPrefab;
        
        rocket = boolean;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
