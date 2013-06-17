using UnityEngine;
using System.Collections;

public class GuideText : MonoBehaviour {
	
	public string text = "Write here";
	private bool isShown = false;
	public GUIText opaste;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag == "Player" && !isShown)
		{
			isShown = true;
			
			opaste.text = text;
			opaste.enabled = true;
			
			Invoke("StopMessage", 5f);
		}
	}
	
	void StopMessage()
	{		
		opaste.enabled = false;
	}
}
