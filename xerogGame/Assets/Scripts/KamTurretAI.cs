using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamTurretAI : MonoBehaviour {

    public const int Patrolling = 0;
    public const int Attacking = 1;

    public float detectionDistance = 15.0f;
    public float patrolPeriod = 2.5f;

    public int state = Patrolling;

    Vector3 patrolDirection;
    float patrolTimer = 0;

    GameObject character;
    KamTurret kamTurret;
    playerHealth pHealth;

    public LayerMask whatToHit;

    void Start()
    {
        kamTurret = GetComponent<KamTurret>();
        character = GameObject.Find("Main Character Doesn't Run(Clone)");
        pHealth = character.GetComponent<playerHealth>();
    }

    void Update()
    {

        var playerDirection = (character.transform.position - transform.position).normalized;

        switch (state)
        {

            case Patrolling:

                if (kamTurret.stuck == false)
                {
                    if (patrolTimer <= 0)
                    {
                        patrolTimer = patrolPeriod;

                        do
                        {
                            patrolDirection = Random.insideUnitCircle;
                        } while (Vector3.Dot(transform.position, patrolDirection) > 0);


                    }

                    patrolTimer -= Time.deltaTime;

                    kamTurret.TurnToward(patrolDirection);
                    kamTurret.MoveForward();
                }
                else if (kamTurret.stuck == true) {
                    kamTurret.TurnToward(-transform.right);
                    patrolDirection = -transform.right;
                    
                }

                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance <= detectionDistance)
                {
                    kamTurret.moveSpeed = 10.0f;
                    state = Attacking;
                }

                break;

            case Attacking:
                if (pHealth.dead == true) {
                    state = Patrolling;
                } else {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, character.transform.position - transform.position, Mathf.Infinity, whatToHit);
                    if (hit.collider.gameObject.tag == "tile") {
                        kamTurret.moveSpeed = 2.0f;
                        state = Patrolling;
                    }
                    else if (hit.collider.gameObject.tag == "Player") {
                        kamTurret.TurnToward(playerDirection);
                        kamTurret.MoveForward();
                    }
                }
                break;
        }
    }
}
