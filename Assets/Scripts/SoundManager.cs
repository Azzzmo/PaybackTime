using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	//public CharacterSounds[] CharacterAudioSources;
	private List<Transform> CharacterAudioSources = new List<Transform>();
	
	public int minWaitTimeBetweenClips = 0;
	public int maxWaitTimeBetweenClips = 10;

	// Use this for initialization
	void Start () 
	{
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
		{
			if(go.GetComponent<CharacterSounds>() != null)
			{
				CharacterAudioSources.Add(go.transform);
			}
		}
		
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(go.GetComponent<CharacterSounds>() != null)
			{
				CharacterAudioSources.Add(go.transform);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(Transform tr in CharacterAudioSources)
		{
			CharacterBase CB = tr.GetComponent<CharacterBase>();
			CharacterSounds CS = tr.GetComponent<CharacterSounds>();
			
			if(CB.mystate == AnimState.Idle || CB.mystate == AnimState.Running || CB.mystate == AnimState.Walking)
			{
				CS.waitingToPlay = true;
				CS.Invoke("PlayIdleClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
			}
			
			else if(CB.mystate == AnimState.Attack || CB.mystate == AnimState.Breaking)
			{
				CS.waitingToPlay = true;
				CS.Invoke("PlayCombatClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
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
