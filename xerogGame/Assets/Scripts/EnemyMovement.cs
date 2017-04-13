using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float maxSpeed = 10f;
    bool facingRight = true;
    float move;

    Animator anim;

    public bool grounded = false;
    public bool stuck = false;
    public bool ceiling = false;
    public Transform groundCheck;
    public Transform ceilingCheck;
    public Transform forwardCheck;
    float groundRadius = 0.01f;
    float forwardRadius = 0.01f;
    public LayerMask whatIsGround;
    public float jumpForce = 15;

    public float maxJump = 30;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void moveLeft() {
        move = -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void moveRight()
    {
        move = 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void stopMoving() {
        move = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void jump() {
        try {
            anim.SetBool("Ground", false);
        }
        catch { }
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxJump);
        GetComponentInChildren<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Code that has to do with the animator
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        stuck = Physics2D.OverlapCircle(forwardCheck.position, forwardRadius, whatIsGround);
        ceiling = Physics2D.OverlapCircle(ceilingCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        EnemyArmRotation[] arm = GetComponentsInChildren<EnemyArmRotation>();
        Flipper[] fp = GetComponentsInChildren<Flipper>();
        if (arm[0].rotationOffset == 0)
        {
            arm[0].rotationOffset = 180;
            fp[0].flipFirePoint();
        }
        else
        {
            arm[0].rotationOffset = 0;
            fp[0].flipFirePoint();
        }
    }

    public bool isFacingRight()
    {
        return facingRight;
    }
}
