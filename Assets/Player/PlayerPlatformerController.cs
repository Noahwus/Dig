using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

	public float jumpTakeOffSpeed = 14;
	public float accel = 2;
	public float maxSpeed = 10;
	public float fallSpeed = .8f;

	//private float deltaY = 0f;

	Vector2 move = Vector2.zero;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		//gravMod = gravModifier;
	}
	
	protected override void ComputeVelocity(){

		move.x = Input.GetAxis ("Horizontal");

		if (!grounded && velocity.y <= 0) {
			velocity.y = velocity.y * fallSpeed;
		} else if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} else if (Input.GetButtonUp ("Jump")) {
			if (velocity.y > 0) {
				velocity.y = velocity.y * -fallSpeed;
			}
		} 

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if(flipSprite){
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		//animator.SetBool ("grounded", grounded);
		//animator.SetFloat ("VelocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;


	}
}
