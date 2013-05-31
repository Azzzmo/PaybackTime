using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	//public CharacterSounds[] CharacterAudioSources;
	private List<SoundObject> soundObjects = new List<SoundObject>();
	
	public int minWaitTimeBetweenClips = 0;
	public int maxWaitTimeBetweenClips = 10;

	// Use this for initialization
	void Start () 
	{
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
		{
			CharacterSounds CS = go.GetComponent<CharacterSounds>();
			if(CS != null)
			{
				soundObjects.Add(new SoundObject(go.GetComponent<CharacterBase>().currentTarget, CS));
			}
		}
		
		foreach(GameObject ko in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			CharacterSounds CS = ko.GetComponent<CharacterSounds>();
			if(ko.GetComponent<CharacterSounds>() != null)
			{
				soundObjects.Add(new SoundObject(ko.GetComponent<CharacterBase>().currentTarget, CS));
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(SoundObject so in soundObjects)
		{			
			if(so.soundTarget == null)
			{
				if(so.sounds != null && !so.sounds.audio.isPlaying && !so.sounds.waitingToPlay)
				{
					so.sounds.waitingToPlay = true;
					so.sounds.Invoke("PlayIdleClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
				}
			}
			
			else if(so.soundTarget != null)
			{
				if(so.sounds != null && !so.sounds.audio.isPlaying && !so.sounds.waitingToPlay)
				{
					so.sounds.waitingToPlay = true;
					so.sounds.Invoke("PlayCombatClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
				}
			}
			
			
			
			/*
			if(tr != null && !tr.audio.isPlaying && !tr.waitingToPlay)
			{
				tr.waitingToPlay = true;
				tr.Invoke("PlayIdleClip", Random.Range(0, 10));	
			}*/
				
		}
		
	}
}

public class SoundObject
{
	public Transform soundTarget;
	public CharacterSounds sounds;
	
	public SoundObject(Transform target, CharacterSounds CS)
	{
		soundTarget = target;
		sounds = CS;
	}
	
}
