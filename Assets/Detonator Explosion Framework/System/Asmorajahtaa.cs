using UnityEngine;
using System.Collections;

public class Asmorajahtaa : MonoBehaviour {
	
	public Object rajahys;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
			
		if(col.transform.tag == "Player"){
			//transform.FindChild("Detonator-MushroomCloud").transform.GetComponent<Detonator>().UpdateComponents();
			Instantiate(rajahys, col.transform.position, new Quaternion(0,0,0,0));
			//transform.FindChild("Detonator-MushroomCloud").transform.GetComponent<Detonator>().Explode();
			col.transform.GetComponent<CharacterBase>().GetHit(1000);
			
		}
	}
}
