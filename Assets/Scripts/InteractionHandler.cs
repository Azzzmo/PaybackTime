using UnityEngine;
using System.Collections;

public class CombatMechanic : MonoBehaviour {
	
	private SelectionBehavior selectionBehaviour;
	private RaycastHit TargetCastInfo;
	
	private Transform enemyTarget;

	// Use this for initialization
	void Start () 
	{	
		selectionBehaviour = GameObject.Find("Main Camera").GetComponent<SelectionBehavior>();		
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
					trans.GetComponent<CharacterBase>().MyTarget = TargetCastInfo.transform;
				}
			}
		}

	}
}
