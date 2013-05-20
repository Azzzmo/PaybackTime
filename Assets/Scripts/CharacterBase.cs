using UnityEngine;
using System.Collections;


//base class for defining character properties, character state (what is it doing), what actions it can perform. 
//Animation is executed based on state in a separate character animation script

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
	public bool canControl;
	public bool canDrive;
	public bool canBreak;
	public bool canAttack;
	
	//is character being controlled in first person view
	public bool isControlled;
	
	//character target
	private Transform mytarget;
	public Transform MyTarget
	{	
		get { return mytarget; }
		set { mytarget = value; }
	}
	
	//animation values
	
	
	// Use this for initialization
	void Start () {
		
		//set default attributes
		strength = 1;
		speed = 1; 
		intelligence = 1;
		
		//set default abilities
		canControl = false;
		canDrive = false;
		canBreak = false;
		canAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Attack
	void Attack()
	{
		
		//get target type to know if this is an attack on an enemy, breakable or vehicle
		if(mytarget != null && mytarget.tag == "Enemy")
			
			// get target tag
		
		
		//decide if attack enemy, brekable or drive vehicle
		
		//if attacking an enemy, initiate attack state
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
		mystate = AnimState.Change;
	}
	
	//GetHit
	public void GetHit()
	{
		
	}
	
	//Drive
	void Drive()
	{
		
	}
	
	void Animate(AnimState mystate)
	{

	}
}
