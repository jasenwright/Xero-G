using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class playerHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject player;

    // Use this for initialization
    void Start () {

        currentHealth = startingHealth;
        healthBar.fillAmount = currentHealth / startingHealth;
        
	}
	

    public void takeDamage(float amount) {
        //Debug.Log("entered takeDamage");

        currentHealth -= amount;
        //Make it so health can't be greater than 100 or less than 0
        if (currentHealth > 100) {
            currentHealth = 100;
        }
        if (currentHealth < 0) {
            currentHealth = 0;
        }
        healthBar.fillAmount = currentHealth / startingHealth;
       
    }
}
