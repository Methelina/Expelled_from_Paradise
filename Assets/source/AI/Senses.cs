using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Senses : MonoBehaviour {
	public static List<Senses> all = new List<Senses>();
	
	public bool drawDebug = true;
	
	[Range(0f, 5f)]
	public float delaySenses = 0.5f;
	[Range(0f, 5f)]
	public float delayVariation = 0.2f;
	
	[Space(10)]
	[Header("Memory")]
	public float chaseMemory = 5f;
	public float sightMemory = 10f;
	public float soundMemory = 30f;
	
	[Space(10)]
	[Header("Sight")]
	public bool see = true;
	[Range(0f, 360f)]
	public float FOV = 70f;
	public float sightRange = 10f;
	public LayerMask seeLayers;
	[Range(0f, 1f)]
	public float sightTick = 0.2f;
	private float forgetSight_ = 0f;
	private float forgetChase_ = 0f;
	
	[Space(10)]
	[Header("Hearing")]
	[Space(10)]
	
	public bool hear = false;
	public float hearRange = 10f;
	[Range(0f, 1f)]
	public float hearTick = 0.2f;
	private float forgetHear_ = 0f;
	
	
	[Space(10)]
	[Header("Feeling")]
	[Space(10)]
	
	public bool needsGun = true;
	public bool feel = false;
	public float feelRange = 3f;
	public float feelTick = 0.2f;
	
	[Range(0f, 0.5f)]
	public float triggerReaction = 0.2f;
	[Range(0f, 0.25f)]
	public float triggerReactionVariation = 0.05f;
	
	private RaycastHit2D _lineOfSight_;
	
	public static void AISound(Vector3 position, float power) {
		for(var i = 0; i < all.Count; i++)
			all[i].FeedSound(position, power);
	}
	
	void OnDestroy () {
		all.Remove(this);
	}
	
	void Start () {
		all.Add(this);
	}
	
	void OnEnable() {
		StartCoroutine(BeginSenses());
	}
	
	IEnumerator BeginSenses() {
		yield return new WaitForSeconds(
			delaySenses + Random.Range(-delayVariation, delayVariation)
		);
		
		StartCoroutine(UpdateHearing());
		StartCoroutine(UpdateSight());
		StartCoroutine(UpdateFeelings());
	}
	
	void FixedUpdate () {
		if(Time.time > forgetSight_) {
			seeTarget_ = Vector3.zero;
		}
		if(Time.time > forgetHear_) {
			hearTarget_ = Vector3.zero;
		}
		
		if(Time.time > forgetChase_) {
			chaseTarget_ = null;
		} else if(chaseTarget != null) {
			seeTarget_ = chaseTarget_.position;			
			forgetSight_ = Time.time + sightMemory;
		}
	}
	
	public Transform findWeapon() {
		if(gameObject.IsStunned()) return null;
		
		var guns = Physics2D.OverlapCircleAll(transform.position, 10f, SRLayerMask.Weapon);
		
		float min_distance = float.MaxValue;
		Transform best_weapon = null;
		for(var i = 0; i < guns.Length;i++) {			
			var candidate = Physics2D.Linecast(transform.position, guns[i].transform.position, SRLayerMask.Environment).transform;
			
			if (candidate == null) {
				var distance = Vector3.Distance(transform.position, guns[i].transform.position);
				if(min_distance > distance) {
					min_distance = distance;
					best_weapon = guns[i].transform;
				}
			}
		}
		
		return best_weapon;
	}
	
	private bool canSeeTarget_;
	public bool canSeeTarget {
		get { return canSeeTarget_; }
	}
	
	private Transform chaseTarget_;
	public Transform chaseTarget {
		get { return chaseTarget_ != null && chaseTarget_.gameObject.activeSelf ? chaseTarget_ : null; }
	}
	
	private Vector3 seeTarget_;
	public Vector3 seeTarget {
		get { return seeTarget_; }
	}
	
	private List<Vector3> sounds_ = new List<Vector3>();
	private Vector3 hearTarget_;
	public Vector3 hearTarget {
		get { return hearTarget_; }
	}
	
	IEnumerator UpdateFeelings() {
		while(true) {
			if(!feel) yield return new WaitForFixedUpdate();
			
			if(needsGun && GetComponentInChildren<Weapon>() == null) {
				if(findWeapon() != null)
					GetComponent<Blackboard>().SendEvent("NoWeapon");
			}
			
			yield return new WaitForSeconds(feelTick);
		}
	}
	
	IEnumerator UpdateSight() {
		while(true) {
			if(!see) yield return new WaitForFixedUpdate();
			
			canSeeTarget_ = false;
			var inSight = Physics2D.OverlapCircleAll(transform.position, sightRange, seeLayers.value);
			for(var i = 0; i < inSight.Length; i++) {
				var target = inSight[i].transform;
				
				var direction = target.position - transform.position;
				var anglePlayer = Vector3.Angle(direction.normalized, -transform.up);
				
				if(anglePlayer < FOV / 2f || (feel && direction.magnitude < feelRange)) {
					_lineOfSight_ = Physics2D.Raycast(transform.position, direction, sightRange, ~(1 << gameObject.layer));
					
					if (_lineOfSight_.transform == target) {
						seeTarget_ = target.transform.position;
						chaseTarget_ = target.transform;
						hearTarget_ = Vector3.zero;
						
						forgetChase_ = Time.time + chaseMemory;						
						forgetSight_ = Time.time + sightMemory;
						
						canSeeTarget_ = true;
					}
				}
			}
			
			yield return new WaitForSeconds(sightTick);
		}
	}
	
	public void forget() {
		seeTarget_ = Vector3.zero;
		hearTarget_ = Vector3.zero;
		chaseTarget_ = null;
	}
	
	IEnumerator UpdateHearing() {
		while(true) {
			if(!hear) yield return new WaitForFixedUpdate();
			
			if(sounds_.Count > 0)
				hearTarget_ = Vector3.zero;
			
			var pos = transform.position;			
			while(sounds_.Count > 0) {
				var sound = sounds_[0];
				if(hearTarget_ == Vector3.zero || 
				   Vector3.Distance(pos, sound) < Vector3.Distance(pos, hearTarget_)) {
					hearTarget_ = sound;
					seeTarget_ = Vector3.zero;
					forgetHear_ = Time.time + soundMemory;
				}
				
				sounds_.RemoveAt(0);
			}
			
			yield return new WaitForSeconds(hearTick);
		}
	}
	
	void FeedSound(Vector3 point, float power) {
		if(Vector3.Distance(transform.position, point) > hearRange * power)
			return;
		sounds_.Add(point);
	}
	
	void OnDrawGizmos() {
		if(!drawDebug)
			return;
		
		Gizmos.color = seeTarget_ != Vector3.zero ? Color.red : Color.white;
		
		float totalFOV = FOV;
		float rayRange = sightRange;
		float halfFOV = totalFOV / 2.0f;
		
		Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
		Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);
		
		Vector3 leftRayDirection = leftRayRotation * -transform.up;
		Vector3 rightRayDirection = rightRayRotation * -transform.up;
		
		if(see) {
			Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
			Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
		}
		
		Color color;
		
		if(hear) {
			color = Color.magenta;
			color.a = 0.25f;
			Gizmos.color = color;
			Gizmos.DrawWireSphere(transform.position, hearRange);
		}
		
		if(feel) {
			color = Color.red;
			color.a = 0.25f;
			Gizmos.color = color;
			Gizmos.DrawSphere(transform.position, feelRange);
		}
		
		color = Color.cyan;
		
		if(_lineOfSight_.transform != null) {
			Gizmos.DrawLine(transform.position, _lineOfSight_.point);
		}
		
		color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position - transform.up * 10f);
	}
}
