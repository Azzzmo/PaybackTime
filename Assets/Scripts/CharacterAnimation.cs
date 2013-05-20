//

using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	
	public float vAxis; 
	public float hAxis;
	private CharacterController controller;
	private Animation theanimation;
	private float previous_control_x = 0f;
	private float previous_control_z = 0f;
	
	public float idleAnimationSpeed = 1f;
	public float walkAnimationSpeed = 1f;
	public float runAnimationSpeed = 1f;
	public float sidestepAnimationSpeed = 1f;
	
	private Transform cam;
	public bool isControlled;

	
	enum animState
	{
		Idle = 0, Walking = 1, Sidestepping = 2, Running = 3, Jumping = 4, Reversing = 5,
	}
	
	animState mystate = animState.Idle;
	
	// Use this for initialization
	void Start () {
		
		mystate = animState.Idle;
		isControlled = false;
		
		controller = GetComponentInChildren<CharacterController>(); //assume there's a character controller
		theanimation = GetComponentInChildren<Animation>(); //assume there's an animation component
		
		//if gameobject has a "Character Camera" enabled, the object is being controlled by keyboard
		cam = transform.FindChild("Character Camera");
			
		theanimation.Play("idle");
		/*
		if(controller != null)
			//Debug.Log ("got controller");
		if(theanimation != null)
			//Debug.Log ("got animation");
		if(cam != null)
			//Debug.Log ("got Cam");*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(cam != null && cam.gameObject.activeSelf)
			isControlled = true;
		else 
			isControlled = false;
		
		//if object is being controlled by input, set axis values accordingly
		if(isControlled == true)
		{
			//get input axis values
			vAxis = Input.GetAxisRaw("Vertical");
			hAxis = Input.GetAxisRaw("Horizontal");
			//Debug.Log ("is controlled");
		}else{
			vAxis = 0f;
			hAxis = 0f;
			//Debug.Log ("not controlled");
		}
		
		//set animation state based on input axis values
		if(vAxis > 0)
			mystate = animState.Walking;
		else if (vAxis < 0)
			mystate = animState.Reversing;
		else if(hAxis < 0 || hAxis > 0)
			mystate = animState.Sidestepping;
		else if(vAxis == 0 || hAxis == 0)
			mystate = animState.Idle;
		
		//if controller has moved due to outside control, set animation state accordingly
 		if((controller.transform.position.x != previous_control_x || controller.transform.position.z != previous_control_z) && isControlled == false )
		{
			mystate = animState.Walking;
			//Debug.Log("control object moving");
		}
		
		//Debug.Log (mystate.ToString());
		
		switch (mystate) {
		case animState.Idle: theanimation.Play("idle");
			theanimation["idle"].speed = idleAnimationSpeed;
			break;
		case animState.Walking: theanimation.CrossFade ("walk");
			theanimation["walk"].speed = walkAnimationSpeed;
			break;
		case animState.Reversing: theanimation.CrossFade("walk");
			theanimation["walk"].speed = -1 * walkAnimationSpeed;
			break; 
		case animState.Running: theanimation.CrossFade ("run");
			theanimation["run"].speed = runAnimationSpeed;
			break;
		case animState.Sidestepping: theanimation.CrossFade ("walk");
			theanimation["walk"].speed = sidestepAnimationSpeed;
			break;
		default: theanimation.Play ("idle");
			theanimation["idle"].speed = idleAnimationSpeed;
			break;
		}

		
		//record current position for next round to check if it has changed between rounds
		previous_control_x = controller.transform.position.x;
		previous_control_z = controller.transform.position.z;
		
	}
	

}
