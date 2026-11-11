using UnityEngine;
using System.Collections;

public class PlayOnceAudioSource : MonoBehaviour {
	
	void Start () {
		
	}
	
	void Update () {
		var audio = GetComponent<AudioSource>();
		if (!audio.isPlaying)
			Destroy(gameObject);
	}
}