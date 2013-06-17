using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleHandler : MonoBehaviour {
	
	ControlObjHandler COH;
	List<Transform> charactersIn = new List<Transform>();
	public AudioClip moveSound;
	public AudioClip idleSound;

	Vector3 lastPosition;
	Vector3 currentPosition;
	public bool calebIn = false;
	
	// Use this for initialization
	void Start () {
		COH = this.GetComponent<ControlObjHandler>();
		COH.enabled = false;
		lastPosition = transform.position;
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
				tr.parent = null;
				
				tr.position = new Vector3(i,0,i) + this.transform.position;
					
				i += 2;
			}
			
			charactersIn.Clear();
			calebIn = false;
		}
		
		currentPosition = transform.position;
	
		if(currentPosition != lastPosition)
			PlayActiveSound();
		else if(active)
			PlayIdleSound();
		
		lastPosition = transform.position;
	}
	
	public void setControlling(bool setting)
	{
		COH.enabled = setting;
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
		
		CharacterBase CB = child.GetComponent<CharacterBase>();
		
		if(CB.myType == CharacterType.Caleb)
		{
			calebIn = true;	
		}
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
			audio.clip = moveSound;
			audio.Play();
		}
	}
	
}
