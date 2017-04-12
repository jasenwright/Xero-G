using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBulletBeahviour : MonoBehaviour {

    public float thrust;
    Vector3 vel;
    public LayerMask whatToHit;

    public void passArgs(Transform firePoint)
    {
        vel = firePoint.transform.right;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (vel * thrust), (vel * thrust).magnitude, whatToHit);
        try
        {
            if (hit.collider.tag == "tile" || hit.collider.tag == "Player")
            {
                //Destroy bullet
                Destroy(gameObject);
                //Enemy takes damage when hit
                if (hit.collider.tag == "Player")
                {
                    playerHealth player = hit.collider.gameObject.GetComponent<playerHealth>();
                    player.takeDamage(10);
                }
            }
        }
        catch { }
        transform.position += vel * thrust;
    }
}
