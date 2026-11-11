using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	[Header("Common")]
	public AudioClip Door;
	public AudioClip StageClear;

	[Space(10)]	

	[Header("Guns and such")]
	public AudioClip[] EmptyClip;
	public AudioClip PickUp;
	public AudioClip DropWeapon;
	
	[Space(10)]	

	[Header("Character")]
	public AudioClip MeleeHitSound;
	public AudioClip BulletHitSound;
	public AudioClip MeleeSwingSound;
	public AudioClip Thump;
	
	
	private static SoundManager _instance;
	
	public static SoundManager sounds {
		get { 
			if(_instance == null) {
				_instance = SRResources.SoundManager.Instantiate().GetComponent<SoundManager>();
			}
			
			return _instance; 
		}
	}
	
	void Start() {
		_instance = this;
	}
	
	public static void PlaySound (AudioClip sound, bool once = true, float vol = .8f) {
		if (sound == null)
			return;
		
		var source = new GameObject();
		source.name = "[sound " + sound.name + "]";
		var audio = source.AddComponent<AudioSource>();
		
		if (once)
			source.AddComponent<PlayOnceAudioSource>();
		
		audio.volume = vol;
		audio.clip = sound;
		audio.Play();
	}
	
	public static void PlaySound (AudioClip[] sound, bool once = true, float vol = .8f) {
		if (sound == null)
			return;
		
		PlaySound(sound[Random.Range(0, sound.Length)]);
	}
}
