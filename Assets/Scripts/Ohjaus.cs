using UnityEngine;
using System.Collections;

public class Ohjaus : MonoBehaviour {
	
	public float nopeus = 1.0f;
	public float kaantoNopeus = 2.0f;
	public float aika;
	
	public GameObject pommi;
	
	// Use this for initialization
	void Start () {
	Screen.lockCursor = true;
	aika = Time.time;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")&& Time.time - aika > 1.0f)
		{
			Object.Instantiate (pommi, transform.position, transform.rotation);
			aika = Time.time;
		}
		
	}
	
	
	void FixedUpdate (){
		float liikeZ = Input.GetAxis ("Vertical")  * nopeus;
		float liikeX = Input.GetAxis ("Horizontal")  * nopeus;
		float kaanto = Input.GetAxis ("Mouse X") * kaantoNopeus;
		
		transform.Translate (new Vector3(liikeX, 0.0f, liikeZ));
		transform.Rotate (new Vector3(0.0f, kaanto, 0.0f));
	}
	
	void LateUpdate(){
		Camera.main.transform.position = transform.position - transform.TransformDirection(new Vector3(0.0f, 0.0f, 5.0f));
		Camera.main.transform.rotation = transform.rotation;
	}
	
	
}
