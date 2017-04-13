using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletBehaviour : MonoBehaviour {

    public float thrust;
    Vector3 vel;
    public LayerMask whatToHit;

    public void passArgs(Transform firePoint) {
        vel = firePoint.transform.right;
    }
    
    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (vel * thrust), (vel * thrust).magnitude, whatToHit);
        try
        {
            if (hit.collider.tag == "tile" || hit.collider.tag == "turret" || hit.collider.tag == "Enemy" || hit.collider.tag == "EnemyHead" || hit.collider.tag == "KamTurret")
            {
                //Destroy bullet
                Destroy(gameObject);
                //Enemy takes damage when hit
                if (hit.collider.tag == "turret")
                {
                    turretHealth turret = hit.collider.gameObject.GetComponent<turretHealth>();
                    turret.takeDamage(50);
                }
                if (hit.collider.tag == "Enemy")
                {
                    
                    EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                    enemy.takeDamage(20);
                }
                if (hit.collider.tag == "EnemyHead")
                {

                    EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
                    enemy.takeDamage(50);
                }
                if (hit.collider.tag == "KamTurret")
                {

                    turretHealth enemy = hit.collider.GetComponent<turretHealth>();
                    enemy.takeDamage(100);
                }
            }
        }
        catch { }
        transform.position += vel * thrust;
    }
}
