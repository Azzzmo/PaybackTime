using UnityEngine;
using System.Collections;

public class Asmorajahtaa : MonoBehaviour {
	
	public Object rajahys;
	public Object sytytys;
	public Object toinenkiPaukku;
	private Vector3 paikka;

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
			Instantiate(sytytys, col.transform.position, new Quaternion(0,0,0,0));
			paikka = col.transform.position;
			//transform.FindChild("Detonator-MushroomCloud").transform.GetComponent<Detonator>().Explode();
			col.transform.GetComponent<CharacterBase>().GetHit(1000);
			Invoke("TeeToinenPaukku", 1f);
		}
	}
	
	void TeeToinenPaukku()
	{
		Instantiate(toinenkiPaukku, paikka, new Quaternion(0,0,0,0));
	}
}
