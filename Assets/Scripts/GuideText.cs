using UnityEngine;
using System.Collections;

public class GuideText : MonoBehaviour {
	
	public string text = "Write here";
	private bool isShown = false;
	public GUIText opaste;
	OpasteTekstit opasteet;

	// Use this for initialization
	void Start () 
	{
		opasteet = opaste.GetComponentInChildren<OpasteTekstit>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag == "Player" && !isShown)
		{
			isShown = true;
			
			//opaste.text = text;
			opaste.text = opasteet.GetText(text);
			opaste.enabled = true;
			Debug.Log (opaste.text);
			
			Invoke("StopMessage", 5f);
		}
	}
	
	void StopMessage()
	{		
		opaste.enabled = false;
	}
}
