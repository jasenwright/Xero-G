using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public GameObject bulletPrefab;

    Transform firePoint;

    public AudioClip fireSound;

    public float bulletForce;
    GameObject player;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        player = GameObject.Find("Main Character Doesn't Run(Clone)");
        

    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        //Get bullet's rigid body
        Rigidbody2D bulletController = bullet.GetComponent<Rigidbody2D>();

        //Add forward momentum to bullet's rigid body so it shoots
        bulletController.AddForce(firePoint.transform.right * bulletForce);

        AudioSource.PlayClipAtPoint(fireSound, firePoint.transform.position);

    }
}
