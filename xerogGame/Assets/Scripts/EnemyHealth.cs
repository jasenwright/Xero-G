using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    float startingHealth = 100;
    float currentHealth;
    public GameObject explosion;
    public AudioClip explosionSound;
    public enemyCounter eCounter;
    public Image healthBar;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        eCounter = GameObject.Find("enemyCounter").GetComponent<enemyCounter>();
        healthBar.fillAmount = currentHealth / startingHealth;

    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;

        //Adjust the health bar
        healthBar.fillAmount = currentHealth / startingHealth;

        if (currentHealth <= 0)
        {
            Destroy(transform.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            eCounter.decreaseEnemies();
        }

    }
}
