using UnityEngine;
using System.Collections;

public class Pommi : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Tormays objektiin: " + collision.collider.gameObject.name);
		if (collision.collider is SphereCollider)
		Object.Destroy(gameObject);
	}
}
