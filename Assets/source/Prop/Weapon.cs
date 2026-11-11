using UnityEngine;
using System.Collections;
using BehaviourMachine;

public enum EnumWeaponType {
	Gun,
	Pistol,
	Melee_1,
	Melee_2
}

public class Weapon : MonoBehaviour {
	public string title = "[gun]";
	
	[Header("Graphics")]
	[Space(10)]
	
	[Range(0f, 0.5f)]	
	public float shakeIntencity = 0.1f;
	[Range(0f, 1f)]
	public float shakeTime = 0.1f;
	
	public GameObject DropPrefab;
	public Sprite DropSprite;
	public Sprite UseSprite;
	
	public EnumWeaponType type = EnumWeaponType.Gun;
	
	public Transform MuzzlePoint;
	public GameObject MuzzleFX;
	
	public GameObject Bullet;
	
	public AudioClip[] shotSound;
	
	[Header("Config")]
	[Space(10)]
	
	public int damage = 10;
	public int fireRate = 5;
	
	[Range(0f, 2f)]
	public float soundPower = 1f;
	
	public int ammo = 100;
	
	public float dropVelocity = 5f;
	public float throwVelocity = 50f;
	
	public bool automatic = true;
	
	public GameObject gibPack;
	[HideInInspector]
	public Character owner;
	
	private bool lock_ = false;
	
	private int bulletLayer_;
	
	public float spread = 10f;
	public int numBullets = 1;
	
	public float hitRange = 0.9f;
	public float hitArea = 1f;

	private int _maxAmmo = 0;
	public int maxAmmo {
		get { return _maxAmmo; }
	}
	
	protected float shootCooldown {
		get { return 1f / fireRate; }
	}
	
	protected float lastShotTime;

	void Start () {
		var shadow = gameObject.GetComponent<EZCastShadow>();
		if(shadow != null)
			Destroy(shadow);
			
		gameObject.AddComponent<EZCastShadow>();
		
		_maxAmmo = ammo;
		
		if (MuzzlePoint == null)
			MuzzlePoint = transform;
	}
	
	void Update () {
	}
	
	public void SetBulletLayer(int layer) { 
		bulletLayer_ = layer;
	}
	
	void FireAt(Vector3 pos) {
		if(lock_)
			return;
		
		if(Time.time < lastShotTime + shootCooldown)
			return;
		
		if(!automatic)
			lock_ = true;
			
		var d = pos - transform.position;
		d.z = 0;
		lastShotTime = Time.time;
		
		if (ammo > 0 || maxAmmo <= 0) {
			ammo--;
			SoundManager.PlaySound(shotSound);
			
			if(transform.root.tag == SRTags.Player) {
				CameraController.Shake(shakeIntencity, shakeTime, (transform.position - pos).normalized);
			}
			
			switch(type) {
				case EnumWeaponType.Gun:
				case EnumWeaponType.Pistol:
					for(var i = 0; i < numBullets; i++)				
						BroadcastMessage("DoFire", d.normalized);
				break;
				
				case EnumWeaponType.Melee_1:
				case EnumWeaponType.Melee_2:
					BroadcastMessage("DoHit", false);
				break;
			}
		} else {
			SoundManager.PlaySound(SoundManager.sounds.EmptyClip);
		}
					
	}
	
	void StopFire() {
		lock_ = false;
	}
	
	void DoHit(bool finish) {
		var anim_type = "Punch";
		
		if(type == EnumWeaponType.Melee_1)
			anim_type = "Melee1";
		if(type == EnumWeaponType.Melee_2)
			anim_type = "Melee2";
		
		owner.BroadcastMessage(anim_type, Common.GetMousePosition());
		SoundManager.PlaySound(shotSound);
		
		var point = transform.position - owner.torso.transform.up * hitRange;
		
		//  if(hit_fx != null) {
		//  	SceneController.Spawn(hit_fx, point - GetComponentInChildren<Character>().torso.transform.up * 0.5f, Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward));
		//  }
		
		var AS = GetComponent<AnimatedSprite>();
		if(AS != null) {
			AS.PlayIndex(0, true);
		}
		
		if(finish)
			return;
		
		var drop = Physics2D.OverlapCircle(point, hitArea, SRLayerMask.Enemy);
		if(drop && !drop.gameObject.IsStunned()) {
			drop.BroadcastMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
			drop.GetComponent<Blackboard>().SendEvent("Stunned");
		}
	}
	
	void DoFire(Vector3 d) {
		if(owner.gameObject.layer == SRLayers.Player) {
			Senses.AISound(transform.position, soundPower);
		}
		
		var bullet = SceneController.Spawn(Bullet, MuzzlePoint.transform.position, Quaternion.identity);
		bullet.layer = bulletLayer_;
		
		var component = bullet.GetComponent<Bullet>();
		
		component.HitMark = gibPack;
		component.damage += damage;
		component.direction = d + (Random.insideUnitSphere * spread / 100f);
		
		if (MuzzleFX != null) {
			var muzzle_instance = SceneController.Spawn(
				MuzzleFX, MuzzlePoint.transform.position, 
				MuzzleFX.transform.rotation
			);
			
			muzzle_instance.transform.parent = transform;
		}
	}
}
