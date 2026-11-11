using UnityEngine;
using System.Collections;

public class CharacterHead : AnimatedSprite {
	public AnimSequence anim_alive;	
	public AnimSequence anim_dead;	

	void Start () {
		Play(anim_alive);
	}
	
	void Update () {
	
	}
}
