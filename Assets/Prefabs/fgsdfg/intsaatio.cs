using UnityEngine;
using System.Collections;

public class intsaatio : MonoBehaviour {
	
	public Transform calebPaikka;
	public Transform smartPaikka;
	public Transform fastPaikka;
	public Transform strongPaikka;
	
	public Object caleb;
	public Object smart;
	public Object fast;
	public Object strong;
	

	// Use this for initialization
	void Start () {
		Instantiate(caleb, calebPaikka.position, new Quaternion(0,0,0,0));
		Instantiate(smart, smartPaikka.position, new Quaternion(0,0,0,0));
		Instantiate(fast, fastPaikka.position, new Quaternion(0,0,0,0));
		Instantiate(strong, strongPaikka.position, new Quaternion(0,0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}