using UnityEngine;
using System.Collections;

public class GuideText : MonoBehaviour {
	
	public string text = "Write here";
	private bool isShown = false;
	private GUIText gtext;

	// Use this for initialization
	void Start () 
	{
		gtext = transform.FindChild("GUI Text").GetComponent<GUIText>();
		gtext.text = text;
		gtext.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag == "Player" && !isShown)
		{
			print (text);
			isShown = true;
			gtext.gameObject.SetActive(true);
			Invoke("StopMessage", 5f);
		}
	}
	
	void StopMessage()
	{
		gtext.gameObject.active = false;	
	}
}
