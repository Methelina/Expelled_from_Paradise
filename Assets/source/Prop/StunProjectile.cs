using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class StunProjectile : MonoBehaviour {
	public float stunVelocity = 10f;

	void Start () {
	
	}
	
	void FixedUpdate () {
		if(canStun) {
			var overlap = Physics2D.OverlapCircle(transform.position, 0.25f, 1 << LayerMask.NameToLayer("Enemy"));
			
			if(overlap != null && !overlap.gameObject.IsStunned()) {
				
				overlap.BroadcastMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
				overlap.GetComponent<Blackboard>().SendEvent("Stunned");
				
				GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity * 0.25f;
				enabled = false;
				Destroy(this);
			}
		}
		
	}
	
	public bool canStun {
		get {
			return GetComponent<Rigidbody2D>().velocity.magnitude > stunVelocity;
		}
	}
	
	void OnDrawGizmos() {
		Gizmos.color = new Color(1f,1f,1f,0.1f);
		Gizmos.DrawSphere(transform.position, 0.25f);
	}
}
