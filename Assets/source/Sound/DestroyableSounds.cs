using UnityEngine;
using System.Collections;

public class DestroyableSounds : MonoBehaviour {
	public AudioClip OnHitFX;
	public AudioClip OnDeathFX;
	
	void ApplyDamage(float damage) {
		SoundManager.PlaySound(OnHitFX);
	}
	
	void OnKilled() {
		SoundManager.PlaySound(OnDeathFX);
	}
}
