using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float fireRate = 5;
    public float Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot() {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        CharacterControllerScripts ccs = GetComponent<CharacterControllerScripts>();
        Vector2 fireToPosition;
        if (ccs.isFacingRight())
        {
            fireToPosition = new Vector2(firePoint.position.x + 1, firePoint.position.y);
        }
        else
        {
            fireToPosition = new Vector2(firePoint.position.x - 1, firePoint.position.y);
        }
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireToPosition, whatToHit);
        Debug.DrawLine(firePointPosition, fireToPosition, Color.cyan);
    }
}
