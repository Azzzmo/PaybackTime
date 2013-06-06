using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//base class for defining character properties, character state (what is it doing), what actions it can perform. 

public class CharacterBase : MonoBehaviour {
	
	//character type (enum)
	public CharacterType myType;
	
	//basic attributes
	public int strength = 1;
	public float maxspeed = 1f;
	public int intelligence = 1;
	public int health = 1;
	public bool isAlive = true;
	
	public int shootDamage = 1;
	public int meleeDamage = 1;
	
	//combat timers and variables
	public float attackRate = 1f;
	private float attackTimer;
	
	//state change timer is used for allowing animation to finish before starting another one unless 
	private float stateChangeTimer;
	
	//movement and position
	private Vector3 moveDirection;
	public float gravity;
	public float meleeDistance;
	
	//character states
	public AnimState mystate;
	
	//character abilities
	public bool canDrive = false;
	public bool canBreak = false;
	public bool canAttack = false;
	public bool canShoot = false;
	
	//set enemy tag name
	public string enemyTag = "Enemy";

	//if object has camera component, it can be controlled
	private Transform cam;
	
	//is character being controlled in first person view
	public bool isControlled = false;
	
	//character specific target
	private Transform mySpecificTarget;
	public Transform MyTarget
	{	
		get { return mySpecificTarget; }
		set { mySpecificTarget = value; }
	}
	//list of targets in range
	public List<Transform> myTargetsList = new List<Transform>();
	private List<Transform> enemyremovelist = new List<Transform>();
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
	//private CharacterController controller;
	
	//sounds
	CharacterSounds sounds;
	
	void GoToHeaven() //take this guy to space, away from enemy range
	{
		//yield return new WaitForSeconds(5);
		Vector3 newPosition = new Vector3(transform.position.x, 1000f, transform.position.z);
		transform.position = newPosition;
	}
	
	// Use this for initialization
	void Start () {
		
		//movement and position
		moveDirection = Vector3.zero;

		//sounds script
		sounds = GetComponentInChildren<CharacterSounds>();
		
		//state change timer to track if animation is being run
		stateChangeTimer = 0f;
			
		//set attack timer to attack rate, so that first attack wont have to wait
		attackTimer = attackRate;
		
		//set default animation state at start
		Invoke("SetIdle", Random.value * 10);
		//mystate = AnimState.Idle;
		
		//controller = GetComponentInChildren<CharacterController>(); //assume there's a character controller, and find it
		theanimation = GetComponentInChildren<Animation>(); //assume there's an animation component, and find it
		
		//if gameobject has a "Character Camera" enabled, the object is being controlled by keyboard
		cam = transform.FindChild("Character Camera");
		
		//set current tranform location to trackers. If object is moved in RTS mode, the value changes, and default walk animation is played
		previous_control_x = transform.position.x;
		previous_control_z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
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
			
			
			//if transform has moved due to outside RTS control, set animation state accordingly
	 		if((transform.position.x != previous_control_x || transform.position.z != previous_control_z) && isControlled == false)
			{
				mystate = AnimState.Walking;
				//Debug.Log("control object moving");
			}	
			
			//Check to see if there are any enemies in range, or a specific enemy to attack set
			if(mySpecificTarget != null /*&& myTargetsList.Contains(mySpecificTarget)*/ && mySpecificTarget.gameObject.GetComponent<CharacterBase>().IsAlive())
			{
				currentTarget = mySpecificTarget;
			}
			else if(myTargetsList.Count > 0)
			{
				enemyremovelist.Clear();
				//check all enemies in target list that they are not dead, and if they are, remove from list
				foreach(Transform enemy in myTargetsList)
				{
					if(enemy.gameObject.GetComponent<CharacterBase>() != null)
						if(!enemy.gameObject.GetComponent<CharacterBase>().IsAlive())
							enemyremovelist.Add(enemy);	
				}
				
				foreach(Transform x in enemyremovelist)
				{
					myTargetsList.Remove(x);
				}
				
				//pick a target to attack (last of the list)
				if(myTargetsList.Count > 0)
					currentTarget = myTargetsList[myTargetsList.Count - 1];
			}
			
			if(currentTarget != null)
			{
				Attack ();
			}

			//animate character based on state
			Animate(mystate);
			
		} //end if(IsAlive());
		
