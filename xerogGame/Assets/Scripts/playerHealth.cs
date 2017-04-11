using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class playerHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject newGame;
    public GameObject player;
    public GameObject grey;
    public AudioClip loseSound;
    public bool dead;

    // Use this for initialization
    void Start () {

        dead = false;
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
        if (currentHealth <= 0) {
            currentHealth = 0;
            //If health is 0 enter lose case
            loseCase();
        }
        //Adjust the health bar
        healthBar.fillAmount = currentHealth / startingHealth;
       
    }

    void loseCase() {
       
        dead = true;

        //Makes character disappear
        player.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(player.GetComponent<Rigidbody2D>());

        GameObject jetpack = player.transform.Find("Jetpack").gameObject;
        try {
            Destroy(jetpack);
        }
        catch { }
        GameObject arm = player.transform.Find("Arm").gameObject;
        try {
            Destroy(arm);
        }
        catch { }
        GameObject groundcheck = player.transform.Find("GroundCheck").gameObject;
        try {
            Destroy(groundcheck);
        }
        catch { }
        GameObject sphere = player.transform.Find("PlayerSphere").gameObject;
        try {
            Destroy(sphere);
        }
        catch { }

        //Put the grey background     
        grey.SetActive(true);
        newGame.SetActive(true);

        //Play losing sound, play clip at point where user can hear it loudly but not too loud
        Vector3 newPosition = new Vector3(player.transform.position.x,player.transform.position.y,-10); 
        AudioSource.PlayClipAtPoint(loseSound, newPosition);

    }
}
