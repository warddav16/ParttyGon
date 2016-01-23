using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	

	Animator anim;

	void Start(){

		anim = GetComponent<Animator>(); 

	}

	// Update is called once per frame
	void SetSpeed(float mySpeed) {
	//idle, walk, run, sprint
		anim.SetFloat ("speed", mySpeed);
	
	}
	void SetDirection(Vector3 myDirection){
	//turning, strafing


	}
	void SetJump(bool isGrounded){
	//on wall, ground, air

	}
	void SetCrouch(bool isCrouching){
	//for small passageways, traps
		
	}
	void SetStunned(float fallDistance){
	//from falling

	}
}
