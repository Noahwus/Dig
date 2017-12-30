using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float topSpeed = 10f;

	bool facingRight = true;

	//get reference to animator
	Animator anim;

	//not grounded
	bool grounded = false;

	//empty game object at feet for transform
	public Transform groundCheck;

	//groundcheck radius of checking
	float groundRadius = 0.2f;

	//force of the jump
	public float jumpForce = 700f;

	//what layer is considered the ground
	public LayerMask whatIsGround;



	void Start(){
		anim = GetComponent<Animator> ();

	}

	void FixedUpdate(){
		//true or false did the ground transform hit anything in the layer ground with the circle radius we set
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		//tell the animator that we are on the ground
		anim.SetBool ("Ground", grounded);

		//how fast we are moving upwards or downwards
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		//get move direction
		float move = Input.GetAxis("Horizontal");

		//add velocity to the rigidbody in the move direction * our speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(move* topSpeed, GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat ("Speed", Mathf.Abs(move));

	

		//if we're facing the negative direction and not facing the right, flip
		if (move > 0 && !facingRight) {
			Flip ();

		} else if (move < 0 && facingRight) {
			Flip ();	
		}
			
	}

	void Update(){
		if (grounded && Input.GetKeyDown(KeyCode.W)){
			//not on the ground
			anim.SetBool ("Ground", false);

			//add jump force on the y in an impulse fashion
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, jumpForce), ForceMode2D.Impulse);

		
		}
	}

	void Flip(){
		//saying we are facing the opposite direction
		facingRight = !facingRight;

		//get the local scale
		Vector3 theScale = transform.localScale;

		//flip on the x axis
		theScale.x *= -1;

		//apply that to the localscale
		transform.localScale = theScale;


	}
}
