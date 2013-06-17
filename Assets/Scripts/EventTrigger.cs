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
		print (col.transform.name);
		
		if(col.transform.name == "NZ_kubelwagen_final")
		{
			print ("auto paikalla");
			
			VehicleHandler VH = col.transform.parent.GetComponent<VehicleHandler>();
			
			if(VH.calebIn)
			{
				print ("pitäs olla voitettu");
				
				text.text = "You Won!";
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