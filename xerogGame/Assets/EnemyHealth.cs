using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    float startingHealth = 100;
    float currentHealth;
    public GameObject explosion;
    public AudioClip explosionSound;
    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;

    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(transform.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

    }
}
