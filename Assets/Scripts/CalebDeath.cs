using UnityEngine;
using System.Collections;

public class CalebDeath : MonoBehaviour {
	
	private CharacterBase CalebCB;
	private GameObject guitext;
	public float timetoshowtext = 10f;
	private GUIText text;

		
	// Use this for initialization
	void Start () {
		guitext = GameObject.Find("GUI Text");
		text = guitext.GetComponent<GUIText>();
		
		text.text = "Caleb Died! Game Over...";
		CalebCB = GetComponentInChildren<CharacterBase>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!CalebCB.IsAlive())
		{
			timetoshowtext -= Time.deltaTime;
			if(timetoshowtext > 0f)
			{
				text.text = "Caleb Died! Game Over...";
				text.enabled = true;
			}
			else
			{
				text.enabled = false;
				Application.LoadLevel("MainMenu");
				
			}
		}
	
	}
}