		//record current position for next round to check if it has changed between rounds
		previous_control_x = transform.position.x;
		previous_control_z = transform.position.z;
	
	}
	
	//Attack
	void Attack()
	{
		//todo: get target type to know if this is an attack on an enemy, breakable or vehicle
		
		if(isControlled == false)
		{
				//move character to range
				transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));
				moveDirection = currentTarget.position - transform.position;
				moveDirection.Normalize();

				if(Vector3.Distance(new Vector3(currentTarget.position.x, 0f, currentTarget.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) > meleeDistance)
				{
					mystate = AnimState.Walking;
					moveDirection.y = 0f;
					transform.Translate(moveDirection * maxspeed * Time.deltaTime, Space.World);
				}
				//if in range, attack
				else if(Vector3.Distance(new Vector3(currentTarget.position.x, 0f, currentTarget.position.z), new Vector3(transform.position.x, 0f, transform.position.z)) <= meleeDistance)
				{
						if(attackTimer >= attackRate && currentTarget.gameObject.GetComponent<CharacterBase>().IsAlive())
						{
							mystate = AnimState.Attack;
							Animate(AnimState.Attack);
							Debug.Log("attacking");
							currentTarget.gameObject.GetComponent<CharacterBase>().GetHit(meleeDamage);
							attackTimer = 0f;
						}
						attackTimer += Time.deltaTime;
				}
			
		}
		else if(Input.GetKey(KeyCode.Space))
			mystate = AnimState.Attack;
	}	
	
	//Death
	void Die()
	{
		Debug.Log(this.name + " I died!");
		this.tag = "Dead";
		mystate = AnimState.Death;
		Animate(mystate);
		
		//get out of first person control mode by deactivating the character camera and activating the main camera
		//disable camera
		if(isControlled)
			cam.gameObject.GetComponent<Camera>().enabled = false;
		
		//remove control object handler
		if(this.GetComponentInChildren<ControlObjHandler>() != null)
			this.GetComponentInChildren<ControlObjHandler>().enabled = false;
		
		//remove selection indicator
		if(transform.FindChild("SelectedIndicator") != null)
		{
			transform.FindChild("SelectedIndicator").gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		
		Invoke("GoToHeaven", 4f);
		
		isAlive = false;
	
	}
	
	//Change
	void Change()
	{
		//Debug.Log ("changing");
		mystate = AnimState.Change;
	}
	
	//GetHit
	public void GetHit(int damage)
	{
		mystate = AnimState.GetHit;
		Debug.Log ( this.name + " got hit");
		
		if(myType == CharacterType.Nazi && mySpecificTarget == null)
		{
			transform.RotateAroundLocal(new Vector3(0,1,0), 180f);	
		}
		
		Animate(mystate);
		health -= damage;
		if(health <= 0)
			Die();
	}
	
	void Animate(AnimState mystate)
	{
		if(theanimation != null)
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
				theanimation["run"].speed = runAnimationSpeed / runAnimationSpeed;
				break;
			case AnimState.Sidestepping: 
				theanimation.CrossFade ("walk"); //to be changed
				theanimation["walk"].speed = sidestepAnimationSpeed / sidestepAnimationSpeed;
				break;
			case AnimState.Attack: 
				if(stateChangeTimer <= 0)
				{
					if(myType == CharacterType.StrongZombi) sounds.PlayAttackClip();
					theanimation.CrossFade("attack");
					theanimation["attack"].speed = attackAnimationSpeed;
	 				stateChangeTimer = theanimation["attack"].length / attackAnimationSpeed;
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
					stateChangeTimer = theanimation["gethit"].length / gethitAnimationSpeed;
				}
				break;
			default: 
				if(stateChangeTimer <= 0)
				{
					theanimation.CrossFade ("idle");
				 	theanimation["idle"].speed = idleAnimationSpeed / idleAnimationSpeed;
				}
				break;
			}
		}
	}
	
	void SetIdle()
	{
		mystate = AnimState.Idle;
		//Debug.Log("idle set");
	}
	
	public void AddEnemyToList(Transform transforminrange)
	{
		if(transforminrange.gameObject.tag == enemyTag && transforminrange.gameObject.GetComponent<CharacterBase>() != null)
			myTargetsList.Add (transforminrange);
	}
	
	public void RemoveEnemyFromList(Transform transforminrange)
	{
		if(transforminrange.gameObject.tag == enemyTag)
			if(myTargetsList.Contains(transforminrange))
				myTargetsList.Remove(transforminrange);
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
			Animate(AnimState.Attack);
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
}
