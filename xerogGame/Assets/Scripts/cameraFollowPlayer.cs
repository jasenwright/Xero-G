﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour {

    public GameObject player;

    // Update is called once per frame
    void LateUpdate() {

        //Try and Find Player
        try {
            player = GameObject.Find("Main Character Doesn't Run(Clone)");
        }
        catch {
            Debug.Log("Error: Couldn't find player");
        }

        //Move the camera to see the character
        try {
            transform.position = player.transform.position + new Vector3(0, 0, -10);
        }
        
        //Catch for when character is removed
        catch {
            //Debug.Log ("cameraFollowPlayer script had an error");
        }

    }
}
