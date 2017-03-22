using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{

    GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        //TO DO
        //Get the character object so the camera follows the character
        offset = transform.position - player.transform.position;


    }

    // Update is called once per frame
    void LateUpdate()
    {

        try
        {
            transform.position = player.transform.position + offset;
        }
        catch
        {
            //Debug.Log ("cameraFollowPlayer script had an error");
        }

    }
}
