using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public GameObject bulletPrefab;

    Transform firePoint;

    public AudioClip fireSound;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<turretBulletBeahviour>().passArgs(firePoint);

        AudioSource.PlayClipAtPoint(fireSound, firePoint.transform.position);

    }
}
