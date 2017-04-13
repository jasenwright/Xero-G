using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPackBehaviour : MonoBehaviour {

    public AudioClip health;

    //When player enters the radius of the turret the turret can fire
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            playerHealth player = col.GetComponent<playerHealth>();

            AudioSource.PlayClipAtPoint(health, player.transform.position);
            player.takeDamage(-50);
            Destroy(gameObject);
        }
    }
}
