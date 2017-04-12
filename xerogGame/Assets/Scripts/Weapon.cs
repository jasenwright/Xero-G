using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 5;
    public float damage = 10;
    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;

    public GameObject bulletPrefab;

    float timeToFire = 0;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    Transform firePoint;

    public AudioClip fireSound;

    public float bulletForce;

    // Use this for initialization
    void Awake () {
        firePoint = transform.FindChild("FirePoint");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time > timeToFire) {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
	}

    void Shoot() {
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        //Get bullet's rigid body
        Rigidbody2D bulletController = bullet.GetComponent<Rigidbody2D>();

        //Add forward momentum to bullet's rigid body so it shoots
        bulletController.AddForce(firePoint.transform.right * bulletForce);

        AudioSource.PlayClipAtPoint(fireSound, firePoint.transform.position);
    }
}
