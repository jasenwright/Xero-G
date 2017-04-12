using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletBehaviour : MonoBehaviour {

    //If the player hits a star, the star is destroyed and the score increases
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" || col.tag == "tCollider" || col.tag == "tile") {
            //Destroy bullet
            Destroy(gameObject);
            //Player takes damage when hit
            if (col.tag == "tCollider") {
                turretHealth turret = col.GetComponent<turretHealth>();
                turret.takeDamage(50);
            }
            if (col.tag == "Enemy")
            {
                EnemyHealth enemy = col.GetComponent<EnemyHealth>();
                enemy.takeDamage(20);
            }
        }
    }
}
