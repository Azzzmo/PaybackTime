using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	//public CharacterSounds[] CharacterAudioSources;
	public List<CharacterSounds> CharacterAudioSources = new List<CharacterSounds>();

	// Use this for initialization
	void Start () 
	{
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(CharacterSounds tr in CharacterAudioSources)
		{
			if(tr != null && !tr.audio.isPlaying && !tr.waitingToPlay)
			{
				tr.waitingToPlay = true;
				tr.Invoke("PlayIdleClip", Random.Range(0, 10));	
			}
				
		}
		
	}
}
