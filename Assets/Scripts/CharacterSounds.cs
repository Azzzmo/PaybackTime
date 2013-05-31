using UnityEngine;
using System.Collections;

public class CharacterSounds : MonoBehaviour {
	
	//public List<AudioClip> idleClips = new List<AudioClip>();
	public AudioClip[] idleClips;
	public AudioClip[] combatClips;
	public AudioClip[] selectClips;
	public AudioClip[] attackClips;
	public AudioClip[] getHitClips;
	public AudioClip[] deathClips;
	private AudioClip previousClip;
	public bool waitingToPlay;
	
	//public SoundManager SM;

	// Use this for initialization
	void Start () 
	{
		waitingToPlay = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void PlayIdleClip()
	{
		audio.clip = idleClips[Random.Range(0, idleClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayIdleClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
	}
	
	public void PlayCombatClip()
	{
		audio.clip = combatClips[Random.Range(0, combatClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayCombatClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
	}
	
	public void PlaySelectClip()
	{
		audio.clip = selectClips[Random.Range(0, selectClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlaySelectClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
		
	}
	
	public void PlayAttackClip()
	{
		audio.clip = attackClips[Random.Range(0, attackClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayAttackClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
	}
	
	public void PlayGetHitClip()
	{
		audio.clip = getHitClips[Random.Range(0, getHitClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayGetHitClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
	}
	
	public void PlayDeathClip()
	{
		audio.clip = deathClips[Random.Range(0, deathClips.Length)];
		
		if(previousClip == audio.clip)
		{
			PlayDeathClip();
		}
		else
		{
			previousClip = audio.clip;
			waitingToPlay = false;
			audio.Play();	
		}
	}
	
	void OnDestroy()
	{
		//SM.CharacterAudioSources.Remove(this);
	}
}
