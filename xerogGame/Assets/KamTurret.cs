using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamTurret : MonoBehaviour {
    public float moveSpeed = 2;
    public float turnSpeed = 200;

    public GameObject explosion;
    public AudioClip explosionSound;
    public enemyCounter eCounter;

    public bool stuck = false;

    void Start()
    {
        eCounter = GameObject.Find("enemyCounter").GetComponent<enemyCounter>();
    }

    public void MoveForward()
    {
        transform.position += transform.up * Time.deltaTime * moveSpeed;
    }

    public void MoveBackward()
    {
        transform.position -= transform.up * Time.deltaTime * moveSpeed;
    }

    public void TurnToward(Vector3 direction)
    {

        var axis = Vector3.Cross(direction, transform.up);

        if (axis.magnitude < 0.03)
        {
            axis = Vector3.zero;
        }
        var sign = -Vector3.Dot(Vector3.forward, axis.normalized);

        transform.RotateAround(transform.position, Vector3.forward, sign * Time.deltaTime * turnSpeed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "tile")
        {
            stuck = true;
        }
        if (other.tag == "Player") {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.x * 5, transform.up.y * 5);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            eCounter.decreaseEnemies();
            playerHealth player = other.gameObject.GetComponent<playerHealth>();
            player.takeDamage(50);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "tile")
        {
            stuck = false;
        }
    }
}
