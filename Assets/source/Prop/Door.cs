using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class Door : MonoBehaviour {
	public bool isOpen;
	
	public AudioClip sound;
	public float cooldown = 0.25f;
	
	[Range(0f, 1f)]
	public float lerp = .15f;
	
	public float angle = 160f;
		
	private float nextToggle = 0f;
	
	private Vector3 pos;
	private Vector3 direction;
	
	public bool lock_ = false;
	
	private float targetAngle;
	private float angleClosed;
	private float angleOpened;
	private float angleOpened2;
	
	void Start () {
		pos = transform.position;
		direction = transform.right;
		
		Quaternion leftRayRotation = Quaternion.AngleAxis(-angle / 2.0f, Vector3.forward);
		Quaternion rightRayRotation = Quaternion.AngleAxis(angle / 2.0f, Vector3.forward);
		
		Vector3 leftRayDirection = leftRayRotation * direction;
		Vector3 rightRayDirection = rightRayRotation * direction;
		Vector3 closedDirection = direction;
		
		angleClosed  = Mathf.Atan2(closedDirection.y, closedDirection.x) * Mathf.Rad2Deg;
		angleOpened  = Mathf.Atan2(leftRayDirection.y, leftRayDirection.x) * Mathf.Rad2Deg;
		angleOpened2 = Mathf.Atan2(rightRayDirection.y, rightRayDirection.x) * Mathf.Rad2Deg;
	}
	
	void Update () {
		transform.position = pos;
	}
	
	void FixedUpdate() {
		var rb2d = GetComponent<Rigidbody2D>();
		
		GetComponent<Collider2D>().enabled = !lock_;
		
		if(lock_) {
			rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, targetAngle, lerp));
			
			if(Mathf.Abs(Mathf.DeltaAngle(rb2d.rotation, targetAngle)) < 1f)
				lock_ = false;
		} else {
			
		}
	}
	
	void TryStun(bool inside) {
		Vector3 position;
		
		if(inside)
			position = transform.position + transform.up * 0.5f + transform.right * transform.localScale.x;
		else
			position = transform.position - transform.up * 0.5f + transform.right * transform.localScale.x;
		
		var overlap = Physics2D.OverlapCircle(position, 1.5f, 1 << LayerMask.NameToLayer("Enemy"));
		
		if(overlap != null) {
			overlap.BroadcastMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
			overlap.GetComponent<Blackboard>().SendEvent("Stunned");
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) { 
		var stun = coll.gameObject.GetComponent<StunProjectile>();
		if(stun != null && !stun.canStun) {
			return;
		}
		
		lock_ = true;
		
		if(isOpen && coll.transform.tag == SRTags.Bullet) 
			return;
		
		if(coll.gameObject.layer == SRLayers.Weapon) {
			if(coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 10f) {
				return;
			}
		}
		
		Quaternion leftRayRotation = Quaternion.AngleAxis(-angle / 2.0f, Vector3.forward);
		Quaternion rightRayRotation = Quaternion.AngleAxis(angle / 2.0f, Vector3.forward);
		
		Vector3 leftRayDirection = leftRayRotation * direction;
		Vector3 rightRayDirection = rightRayRotation * direction;
		
		var pos1 = transform.position + leftRayDirection * 2f;
		var pos2 = transform.position + rightRayDirection * 2f;
		
		var inside = false;
		if(Vector3.Distance(coll.transform.position, pos1) < Vector3.Distance(coll.transform.position, pos2)) {
			targetAngle = transform.localScale.x > 0 ? angleOpened2 : angleOpened;
			inside = true;
		} else {
			targetAngle = transform.localScale.x > 0 ? angleOpened : angleOpened2;
		}
		
		if(coll.gameObject.layer == SRLayers.Player || coll.gameObject.layer == SRLayers.Bullets)
			TryStun(inside);
		
		Toggle();
	}
	
	public void Toggle () {
		if(Time.time < nextToggle)
			return;
		
		SoundManager.PlaySound(sound);			
		nextToggle = Time.time + cooldown;
		
		isOpen = !isOpen;
		targetAngle = isOpen ? targetAngle : angleClosed;
	}
	
	public void OnDrawGizmos() {
		float totalFOV = angle;
		float rayRange = 2f;
		float halfFOV = totalFOV / 2.0f;
		
		var color = Gizmos.color;
		color.a = 0.2f;
		Gizmos.color = color;
		
		Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
		Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);
		
		if(!Application.isPlaying)
			direction = transform.right;
		
		Vector3 leftRayDirection = leftRayRotation * direction * transform.localScale.x;
		Vector3 rightRayDirection = rightRayRotation * direction * transform.localScale.x;
		
		Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
		Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
		Gizmos.DrawRay(transform.position, direction * rayRange * transform.localScale.x);
		
		Gizmos.DrawCube(transform.position + transform.up * 0.5f + transform.right * transform.localScale.x, Vector3.one * 1.5f);
		Gizmos.DrawCube(transform.position - transform.up * 0.5f + transform.right * transform.localScale.x, Vector3.one * 1.5f);
		
	}
	
}
