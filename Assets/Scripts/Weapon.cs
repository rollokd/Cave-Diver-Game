using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool rocket;

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject rocketPrefab;

    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
        if (gameController != null && gameController.paused)
            return;

        if (Input.GetButtonDown("Fire1"))
            Shoot(bulletPrefab);
        
        if(rocket && Input.GetButtonDown("Fire2"))
            Shoot(rocketPrefab);
    }

    private void Shoot(GameObject bullet)
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
