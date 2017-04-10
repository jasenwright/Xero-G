using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBeahviour : MonoBehaviour {

    //If the player hits a star, the star is destroyed and the score increases
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" || col.tag=="tile") {
            Destroy(gameObject);
        }
    }
}
