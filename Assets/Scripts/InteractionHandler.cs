using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour {
	
	private SelectionBehavior selectionBehaviour;
	private RaycastHit TargetCastInfo;
	
	private Transform enemyTarget;
	private VehicleHandler wagen;

	// Use this for initialization
	void Start () 
	{	
		selectionBehaviour = GameObject.Find("Main Camera").GetComponent<SelectionBehavior>();	
		wagen = GameObject.Find("Car").transform.GetComponent<VehicleHandler>();
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
						if(CB.canDrive)
						{
							print("Kuski käsketty!");
							selectionBehaviour.CaculateNewPathForOne(transform, TargetCastInfo.transform.position);
						}
						else
						{
							print ("Kyytiläinen käsketty!");
						}
					}
					else
					{
						CB.MyTarget = TargetCastInfo.transform;
					}
				}
			}
		}

	}
}
