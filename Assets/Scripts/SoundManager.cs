using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	//public CharacterSounds[] CharacterAudioSources;
	private List<SoundObject> soundObjects = new List<SoundObject>();
	private List<SoundObject> enemySOs = new List<SoundObject>();
	private List<SoundObject> SOsInCombat = new List<SoundObject>(); 
	
	private Transform Caleb;
	
	public Transform musicSource;
	public AudioClip[] gameMusic;

	private AudioClip previousMusic;
	
	public int minWaitTimeBetweenClips = 5;
	public int maxWaitTimeBetweenClips = 60;

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
				if(CB.myType == CharacterType.Caleb)
					Caleb = go.transform;
				else
					soundObjects.Add(new SoundObject(CB, CS));
			}
		}
		
		foreach(GameObject ko in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			CharacterSounds CS = ko.GetComponent<CharacterSounds>();
			CharacterBase CB = ko.GetComponent<CharacterBase>();
			Enemy eai = ko.GetComponent<Enemy>();
			if(ko.GetComponent<CharacterSounds>() != null)
			{
				enemySOs.Add(new SoundObject(CB, CS, eai));
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
					SOsInCombat.Add(so);
				}
			}
				
		}
		
		foreach(SoundObject esso in enemySOs)
		{
			print(esso.eScript.m_type);
			if(esso.eScript.inCombat)
			{

				esso.sounds.waitingToPlay = true;
				esso.sounds.Invoke("PlayCombatClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
			}
			else
			{

				esso.sounds.waitingToPlay = true;
				esso.sounds.Invoke("PlayIdleClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
			}
		}
		
		if(SOsInCombat.Count > 0)
		{
			Caleb.GetComponent<CharacterSounds>().waitingToPlay = true;
			Caleb.GetComponent<CharacterSounds>().Invoke("PlayCombatClip", Random.Range(minWaitTimeBetweenClips, maxWaitTimeBetweenClips));
			SOsInCombat.Clear();
		}
		
	}
}

public class SoundObject
{
	public bool life;
	public CharacterSounds sounds;
	public List<Transform> targets;
	public Enemy eScript;
	
	public SoundObject(CharacterBase CB, CharacterSounds CS)
	{
		life = CB.isAlive;
		sounds = CS;
		targets = CB.myTargetsList;
		eScript = null;
	}
	
	public SoundObject(CharacterBase CB, CharacterSounds CS, Enemy EScript)
	{
		life = CB.isAlive;
		sounds = CS;
		targets = CB.myTargetsList;
		eScript = EScript;
	}
}
