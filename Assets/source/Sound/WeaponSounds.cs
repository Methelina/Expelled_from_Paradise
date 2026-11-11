using UnityEngine;
using System.Collections;

public class WeaponSounds : MonoBehaviour {
	public AudioClip FireSound;
	public AudioClip ClipSound;
	
	void DoFire(Vector3 d) {
		SoundManager.PlaySound(FireSound);
	}
	
	void StopFire() {
		SoundManager.PlaySound(ClipSound);
	}
}
