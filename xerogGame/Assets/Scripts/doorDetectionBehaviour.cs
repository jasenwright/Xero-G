using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorDetectionBehaviour : MonoBehaviour {

    public GameObject eCounter;
    public enemyCounter numberOfEnemies; 
    Animator anim;

    // Use this for initialization
    void Start () {
        //Get Animator
        anim = GetComponent<Animator>();

        //Get Enemy counter and it's script
        eCounter = GameObject.Find("enemyCounter");
        numberOfEnemies = eCounter.GetComponent<enemyCounter>();
    }
	
    void OnTriggerEnter2D(Collider2D col) {
        //Try needed as .numberOfEnemies might have been instantiated yet
        try {
            if (col.tag == "Player" && numberOfEnemies.numberOfEnemies == 0) {
                anim.SetBool("openDoor", true);
            }
        }
        catch { }

    }
}
