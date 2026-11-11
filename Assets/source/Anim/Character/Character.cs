using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public CharacterHead head;
	public CharacterTorso torso;
	public CharacterLegs legs; 
	
	public string bulletLayer;
	
	private Weapon weapon_;
	
	private Vector3 point_;
	private bool alive_ = true;
	
	private bool hit_ = false;
	
	void Start () {
		if(torso != null)
			torso.Play(torso.anim_stay);
		if(legs != null)
			legs.Play(legs.anim_stay);	
	}
	
	public void Stay() {
		torso.transform.rotation = Quaternion.identity;
		legs.transform.rotation = Quaternion.identity;	
		transform.rotation = Quaternion.identity;
	}
	
	public void Face(Vector3 point) {
		if(torso != null && legs != null) {
			torso.transform.rotation = Common.GetRotationTowards(transform, point, 90f);
			legs.transform.rotation = Common.GetRotationTowards(transform, point, 90f);
		}
	}
	
	public void Run(Vector3 tv) {
		if(head != null)
			head.Play(head.anim_alive);
		
		if(legs != null) {
			if(tv.magnitude > 0) {
				legs.Play(legs.anim_running);
			} else {
				legs.Play(legs.anim_stay);
			}
		}
		
		if(torso != null && !hit_) {
			if(tv.magnitude > 0) {
				if(weapon_ == null) torso.Play(torso.anim_running);
			} else {
				if(weapon_ == null) torso.Play(torso.anim_stay);
			}
		}
	}
	
	public void UpdateMovement(Vector3 tv) {
		if(head != null)
			head.Play(head.anim_alive);
		
		if(legs != null) {
			if(tv.magnitude > 0) {
				legs.Play(legs.anim_running);
				legs.transform.rotation = Common.RotateTowardsV3(legs.transform, legs.transform.position + tv, 90f);
			} else {
				legs.Play(legs.anim_stay);
				legs.transform.rotation = torso.transform.rotation;
			}
		}
		
		if(torso != null && !hit_) {
			if(tv.magnitude > 0) {
				torso.transform.rotation = Common.GetRotationTowards(transform, transform.position + tv, 90f);
				if(weapon_ == null) torso.Play(torso.anim_running);
			} else {
				if(weapon_ == null) torso.Play(torso.anim_stay);
			}
		}
	}
	
	public void Look(Vector3 point) {
		if(torso) {
			torso.transform.rotation = Common.GetRotationTowards(transform, point, 90f);
		}
	}
	
	public void TakeWeapon(Weapon wp) {
		weapon_ = wp;
		
		switch(weapon_.type) {		
			case EnumWeaponType.Gun:
				weapon_.transform.parent = torso.weaponPosition_1;
			break;
			
			case EnumWeaponType.Pistol:
				weapon_.transform.parent = torso.weaponPosition_2;
			break;
			
			case EnumWeaponType.Melee_1:
				weapon_.transform.parent = torso.weaponPosition_Melee_1;
			break;
			
			case EnumWeaponType.Melee_2:
				weapon_.transform.parent = torso.weaponPosition_Melee_2;
			break;
		}
		
		weapon_.transform.localPosition = Vector3.zero;
		weapon_.transform.rotation = Quaternion.identity;
		weapon_.transform.localRotation = Quaternion.identity;
		
		weapon_.GetComponent<SpriteRenderer>().sprite = weapon_.UseSprite;
		weapon_.GetComponent<SpriteRenderer>().sortingOrder = 20;
		weapon_.SetBulletLayer(LayerMask.NameToLayer(bulletLayer));
		weapon_.owner = this;
	}
	
	public void DropWeapon(bool is_throw = true) {
		var drop = SceneController.Spawn(weapon_.DropPrefab, transform.position, Quaternion.identity);
		
		if(is_throw) {
			drop.GetComponent<Rigidbody2D>().velocity = (Common.GetMousePosition() - transform.position).normalized * weapon_.throwVelocity;
			drop.GetComponent<Rigidbody2D>().angularVelocity = 900;
		} else {
			drop.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * weapon_.dropVelocity;
			drop.GetComponent<Rigidbody2D>().angularVelocity = 60;
		}
		
		weapon_.transform.parent = drop.transform;
		weapon_.transform.localPosition = Vector3.zero;
		weapon_.GetComponent<SpriteRenderer>().sprite = weapon_.DropSprite;
		weapon_.GetComponent<SpriteRenderer>().sortingOrder = 0;
		weapon_.owner = null;
		
		if(legs != null) 
			legs.Play(legs.anim_stay, true);
		if(torso != null && !hit_) 
			torso.Play(torso.anim_stay, true);
		
		weapon_ = null;
	}
	
	public void Punch(Vector3 pos) {
		if(weapon_ != null || hit_)
			return;
		
		hit_ = true;
		torso.Play(torso.anim_punch, true, (AnimSequence aseq) => {
			hit_ = false;
		});
	}
	
	public void Melee1(Vector3 pos) {
		if(hit_)
			return;
		
		hit_ = true;
		torso.Play(torso.anim_melee_1, true, (AnimSequence aseq) => {
			hit_ = false;
		});
	}
	
	public void Melee2(Vector3 pos) {
		if(hit_)
			return;
		
		hit_ = true;
		torso.Play(torso.anim_melee_2, true, (AnimSequence aseq) => {
			hit_ = false;
		});
	}
	
	public void OnPunched() {
		SoundManager.PlaySound(SoundManager.sounds.MeleeHitSound);
	}
	
	public void Stunned() {
		if(weapon_ != null)		
			DropWeapon(false);
			
		if(torso != null)
			torso.Play(torso.anim_dead);
		if(legs != null)
			legs.Play(legs.anim_dead);
		if(head != null)
			head.Play(head.anim_dead);
		
		if(legs != null && torso != null)
			legs.transform.rotation = torso.transform.rotation;
	}
	
	public void OnKilled() {
		SoundManager.PlaySound(SoundManager.sounds.Thump, true, 0.6f);
		
		if(GetComponentInParent<Destroyable>().corpsePrefab != null) {
			if(weapon_ != null)		
				DropWeapon(false);
			
			return;
		}
		
		transform.parent = SceneController.dynamicContainer;
		
		Stunned();
		
		alive_ = false;
	}
	
	void Update () {
		if(!alive_ || hit_)
			return;
		
		if(weapon_ != null) {
			switch(weapon_.type) {
				case EnumWeaponType.Gun:
					torso.Play(torso.anim_weapon_2);
				break;
				
				case EnumWeaponType.Pistol:
					torso.Play(torso.anim_weapon_1);
				break;
				
				case EnumWeaponType.Melee_1:
					torso.PlayFrame(torso.anim_melee_1, 0);
				break;
				
				case EnumWeaponType.Melee_2:
					torso.PlayFrame(torso.anim_melee_2, 0);
				break;
			}
		}
	}
}
