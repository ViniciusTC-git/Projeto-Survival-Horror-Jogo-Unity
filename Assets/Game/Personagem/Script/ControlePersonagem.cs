using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePersonagem : MonoBehaviour {
	private string maoDireita = "Armature/COG/Body_0/backbone1/backbone2/chest/shoulder_R/arm_R/forearm1_R/forearm2_R/palm_R/mid1_R/Item_R";
	public bool lockControl = false;
	public bool lockRun = false;
	public float walkSpeed = 5;
	public float runSpeed = 15;
	public float gravity = -12;

	public float turnSmoothTime = 0.5f;
	float turnSmoothVelocity;


	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	Animator animator;
	CharacterController controller;

	void Start () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<CharacterController> ();
	}
	void Update () {
		control ();
		attackState ();
	}
	void attackState(){

		int weaponLength = transform.FindChild (maoDireita).childCount;
		if (weaponLength > 0) {
			bool isEquiped = transform.FindChild (maoDireita).GetChild (0).gameObject.activeInHierarchy;
			string typeWeapon = transform.FindChild (maoDireita).GetChild (0).name;
			if (isEquiped && Input.GetKey (KeyCode.Mouse1)) {
				if (typeWeapon == "taco de baseball") {
					animator.SetBool ("attackState", true);
					if (Input.GetKey (KeyCode.Mouse0) && !animAttackState() && controller.velocity.magnitude == 0) {
						animator.CrossFade ("Attack", 0.15f);
					}
					if (Input.GetKey (KeyCode.LeftShift) && !animAttackState() && controller.velocity.magnitude == 0) {
						animator.CrossFade ("Defense", 0.05f);
					}
				}
			} else {
				animator.SetBool ("attackState", false);
			}
		}
	}
    bool animAttackState(){
		
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack") || animator.GetCurrentAnimatorStateInfo (0).IsName ("Defense")
		   || animator.GetCurrentAnimatorStateInfo (0).IsName ("AttackWalkF") || animator.GetCurrentAnimatorStateInfo (0).IsName ("AttackWalkB")) {
			return true;
		}
		
		return false;
	}
	void control(){

		if (!lockControl) {
			Vector3 input = new Vector3 (0, 0, Input.GetAxisRaw ("Vertical"));
			Vector3 inputDir = input.normalized;
			bool running = (!lockRun ? Input.GetKey (KeyCode.LeftShift):false);
			bool back = Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

			/*inputDir = transform.TransformDirection (inputDir);
		    controller.Move (inputDir * walkSpeed * Time.deltaTime);
		    animator.SetFloat ("speedPercent", -1, speedSmoothTime, Time.deltaTime);*/

			float targetSpeed = ((running && !back) ? runSpeed : walkSpeed);
			inputDir = transform.TransformDirection (inputDir);
			currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
			velocityY += gravity * Time.deltaTime;
			controller.Move (inputDir * currentSpeed * Time.deltaTime);
			currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;
			if (controller.isGrounded) {
				velocityY = 0;
			}
			float animationSpeedPercent = ((running && !back) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
			animationSpeedPercent = ((back) ? currentSpeed / runSpeed * -1.0f : animationSpeedPercent);
			animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
			transform.Rotate (0, Input.GetAxis ("Horizontal") * 50 * Time.deltaTime, 0);
		}
	}



}