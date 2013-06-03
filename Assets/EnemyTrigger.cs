using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {
	
	CharacterBase CB;

	// Use this for initialization
	void Start () {	
		
		CB = transform.parent.GetComponent<CharacterBase>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider other)
	{
		CB.AddEnemyToList(other.transform);
	}
	
	//remove the enemy from the list of targets when it exits the trigger area
	public void OnTriggerExit(Collider other)
	{
		CB.RemoveEnemyFromList(other.transform);
	}
}
