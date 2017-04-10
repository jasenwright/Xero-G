using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;

    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(float amount) {
        Debug.Log("entered takeDamage");
        currentHealth -= amount;
    }
}
