using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpasteTekstit : MonoBehaviour {
	
	Dictionary<string, string> opasteet;

	// Use this for initialization
	void Start () {
		
		opasteet = new Dictionary<string, string>();
		
		opasteet.Add("car", "There's a car ahead. Use it to get away from the city. \nYou have to have a smart Zombie (purple indicator) as a driver.");
		opasteet.Add("hidden", "Ok. Now Caleb has his hidden stuff with him. \nNow try to find your way to the west.");
		opasteet.Add ("select", "Select your troops with a mouse drag \nand move them behind the synagogue.");
		opasteet.Add ("final", "The final roadblock! Destroy it and get away in the car!");
		opasteet.Add ("roadblock", "There is a roadblock ahead! \nUse your strong Zombie (red indicator) to break it.");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public string GetText(string key)
	{
		if(opasteet.ContainsKey(key))
			return opasteet[key].ToString();
		else
			return "";
	}
}
