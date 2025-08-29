using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    [Header("Referências")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Shooting Settings")]
    public float bulletForce = 20f;
    public float fireRate = 0.5f;
    public float lifeTime = 2f;
    public int bulletDamage = 10;

    private float nextFireTime = 0f;

    private enum FireMode { Single, TripleShot }
    private FireMode currentFireMode = FireMode.TripleShot;

    public void Fire()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        switch (currentFireMode)
        {
            case FireMode.Single:
                SpawnBullet(firePoint.position, firePoint.rotation);
                break;

            case FireMode.TripleShot:
                SpawnBullet(firePoint.position, firePoint.rotation);

                SpawnBullet(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 30));

                SpawnBullet(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 15));
                break;
        }
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        // pega o script Bullet
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.damage = bulletDamage;
            b.lifeTime = lifeTime;
            b.owner = gameObject;
        }

        // movimento da bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = bullet.transform.right * bulletForce;
        }

    }

}
