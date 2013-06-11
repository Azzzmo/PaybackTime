using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {
	

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		CharacterBase CB = col.gameObject.GetComponentInChildren<CharacterBase>();
		if(CB != null)
		{
			if(CB.myType == CharacterType.Caleb)
			{
				Debug.Log ("Game Ended");	
			}
		}
	}
	
}