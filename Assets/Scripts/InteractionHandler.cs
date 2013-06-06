using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InteractionHandler : MonoBehaviour {
	
	private SelectionBehavior selectionBehaviour;
	private RaycastHit TargetCastInfo;
	
	private Transform enemyTarget;
	private VehicleHandler wagen;
	
	private List<MoveToCarObj> moveToCar = new List<MoveToCarObj>();

	// Use this for initialization
	void Start () 
	{	
		selectionBehaviour = GameObject.Find("Main Camera").GetComponent<SelectionBehavior>();	
		wagen = GameObject.FindGameObjectWithTag("Car").transform.GetComponent<VehicleHandler>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TargetCastInfo = selectionBehaviour.getTargetCastInfo();

		if(selectionBehaviour.getSelectedTarget() != null && TargetCastInfo.transform != null)
		{
			if(Input.GetMouseButtonUp(1))
			{
				foreach (Transform trans in selectionBehaviour.multiselect_toggle.getCurrentlySelected())
				{			
					CharacterBase CB = trans.GetComponent<CharacterBase>();
					
					if(TargetCastInfo.transform.tag == "Car")
					{
						print("Kuski käsketty!");
						moveToCar.Add(new MoveToCarObj(trans, CB, TargetCastInfo.transform.position));
					}
					else if(TargetCastInfo.transform.tag == "Enemy")
					{
						print ("Spesifinen targetti annettu: " + TargetCastInfo.transform.name);
						CB.MyTarget = TargetCastInfo.transform;
					}
					else if (TargetCastInfo.transform.tag == "Brakable" && CB.myType == CharacterType.StrongZombi)
					{
						print ("Spesifinen targetti annettu: " + TargetCastInfo.transform.name);
						CB.MyTarget = TargetCastInfo.transform;
					}
				}
			}
		}
		
		foreach(MoveToCarObj mtco in moveToCar)
		{
			MoveTo(mtco);
		}

	}
	
	void MoveTo(MoveToCarObj MTCO)
	{
		//move character to range
		MTCO.ownTrans.LookAt(new Vector3(MTCO.ownMoveTo.x, MTCO.ownTrans.position.y, MTCO.ownMoveTo.z));
		Vector3 moveDirection = MTCO.ownMoveTo - MTCO.ownTrans.position;//this.transform.forward;
		moveDirection.Normalize();
		moveDirection *= MTCO.ownCB.maxspeed;
		
		if(Vector3.Distance(new Vector3(MTCO.ownMoveTo.x, 0f, MTCO.ownMoveTo.z), new Vector3(MTCO.ownTrans.position.x, 0f, MTCO.ownTrans.position.z)) > MTCO.ownCB.meleeDistance +3)
		{
			MTCO.ownCB.mystate = AnimState.Walking;
			//moveDirection.y -= gravity * Time.deltaTime;
			//MTCO.ownTrans.GetComponent<CharacterController>().Move(moveDirection * MTCO.ownCB.maxspeed * Time.deltaTime);
			MTCO.ownTrans.Translate(moveDirection * MTCO.ownCB.maxspeed * Time.deltaTime, Space.World);
		}
		//if in range, attack
		else if(Vector3.Distance(new Vector3(MTCO.ownMoveTo.x, 0f, MTCO.ownMoveTo.z), new Vector3(MTCO.ownTrans.position.x, 0f, MTCO.ownTrans.position.z)) <= MTCO.ownCB.meleeDistance +3)
		{
			MTCO.movingTo = false;
			moveToCar.Remove(MTCO);
			print ("Auton vieressä!");
			
			wagen.makeChild(MTCO.ownTrans);
			
			if(MTCO.ownCB.canDrive)
				wagen.setControlling(true);
		}	
	}
	

}

public class MoveToCarObj
{
	public Transform ownTrans;
	public CharacterBase ownCB;
	public Vector3 ownMoveTo;
	public bool movingTo;
	
	public MoveToCarObj(Transform trans, CharacterBase CB, Vector3 moveTo)
	{
		ownTrans = trans;
		ownCB = CB;
		ownMoveTo = moveTo;
		movingTo = true;
	}
}