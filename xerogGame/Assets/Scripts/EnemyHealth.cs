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
    EnemyAI enemyai;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        eCounter = GameObject.Find("enemyCounter").GetComponent<enemyCounter>();
        healthBar.fillAmount = currentHealth / startingHealth;
        enemyai = GetComponent<EnemyAI>();
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;

        enemyai.state = 1;

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
