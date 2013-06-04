using UnityEngine;
using System.Collections;

public class VehicleHandler : MonoBehaviour {
	
	ControlObjHandler COH;

	// Use this for initialization
	void Start () {
		COH = this.GetComponent<ControlObjHandler>();
		//COH.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EnableControlling()
	{
		COH.enabled = true;
	}
	
	public void setMovable(bool movable)
	{
		COH.Moveable = movable;	
	}
}
