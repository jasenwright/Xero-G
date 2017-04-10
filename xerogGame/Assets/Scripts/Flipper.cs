using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

    GameObject fp;

	// Use this for initialization
	void Start () {
        fp = GameObject.Find("FirePoint");
        if (fp == null) {
            Debug.Log("Can't find fp");
        }
	}

    public void flipFirePoint() {
        transform.RotateAround(transform.position, transform.up, 180f);
    }


}
