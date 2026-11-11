using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5f;
	
	public float pickupRange = 1f;
	[Range(0f, 1f)]
	
	public float moveLerp = 0.3f;
	
	public float hitRange = 0.4f;
	public float hitArea = 0.9f;
	public GameObject hit_fx;
	
	private static int LEFT = 2;
	private static int RIGHT = 8;
	private static int UP = 16;
	private static int DOWN = 32;
	private int collisionSides = 0;
	
	void Start () {
	
	}
	
	public Weapon Weapon {
		get { return GetComponentInChildren<Weapon>(); }
	}
	
	void Update() {
		if(killLock != null) 
			return;
		
		if (Input.GetMouseButtonDown(1))
			DropOrPickupWeapon();
	}
	
	private Vector3 translate = Vector3.zero;
	private float nextPunch = 0f;
	Transform killLock = null;
	private float freeLock = 0f;
	
	void FixedUpdate () {
		if(killLock != null) {
			transform.position = killLock.position;
			BroadcastMessage("Look", killLock.position + killLock.up);
			
			if (Time.time > nextPunch) {
				if(!killLock.gameObject.IsAlive())
					killLock = null;
				
				if (Input.GetKeyDown(KeyCode.Space) && Time.time > freeLock) {
					killLock = null;
					freeLock = Time.time + 0.2f;
				}
				
				if(killLock && Input.GetMouseButtonDown(0)) {
					nextPunch = Time.time + 0.2f;
					
					if(Weapon == null)
						DoPunch(true);
					else
						BroadcastMessage("DoHit", true);	
					
					SceneController.Spawn(SRResources.gib_pack_small, transform.position);
					
					killLock.BroadcastMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
					killLock.BroadcastMessage("ApplyDamage", Weapon != null ? Weapon.damage : 2, SendMessageOptions.DontRequireReceiver);
				}
			}
			
			return;
		}
		
		var tv = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		
		translate = Vector3.Lerp(translate, tv, moveLerp);
		
		transform.position = transform.position + translate * Time.fixedDeltaTime * moveSpeed;
		
		if (Time.time > nextPunch && Input.GetMouseButtonDown(0)) {
			nextPunch = Time.time + 0.2f;
			if(Weapon == null) {
				DoPunch();
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > freeLock) { 
			var killo = Physics2D.OverlapCircle(transform.position, 1f, SRLayerMask.Enemy);
			
			if(killo != null) {
				var stun = killo.GetComponent<AIStunned>();
				if(stun != null && stun.enabled) {
					killLock = killo.transform;
					freeLock = Time.time + 0.2f;
					
					if (Weapon != null) {
						switch(Weapon.type) {
							case EnumWeaponType.Gun:
							case EnumWeaponType.Pistol:
								BroadcastMessage("DropWeapon", true);
							break;
						}
					}
				}
			}
		}
		
		if (Input.GetMouseButton(0)) {
			BroadcastMessage("FireAt", Common.GetMousePosition(), SendMessageOptions.DontRequireReceiver);
		} else if(Input.GetMouseButtonUp(0)) {
			BroadcastMessage("StopFire", SendMessageOptions.DontRequireReceiver);
		}
		
		BroadcastMessage("UpdateMovement", tv);
		BroadcastMessage("Look", Common.GetMousePosition());
	}
	
	void DoPunch(bool finish = false) {
		BroadcastMessage("Punch", Common.GetMousePosition(), SendMessageOptions.DontRequireReceiver);
		SoundManager.PlaySound(SoundManager.sounds.MeleeSwingSound);
		
		var point = transform.position - GetComponentInChildren<Character>().torso.transform.up * hitRange;
		
		if(hit_fx != null) {
			SceneController.Spawn(hit_fx, point - GetComponentInChildren<Character>().torso.transform.up * 0.5f, Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward));
		}
		
		if(finish)
			return;
		
		var drop = Physics2D.OverlapCircle(point, hitArea, SRLayerMask.Enemy);
		if(drop && !drop.gameObject.IsStunned()) {
			drop.BroadcastMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
			drop.GetComponent<Blackboard>().SendEvent("Stunned");
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		collisionSides = 1;
	
		for (var i = 0;i < collision.contacts.Length;i++) {
			var contact = collision.contacts[i];
			
			if (contact.normal.x > 0)
				collisionSides |= RIGHT;
			if (contact.normal.x < 0)
				collisionSides |= LEFT;
			if (contact.normal.y > 0)
				collisionSides |= UP;
			if (contact.normal.y < 0)
				collisionSides |= DOWN;
		}
	}
	
	void DropOrPickupWeapon () {
		var item = Physics2D.OverlapCircle(transform.position, pickupRange, SRLayerMask.Trigger);
		if (item != null && item.GetComponent<PickupItem>() != null) {
			SoundManager.PlaySound(SoundManager.sounds.PickUp);
			item.BroadcastMessage("PickUp", SendMessageOptions.DontRequireReceiver);
			Destroy(item.gameObject);
			return;
		}
		
		if (Weapon != null) {
			BroadcastMessage("DropWeapon", true);
			SoundManager.PlaySound(SoundManager.sounds.DropWeapon);
		} else {
			var drop = Physics2D.OverlapCircle(transform.position, pickupRange, SRLayerMask.Weapon);
			
			if (drop != null) {
				var weapon = drop.gameObject.GetComponentInChildren<Weapon>();
		
				BroadcastMessage("TakeWeapon", weapon);
				SoundManager.PlaySound(SoundManager.sounds.PickUp);
				
				Destroy(drop.gameObject);
			}
			
		}
	}
	
	void OnKilled () {
	}
	
	void OnDrawGizmosSelected() {
		var color = Color.magenta;
		color.a = 0.15f;
		
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, pickupRange);
		
		color = Color.black;
		color.a = 0.5f;
	}
	
	void OnDrawGizmos() {
		var color = Color.magenta;
		color.a = 0.25f;
		
		Gizmos.color = color;
		
		Gizmos.DrawSphere(transform.position - GetComponentInChildren<Character>().torso.transform.up * hitRange, hitArea);
	}
	
}
