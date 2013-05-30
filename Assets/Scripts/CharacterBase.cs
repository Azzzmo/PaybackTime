using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//base class for defining character properties, character state (what is it doing), what actions it can perform. 

public class CharacterBase : MonoBehaviour {
	
	//character type (enum)
	public CharacterType myType;
	
	//basic attributes
	public int strength;
	public float maxspeed;
	public int intelligence;
	public int health;
	
	public int shootDamage;
	public int meleeDamage;
	
<<<<<<< HEAD
=======
	//combat timers and variables
	public float attackRate = 1f;
	private float attackTimer;
	
	//state change timer is used for allowing animation to finish before starting another one unless 
	private float stateChangeTimer;
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
	
	//movement and position
	private Vector3 moveDirection;
	public float gravity;
	public float meleeDistance;
<<<<<<< HEAD
	
=======
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
	
	//character states todo: enum - use in character animation script
	public AnimState mystate;
	
	//character abilities
	public bool canDrive;
	public bool canBreak;
	public bool canAttack;
	public bool canShoot;
	
	//set enemy tag name
	public string enemyTag;

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
	public Transform currentTarget;
	
	
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
		
		//movement and position
		moveDirection = Vector3.zero;
		
		//state change timer to track if animation is being run
		stateChangeTimer = 0f;
			
		//set attack timer to attack rate, so that first attack wont have to wait
		attackTimer = attackRate;
		
		//set default animation state at start
		Invoke("SetIdle", Random.value * 10);
<<<<<<< HEAD
=======
		//mystate = AnimState.Idle;
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
		
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
<<<<<<< HEAD
		
		//if already dead, destroy parent object
		if(mystate == AnimState.Death)
			Destroy(gameObject, 5f);
		
		//reset currentTarget enemy
		currentTarget = null;
		
		if(cam != null && cam.gameObject.activeSelf)
			isControlled = true;
		else 
			isControlled = false;
=======
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
		
		if(IsAlive())
		{
			//reset currentTarget enemy
			currentTarget = null;
			
			//if there's a camera active in gameobject, it's controlled by keyboard
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
	 		if((controller.transform.position.x != previous_control_x || controller.transform.position.z != previous_control_z) && isControlled == false)
			{
				mystate = AnimState.Walking;
				//Debug.Log("control object moving");
			}	
			
			
			//Check to see if there are any enemies in range, or a specific enemy to attack set
			if(mySpecificTarget != null && myTargetsList.Contains(mySpecificTarget) && mySpecificTarget.gameObject.GetComponent<CharacterBase>().IsAlive())
			{
				currentTarget = mySpecificTarget;
			}
			else if(myTargetsList.Count > 0)
			{
				//check all enemies in target list that they are not dead, and if they are, remove from list
				foreach(Transform enemy in myTargetsList)
				{
					if(!enemy.gameObject.GetComponent<CharacterBase>().IsAlive())
						myTargetsList.Remove(enemy);
				}
				
				//pick a target to attack (last of the list)
				if(myTargetsList.Count > 0)
					currentTarget = myTargetsList[myTargetsList.Count - 1];
			}
			
			if(currentTarget != null && canShoot == false)
			{
				Attack ();
			}

			//animate character based on state
			Animate(mystate);
			
		} //end if(IsAlive());
		
