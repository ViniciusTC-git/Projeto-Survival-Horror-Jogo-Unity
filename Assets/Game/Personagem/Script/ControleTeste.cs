using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour {
	public Animator anim;
	//public Rigidbody rbody;
	private float inputH;
	private float inputV;
	public float walkSpeed = 5;
	public float runSpeed = 10;
	public float gravity = 0;
	float currentSpeed;
	float velocityY;
	CharacterController controller;
	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	//	rbody = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Animacoes ();
		Movimento ();
	
	}
	void Movimento(){
		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");
		anim.SetFloat ("inputH", inputH);
		anim.SetFloat ("inputV", inputV);
		Vector3 inputDir = new Vector3 (Input.GetAxis ("Horizontal"),0, Input.GetAxis ("Vertical"));
		float moveX = inputH * 5 * Time.deltaTime;
		float moveZ = inputV * 5 * Time.deltaTime;
		inputDir = transform.TransformDirection (inputDir);
		/*if (moveZ <= 0) {
			moveX = 0;
		}*/
		transform.Rotate (0, Input.GetAxis ("Horizontal") * 50 * Time.deltaTime, 0);
		controller.Move(new Vector3(moveX, 0f, moveZ));

		/*bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY; 

		rbody.velocity = new Vector3(moveX, 0f, moveZ);*/

	}
	void Animacoes(){
		if (Input.GetKeyDown ("1")) {
			anim.Play ("WAIT02", -1,0f);
		}
		if (Input.GetKeyDown ("2")) {
			anim.Play ("WAIT03", -1,0f);
		}
	}
}
