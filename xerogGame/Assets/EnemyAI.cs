using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    const int Patrolling = 0;
    const int Attacking = 1;
    const int Following = 2;

    float detectionDistance = 16f;
    float followDistance = 12f;
    float patrolPeriod = 3.0f;
    float gunReloadTime = 2;

    int state = Patrolling;

    EnemyMovement enemy;
    EnemyArmRotation enemyArm;
    EnemyWeapon enemyWeapon;

    float patrolTimer = 0;

    float gunCooldown = 0;

    float rand;

    GameObject character;

    public LayerMask whatToHit;

    float distance;

    void Start()
    {
        enemy = GetComponent<EnemyMovement>();
        enemyArm = GetComponentInChildren<EnemyArmRotation>();
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();
        character = GameObject.Find("Main Character Doesn't Run(Clone)");
        if (character == null) {
            Debug.Log("FREAK OUT");
        }
        if (enemyArm == null || enemyWeapon == null)
        {
            Debug.Log("FREAK OUT");
        }
    }

    void Update()
    {

        var playerDirection = (character.transform.position - transform.position).normalized;

        switch (state)
        {

            case Patrolling:
                // TODO: -------------------------
                // Add patrol behaviour
                if (patrolTimer <= 0)
                {
                    patrolTimer = patrolPeriod;

                    // Change patrolDirection
                    rand = Random.value;
                }

                patrolTimer -= Time.deltaTime;

                // Move enemy
                if (rand < .5f)
                {
                    if (enemy.stuck)
                    {
                        if (enemy.ceiling)
                        {
                            enemy.Flip();
                            rand = 0.51f;
                        }
                        else
                        {
                            enemy.jump();
                        }
                    }
                    else {
                        enemy.moveLeft();
                    }
                }
                else
                {
                    if (enemy.stuck)
                    {
                        if (enemy.ceiling)
                        {
                            enemy.Flip();
                            rand = 0.49f;
                        }
                        else
                        {
                            enemy.jump();
                        }
                    }
                    else
                    {
                        enemy.moveRight();
                    }
                }


                // -------------------------------

                // TODO: -------------------------
                // Under what circumstances should state transitions occur?
                // Add logic to leave patrol state (Check for player within detection distance)
                distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance <= detectionDistance) {
                    state = Attacking;
                }
                // -------------------------------

                break;

            case Attacking:

                // TODO: -------------------------
                // Add attack behaviour
                // Face player
                enemy.stopMoving();
                float dot = Vector3.Dot((character.transform.position - transform.position).normalized, transform.right);
                if (dot > 0) {
                    if (!enemy.isFacingRight()) {
                        enemy.Flip();
                    }
                }
                if (dot <= 0)
                {
                    if (enemy.isFacingRight())
                    {
                        enemy.Flip();
                    }
                }
                enemyArm.rotateArm();
                if (gunCooldown <= 0)
                {
                    gunCooldown = gunReloadTime;

                    // Fire gun at player
                    //Debug.DrawRay(transform.position, character.transform.position - transform.position, Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, character.transform.position - transform.position, Mathf.Infinity, whatToHit);
                    if (hit.collider.gameObject.tag == "tile")
                    {
                        return;
                    }
                    else if (hit.collider.gameObject.tag == "Player")
                    {
                        enemyWeapon.Shoot();
                    }
                }

                gunCooldown -= Time.deltaTime;

                // TODO: -------------------------
                // Add logic to leave attack state (Check if player out of attack range to go back to patrol)
                distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance >= followDistance)
                {
                    state = Following;
                }
                // -------------------------------

                break;

            case Following:
                float dot2 = Vector3.Dot((character.transform.position - transform.position).normalized, transform.right);
                // Character on right
                if (dot2 > 0)
                {
                    // You might not need this
                    /*
                    if (!enemy.isFacingRight())
                    {
                        enemy.Flip();
                    }*/
                    if (enemy.stuck)
                    {
                        if (enemy.ceiling)
                        {
                            enemy.Flip();
                            dot2 = -1f;
                        }
                        else
                        {
                            enemy.jump();
                        }
                    }
                    else
                    {
                        enemy.moveRight();
                    }
                }
                // Character on left
                if (dot2 <= 0)
                {
                    if (enemy.stuck)
                    {
                        if (enemy.ceiling)
                        {
                            enemy.Flip();
                            dot2 = 1f;
                        }
                        else
                        {
                            enemy.jump();
                        }
                    }
                    else
                    {
                        enemy.moveLeft();
                    }
                }
                enemyArm.rotateArm();
                if (gunCooldown <= 0)
                {
                    gunCooldown = gunReloadTime;

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, character.transform.position - transform.position, Mathf.Infinity, whatToHit);
                    if (hit.collider.gameObject.tag == "tile")
                    {
                        return;
                    }
                    else if (hit.collider.gameObject.tag == "Player")
                    {
                        enemyWeapon.Shoot();
                    }
                }

                gunCooldown -= Time.deltaTime;

                distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance >= detectionDistance)
                {
                    state = Patrolling;
                }
                if (distance < followDistance)
                {
                    state = Attacking;
                }
                break;
        }
    }
}