		//record current position for next round to check if it has changed between rounds
		previous_control_x = controller.transform.position.x;
		previous_control_z = controller.transform.position.z;
	
	}
	
	//Attack
	void Attack()
	{
		//todo: get target type to know if this is an attack on an enemy, breakable or vehicle
		
		if(isControlled == false)
		{
				//move character to range
				transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));
				moveDirection = currentTarget.position - transform.position;//this.transform.forward;
				moveDirection.Normalize();
				moveDirection *= maxspeed;
				
				if(Vector3.Distance(new Vector3(currentTarget.position.x, 0f, currentTarget.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) > meleeDistance)
				{
					mystate = AnimState.Walking;
					moveDirection.y -= gravity * Time.deltaTime;
					controller.Move(moveDirection * Time.deltaTime);
				}
<<<<<<< HEAD
				else if(Vector3.Distance(new Vector3(currentTarget.position.x, 0f, currentTarget.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) <= meleeDistance)
				{
						mystate = AnimState.Attack;
						currentTarget.gameObject.GetComponent<CharacterBase>().GetHit(meleeDamage);
=======
				//if in range, attack
				else if(Vector3.Distance(new Vector3(currentTarget.position.x, 0f, currentTarget.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) <= meleeDistance)
				{
						if(attackTimer >= attackRate && currentTarget.gameObject.GetComponent<CharacterBase>().IsAlive())
						{
							mystate = AnimState.Attack;
							Animate(mystate);
							Debug.Log("attacking");
							currentTarget.gameObject.GetComponent<CharacterBase>().GetHit(meleeDamage);
							attackTimer = 0f;
						}
						attackTimer += Time.deltaTime;
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
				}
			
		}
		else if(Input.GetKey(KeyCode.Space))
			mystate = AnimState.Attack;
	}	
	
	//Death
	void Die()
	{
		Debug.Log(this.name + " I died!");
<<<<<<< HEAD
=======
		this.tag = "Dead";
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
		mystate = AnimState.Death;
		Animate(mystate);
		//Destroy(this.gameObject, 10f);
		
		//remove control object handler
		this.GetComponentInChildren<ControlObjHandler>().enabled = false;
		
		//remove camera
		DestroyObject(cam.gameObject);
		
		//remove selection indicator
		if(transform.FindChild("SelectedIndicator") != null)
		{
			transform.FindChild("SelectedIndicator").gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	
	//Change
	void Change()
	{
		//Debug.Log ("changing");
		mystate = AnimState.Change;
	}
	
	//GetHit
	public void GetHit(int damage)
<<<<<<< HEAD
	{
		Debug.Log ( this.name + "got hit");
		health -= damage;
		if(health <= 0)
			Die();
	}
	
	//Drive
	void Drive()
=======
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
	{
		mystate = AnimState.GetHit;
		Debug.Log ( this.name + " got hit");
		
		Animate(mystate);
		health -= damage;
		if(health <= 0)
			Die();
	}
	
	void Animate(AnimState mystate)
	{
		//idle, death, walking not included in state change, as they do not need to finish, but attack, get hit, change need to use timer. If dead, infinite
		stateChangeTimer -= Time.deltaTime;

		switch (mystate) {
		case AnimState.Idle: 
			if(stateChangeTimer <= 0) 
			{
				theanimation.CrossFade("idle");
				theanimation["idle"].speed = idleAnimationSpeed;
			}
			break;
		case AnimState.Walking:
			theanimation.CrossFade ("walk");
			theanimation["walk"].speed = walkAnimationSpeed;
			break;
		case AnimState.Reversing: 
			theanimation.CrossFade("walk");
			theanimation["walk"].speed = -1 * walkAnimationSpeed;
			break; 
		case AnimState.Running: 
			theanimation.CrossFade ("run");
			theanimation["run"].speed = runAnimationSpeed;
			break;
		case AnimState.Sidestepping: 
			theanimation.CrossFade ("walk"); //to be changed
			theanimation["walk"].speed = sidestepAnimationSpeed;
			break;
		case AnimState.Attack: 
			if(stateChangeTimer <= 0)
			{
				theanimation.CrossFade("attack");
				theanimation["attack"].speed = attackAnimationSpeed;
 				stateChangeTimer = theanimation["attack"].length;
			}
			break;
		case AnimState.Death: 
			theanimation.CrossFade("death");
			theanimation["death"].speed = deathAnimationSpeed;	
			stateChangeTimer = theanimation["death"].length + 1000f;
			break;
		case AnimState.GetHit: if(stateChangeTimer <= 0)
			{
				theanimation.CrossFade("gethit");
				theanimation["gethit"].speed = gethitAnimationSpeed;
				stateChangeTimer = theanimation["gethit"].length;
			}
			break;
		default: 
			if(stateChangeTimer <= 0)
			{
				theanimation.CrossFade ("idle");
			 	theanimation["idle"].speed = idleAnimationSpeed;
			}
			break;
		}
	}
	
	void SetIdle()
	{
		mystate = AnimState.Idle;
		Debug.Log("idle set");
	}
	
	//add the enemy to the list of targets when it enters the trigger area
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == enemyTag)
		{
			//Debug.Log("Enemy added in list of enemies");
			myTargetsList.Add(other.transform);
		}
	}
	
	//remove the enemy from the list of targets when it exits the trigger area
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == enemyTag)
		{
			//Debug.Log("Enemy removed from list of enemies");
			if(myTargetsList.Contains(other.transform))
				myTargetsList.Remove(other.transform);
		}
	}
	
	//shoot script is alternative to enemy characters that can shoot, and is called from enemy script. 
	public void Shoot(Transform enemy, float shootdistance)
	{
		Vector3 targetDirection = enemy.transform.position - transform.position;
		
		RaycastHit rHit;
		if(Physics.Raycast(this.transform.position, targetDirection, out rHit, shootdistance))
		{
			//animation.CrossFade("shoot");
			Debug.Log("shooting");
			enemy.gameObject.GetComponent<CharacterBase>().GetHit(shootDamage);
		}
		
	}
	
	public bool IsAlive()
	{
		if(health > 0)
			return true;
		else
			return false;
	}
<<<<<<< HEAD
	
=======
>>>>>>> 8de82df7b09626c9a93df36f26f8027436287f7c
}
