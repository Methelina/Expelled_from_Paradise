using UnityEngine;
using System.Collections;

public class CharacterLegs : AnimatedSprite {
	[Space(10)]
	[Header("Leg animations")]
	[Space(10)]
	
	public AnimSequence anim_running;
	public AnimSequence anim_stay;
	public AnimSequence anim_dead;
}
