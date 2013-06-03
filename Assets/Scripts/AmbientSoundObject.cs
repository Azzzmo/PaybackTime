using UnityEngine;
using System.Collections;

public class AmbientSoundObject : MonoBehaviour {
	
	public int minWait = 0;
	public int maxWait = 10;
	public AudioClip[] ambientSounds;
	private AudioClip previousClip;
	private bool waiting;

	// Use this for initialization
	void Start () {
		waiting = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!audio.isPlaying)
		{
			waiting = true;
			Invoke("PlayAmbient", Random.Range(minWait, maxWait));
		}
	
	}
	
	void PlayAmbient()
	{
		audio.clip = ambientSounds[Random.Range(0, ambientSounds.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayAmbient();
		}
		else
		{
			previousClip = audio.clip;
			waiting = false;
			audio.Play();	
		}
	}
}
