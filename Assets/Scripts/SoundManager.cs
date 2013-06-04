using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	//public CharacterSounds[] CharacterAudioSources;
	private List<SoundObject> soundObjects = new List<SoundObject>();
	
	public Transform musicSource;
	public AudioClip[] gameMusic;

	private AudioClip previousMusic;
	
	public int minWaitTimeBetweenClips = 0;
	public int maxWaitTimeBetweenClips = 10;

	// Use this for initialization
	void Start () 
	{
		//musicSource = transform.Find("Terrain");
		musicSource.audio.clip = gameMusic[Random.Range(0, gameMusic.Length)];
		previousMusic = musicSource.audio.clip;
		musicSource.audio.Play();
		
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
		{
			CharacterSounds CS = go.GetComponent<CharacterSounds>();
			CharacterBase CB = go.GetComponent<CharacterBase>();
			if(CS != null)
			{
				soundObjects.Add(new SoundObject(CB.isAlive, CS, CB.myTargetsList));
			}
		}
		
		foreach(GameObject ko in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			CharacterSounds CS = ko.GetComponent<CharacterSounds>();
			CharacterBase CB = ko.GetComponent<CharacterBase>();
			if(ko.GetComponent<CharacterSounds>() != null)
			{
				soundObjects.Add(new SoundObject(CB.isAlive, CS, CB.myTargetsList));
			}
		}
		
	}
	
	void PlayMusic()
	{
		musicSource.audio.clip = gameMusic[Random.Range(0, gameMusic.Length)];
		
		if(previousMusic == musicSource.audio.clip)
		{
			PlayMusic();
		}
		else
		{
			previousMusic = musicSource.audio.clip;
			musicSource.audio.Play();	
		}	
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!musicSource.audio.isPlaying)
			PlayMusic();
		
		
		foreach(SoundObject so in soundObjects)
		{			
			if(so.targets.Count == 0 && so.life)
			{
				if(so.sounds != null && !so.sounds.audio.isPlaying && !so.sounds.waitingToPlay)
				{
					so.sounds.waitingToPlay = true;
					so.sounds.Invoke("PlayIdleClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
				}
			}
			
			else if(so.targets.Count > 0 && so.life)
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
	public bool life;
	public CharacterSounds sounds;
	public List<Transform> targets;
	
	public SoundObject(bool isAlive, CharacterSounds CS, List<Transform> sTargets)
	{
		life = isAlive;
		sounds = CS;
		targets = sTargets;
	}
	
}
