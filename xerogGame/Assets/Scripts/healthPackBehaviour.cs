using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPackBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //When player enters the radius of the turret the turret can fire
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Entered collision");
        if (col.gameObject.tag == "Player") {
            playerHealth player = col.GetComponent<playerHealth>();
            player.takeDamage(-20);
        }
    }
}
