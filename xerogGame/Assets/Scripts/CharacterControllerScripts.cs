/*
 / Character controller. Animates the player as they move and jump.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScripts : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.01f;
	public LayerMask whatIsGround;
	public float jumpForce = 15;

    public float maxJump = 30;

    public new Camera camera;


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        try {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

            float move = Input.GetAxis("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(move));

            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
        }
        catch { }
	}

	void Update () {
		if (Input.GetKey(KeyCode.W)) {

            try {
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxJump);
                //GameObject jetpack = transform.Find("Jetpack").gameObject;
                GetComponentInChildren<ParticleSystem>().Play();
            }
            catch { }
        }
    }

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
        ArmRotation[] arm = GetComponentsInChildren<ArmRotation>();
        Flipper[] fp = GetComponentsInChildren<Flipper>();
        if (arm[0].rotationOffset == 0)
        {
            arm[0].rotationOffset = 180;
            fp[0].flipFirePoint();
        }
        else {
            arm[0].rotationOffset = 0;
            fp[0].flipFirePoint();
        }
	}

    public bool isFacingRight() {
        return facingRight;
    }
}
