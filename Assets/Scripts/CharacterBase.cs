using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//base class for defining character properties, character state (what is it doing), what actions it can perform. 

public class CharacterBase : MonoBehaviour {
	
	//character type (enum)
	
	public CharacterType myType;
	
	//basic attributes
	public int strength;
	public int speed;
	public int intelligence;
	public int health;
	
	//character states todo: enum - use in character animation script
	public AnimState mystate;
	
	//character abilities
	public bool canDrive;
	public bool canBreak;
	public bool canAttack;
	

	//if object has camera component, it can be controlled
	private Transform cam;
	
	//is character being controlled in first person view
	public bool isControlled;

	
	//character specific target
	private Transform mySpecificTarget;
	public Transform MyTarget
	{	
		get { return mySpecificTarget; }
		set { mySpecificTarget = value; }
	}
	//list of targets in range
	List<Transform> myTargetsList = new List<Transform>();
	//current target
	Transform currentTarget;
	
	
	//animation values	
	private Animation theanimation;
	
	public float idleAnimationSpeed = 1f;
	public float walkAnimationSpeed = 1f;
	public float runAnimationSpeed = 1f;
	public float sidestepAnimationSpeed = 1f;
	public float attackAnimationSpeed = 1f;
	public float deathAnimationSpeed = 1f;
	public float changeAnimationSpeed = 1f;
	public float gethitAnimationSpeed = 1f;
	
	//user input
	public float vAxis; 
	public float hAxis;
	
	//last tranform position
	private float previous_control_x = 0f;
	private float previous_control_z = 0f;
	private CharacterController controller;


	// Use this for initialization
	void Start () {
		
		//set default attributes
		strength = 1;
		speed = 1; 
		intelligence = 1;
		
		//set default abilities
		canDrive = false;
		canBreak = false;
		canAttack = false;
		
		//set default animation state at start
		Invoke("SetIdle", Random.value * 10);
		
		controller = GetComponentInChildren<CharacterController>(); //assume there's a character controller, and find it
		theanimation = GetComponentInChildren<Animation>(); //assume there's an animation component, and find it
		
		//if gameobject has a "Character Camera" enabled, the object is being controlled by keyboard
		cam = transform.FindChild("Character Camera");
		
		//set current tranform location to trackers. If object is moved in RTS mode, the value changes, and default walk animation is played
		previous_control_x = controller.transform.position.x;
		previous_control_z = controller.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		//reset currentTarget enemy
		currentTarget = null;
		
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
				mystate = AnimState.Walking;
			else if (vAxis < 0)
				mystate = AnimState.Reversing;
			else if(hAxis < 0 || hAxis > 0)
				mystate = AnimState.Sidestepping;
			else if(vAxis == 0 || hAxis == 0)
				mystate = AnimState.Idle;
		
		//if controller has moved due to outside RTS control, set animation state accordingly
 		if((controller.transform.position.x != previous_control_x || controller.transform.position.z != previous_control_z) && isControlled == false )
		{
			mystate = AnimState.Walking;
			//Debug.Log("control object moving");
		}	
		
		//Check to see if there are any enemies in range, or a specific enemy to attack set
		if(mySpecificTarget != null && myTargetsList.Contains(mySpecificTarget))
		{
			currentTarget = mySpecificTarget;
		}
		else if(myTargetsList.Count > 0)
		{
			//pick a target to attack (last of the list)
			currentTarget = myTargetsList[myTargetsList.Count - 1];
		}
		
		if(currentTarget != null)
		{
			Attack ();
		}
		
		//animate character based on state
		Animate(mystate);
		
		//record current position for next round to check if it has changed between rounds
		previous_control_x = controller.transform.position.x;
		previous_control_z = controller.transform.position.z;
	
	}
	
	//Attack
	void Attack()
	{
		//todo: get target type to know if this is an attack on an enemy, breakable or vehicle
		
		//turn towards target
		if(isControlled == false)
			controller.Move(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z) * Time.deltaTime);
		mystate = AnimState.Attack;
	}	
	
	//Death
	void Die()
	{
		mystate = AnimState.Death;
	}
	
	//Change
	void Change()
	{
		Debug.Log ("changing");
		mystate = AnimState.Change;
	}
	
	//GetHit
	public void GetHit()
	{
		Debug.Log ("got hit");
	}
	
	//Drive
	void Drive()
	{
		Debug.Log ("Driving");
	}
	
	void Animate(AnimState mystate)
	{
		switch (mystate) {
		case AnimState.Idle: theanimation.CrossFade("idle");
			theanimation["idle"].speed = idleAnimationSpeed;
			break;
		case AnimState.Walking: theanimation.CrossFade ("walk");
			theanimation["walk"].speed = walkAnimationSpeed;
			break;
		case AnimState.Reversing: theanimation.CrossFade("walk");
			theanimation["walk"].speed = -1 * walkAnimationSpeed;
			break; 
		case AnimState.Running: theanimation.CrossFade ("run");
			theanimation["run"].speed = runAnimationSpeed;
			break;
		case AnimState.Sidestepping: theanimation.CrossFade ("walk");
			theanimation["walk"].speed = sidestepAnimationSpeed;
			break;
		case AnimState.Attack: theanimation.CrossFade("attack");
			theanimation["walk"].speed = attackAnimationSpeed;
			break;
		case AnimState.Death: theanimation.CrossFade("death");
			theanimation["walk"].speed = deathAnimationSpeed;
			break;
		case AnimState.GetHit: theanimation.CrossFade("gethit");
			theanimation["walk"].speed = gethitAnimationSpeed;
			break;
		default: theanimation.CrossFade ("idle");
			theanimation["idle"].speed = idleAnimationSpeed;
			break;
		}
	}
	
	void SetIdle()
	{
		mystate = AnimState.Idle;
	}
	
	//add the enemy to the list of targets when it enters the trigger area
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			//Debug.Log("Enemy added in list of enemies");
			myTargetsList.Add(other.transform);
		}
	}
	
	//remove the enemy from the list of targets when it exits the trigger area
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			//Debug.Log("Enemy removed from list of enemies");
			if(myTargetsList.Contains(other.transform))
				myTargetsList.Remove(other.transform);			
		}
	}
}
