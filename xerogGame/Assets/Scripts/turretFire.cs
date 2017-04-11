using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class turretFire : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletEmitterWest;
    public GameObject bulletEmitterNorthWest;
    public GameObject bulletEmitterNorth;
    public GameObject bulletEmitterNorthEast;
    public GameObject bulletEmitterEast;
    public float bulletForce;
    public AudioClip fireSound;
    public LayerMask whatToHit;
    //public GameObject spark;
    //public Camera camera;
    
    private GameObject[] emitterList = new GameObject[5];
    private float[] distanceArray = new float[5];
    private bool canFire;
    private float lastFireTime = 0;

    // Use this for initialization
    void Start () {
        
        emitterList[0] = bulletEmitterWest;
        emitterList[1] = bulletEmitterNorthWest;
        emitterList[2] = bulletEmitterNorth;
        emitterList[3] = bulletEmitterNorthEast;
        emitterList[4] = bulletEmitterEast;
        canFire = false;
	}
	
	// Update is called once per frame
	void Update () {

        try {
            GameObject character;
            character = GameObject.Find("Main Character Doesn't Run(Clone)");
            if (canFire) {
                //Debug.DrawRay(transform.position, character.transform.position-transform.position, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, character.transform.position - transform.position, whatToHit);
                if (hit.collider.gameObject.tag == "tile") {
                    return;
                }
                else if (hit.collider.gameObject.tag == "Player") {
                    //Create List of distances
                    distanceArray[0] = Vector3.Distance(bulletEmitterWest.transform.position, character.transform.position);
                    distanceArray[1] = Vector3.Distance(bulletEmitterNorthWest.transform.position, character.transform.position);
                    distanceArray[2] = Vector3.Distance(bulletEmitterNorth.transform.position, character.transform.position);
                    distanceArray[3] = Vector3.Distance(bulletEmitterNorthEast.transform.position, character.transform.position);
                    distanceArray[4] = Vector3.Distance(bulletEmitterEast.transform.position, character.transform.position);

                    //Then whatever turret is closest to the character fire from that turret
                    int index = 0;
                    float min = distanceArray[0];
                    for (int i = 0; i < distanceArray.Length; i++) {
                        if (distanceArray[i] < min) {
                            min = distanceArray[i];
                            index = i;
                        }
                    }

                    fire(emitterList[index]);
                }
            }
        }
        catch {
            //Nothing Happens
        }
      }

    void fire(GameObject bulletEmitter){

        if (Time.time - lastFireTime > 1) {

            GameObject player = GameObject.Find("Main Character Doesn't Run(Clone)");

            playerHealth health = player.GetComponent<playerHealth>();

            if (health.dead == false) {

                lastFireTime = Time.time;

                //Get the bulletEmitter to rotate towards the player
                Vector3 dir = player.transform.position - bulletEmitter.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bulletEmitter.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                //This code was based off of https://www.youtube.com/watch?v=FD9HZB0Jn1w
                //Get bullet
                GameObject bullet = Instantiate(bulletPrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation);

                //Get bullet's rigid body
                Rigidbody2D bulletController = bullet.GetComponent<Rigidbody2D>();

                //Add forward momentum to bullet's rigid body so it shoots
                bulletController.AddForce(bulletEmitter.transform.right * bulletForce);

                AudioSource.PlayClipAtPoint(fireSound, bulletEmitter.transform.position);

                //Destroy bullet after 2 seconds
                Destroy(bullet, 2);
            }
        }
    }


    //When player enters the radius of the turret the turret can fire
    void OnTriggerEnter2D(Collider2D col){
        //Debug.Log("Entered collision");
        if (col.gameObject.tag == "Player")
        {
            canFire = true;
        }
    }

    //When player leaves the radius of the turret it stops firing
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            canFire = false;
        }
    }

}
