using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretHealth : MonoBehaviour {

    public float startingHealth = 100;
    public float currentHealth;
    public GameObject explosion;
    public AudioClip explosionSound;
    public enemyCounter eCounter;
    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        eCounter = GameObject.Find("enemyCounter").GetComponent<enemyCounter>();

    }

    public void takeDamage(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Destroy(transform.parent.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            eCounter.decreaseEnemies();
            
           
        }
       
    }
}
