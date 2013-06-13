using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {
	
	public GameObject guitext;
	public float timetoshowtext = 10f;
	private GUIText text;

	// Use this for initialization
	void Start () 
	{
		text = guitext.GetComponent<GUIText>();
		text.text = "You Won!";
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
				text.enabled = true;
				Invoke("ShowMenu", timetoshowtext);
				
			}
		}
	}
	
	void ShowMenu()
	{
		text.enabled = false;
		Application.LoadLevel("MainMenu");
	}
	
}