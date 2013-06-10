using UnityEngine;
using System.Collections;

public class PaaOhjelma : MonoBehaviour {
	
	public GameObject prefab;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Object.Instantiate (prefab, Vector3.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
