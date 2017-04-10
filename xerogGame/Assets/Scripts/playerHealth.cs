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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(float amount) {
        //Debug.Log("entered takeDamage");

        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / startingHealth;
        //Debug.Log(healthBar.fillAmount);


    }
}
