using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBulletBeahviour : MonoBehaviour {

    //If the player hits a star, the star is destroyed and the score increases
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" || col.tag=="tile") {
   
            //Destroy bullet
            Destroy(gameObject);
        //Player takes damage when hit
        if (col.tag == "Player") {
            playerHealth player = col.GetComponent<playerHealth>();
            player.takeDamage(10);
        }
        }
    }
}
