using UnityEngine;
using System.Collections;

public class CharacterTorso : AnimatedSprite {
	[Space(10)]
	[Header("Torso animations")]
	
	public AnimSequence anim_running;
	public AnimSequence anim_stay;

	[Space(10)]

	public AnimSequence anim_weapon_1;	
	public AnimSequence anim_weapon_2;

	[Space(10)]

	public AnimSequence anim_melee_1;
	public AnimSequence anim_melee_2;

	[Space(10)]
	
	public AnimSequence anim_punch;	
	public AnimSequence anim_dead;	
	
	[Space(10)]
	[Header("Extra")]
	
	public Transform weaponPosition_1;
	public Transform weaponPosition_2;
	[Space(10)]
	public Transform weaponPosition_Melee_1;
	public Transform weaponPosition_Melee_2;
}
