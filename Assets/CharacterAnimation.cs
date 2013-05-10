using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	
	public float vAxis; 
	public float hAxis;
	
	
	enum animState
	{
		Idle = 0, Walking = 1, Sidestepping = 2, Running = 3, Jumping = 4, Reversing = 5,
	}
	
	animState mystate = animState.Idle;
	
	// Use this for initialization
	void Start () {
		
		animation.Play("idle");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		vAxis = Input.GetAxisRaw("Vertical");
		hAxis = Input.GetAxisRaw("Horizontal");
		
		if(vAxis > 0)
			mystate = animState.Walking;
		else if (vAxis < 0)
			mystate = animState.Reversing;
		else if(vAxis == 0 || hAxis == 0)
			mystate = animState.Idle;
		else if(hAxis > 0 || hAxis < 0)
			mystate = animState.Walking;
		else
			mystate = animState.Idle;
		
		switch (mystate) {
		case animState.Idle: animation.Play("idle");
			break;
		case animState.Walking: animation.Play("walk");
			break;
		case animState.Reversing: animation.Play ("walk");
			animation["walk"].speed = -1;
			break;
		case animState.Running: animation.Play ("run");
			break;
		case animState.Sidestepping: animation.Play ("jump_pose");	
			break;
		default: animation.Play ("idle");
			break;
		}

		
	}
}
