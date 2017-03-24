using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveXero : MonoBehaviour {

    public GameObject xero;
    bool changeColor = false;
    public GameObject startPosition;
    public GameObject endPosition;
    public float speed = 5.0F;
    private float startTime;
    private float journeyLength;
    public AudioClip whooshSound;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition.transform.position, endPosition.transform.position);

    }
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        xero.transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, fracJourney);

        SpriteRenderer renderer = xero.GetComponent<SpriteRenderer>();
        Color tempColor = renderer.color;
        

        if (xero.transform.position == endPosition.transform.position && changeColor==false)
        {
            //renderer.material.SetColor("_Color", Color.white);
            //renderer.color = new Color(0f, 0f, 0f, 1f); // Set to opaque black
            changeColor = true;
        }

        //renderer.color = tempColor;
        
    }


}
