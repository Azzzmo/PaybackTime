using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleHandler : MonoBehaviour {
	
	ControlObjHandler COH;
	List<Transform> charactersIn = new List<Transform>();
	public AudioClip moveSound;
	public AudioClip idleSound;
	bool active;
	Vector3 lastPosition;
	Vector3 currentPosition;
	
	// Use this for initialization
	void Start () {
		COH = this.GetComponent<ControlObjHandler>();
		COH.enabled = false;
		lastPosition = transform.position;
		active = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonUp("Jump") && charactersIn.Count > 0)
		{
			int i = 5;
			setControlling(false);
			
			
			foreach(Transform tr in charactersIn)
			{
				tr.position = new Vector3(i,0,i) + this.transform.position;
				tr.parent = null;	
				i += 2;
			}
			
			charactersIn.Clear();
		}
		
		currentPosition = transform.position;
	
		if(currentPosition != lastPosition && active)
			PlayActiveSound();
		else if(active)
			PlayIdleSound();
	}
	
	public void setControlling(bool setting)
	{
		COH.enabled = setting;
		active = setting;
	}
	
	public void setMovable(bool movable)
	{
		COH.Moveable = movable;	
	}
	
	public void makeChild(Transform child)
	{		
		child.parent = this.transform;
		child.position = new Vector3(0, -100, 0);
		charactersIn.Add(child);
	}
	
	public void PlayIdleSound()
	{
		if(!audio.isPlaying)
		{
			audio.clip = idleSound;
			audio.Play();
		}
		
	}
	
	public void PlayActiveSound()
	{
		if(!audio.isPlaying)
		{
			audio.clip = activeSound;
			audio.Play();
		}
	}
}
