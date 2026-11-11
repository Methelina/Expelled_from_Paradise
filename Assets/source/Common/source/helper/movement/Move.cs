using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Makes stuff move on a fixed velocity with no awarness of the situation.
/// </summary>

public class Move : MonoBehaviour {
	public Vector2 Direction;
	public Vector2 Velocity;
	
	public bool Wander;
	[Range(100, 10000)]
	public int Interval = 100;
	
	[Range(0, 5)]
	public float Range = 1;
	
	private Vector2 CurrentVelocity;
	private Vector2 InitialPosition;

	void Start () {
		if(Wander)
			StartCoroutine(Wandering());
		
		CurrentVelocity = Direction;
		CurrentVelocity.x *= Velocity.x;
		CurrentVelocity.y *= Velocity.y;
		
		InitialPosition = transform.position;
	}
	
	void FixedUpdate () {
		transform.position = transform.position + (Vector3)CurrentVelocity * Time.fixedDeltaTime;
	}
	
	IEnumerator Wandering() {
		while(true) {
			int time = Random.Range(100, Interval);
			
			if(Vector2.Distance(InitialPosition, (Vector2)transform.position) < Range) {
				CurrentVelocity = Random.insideUnitCircle;
				CurrentVelocity.x *= Velocity.x;
				CurrentVelocity.y *= Velocity.y;
			} else {
				CurrentVelocity = InitialPosition - (Vector2)transform.position;
			}
			
			for(int i = 0;i < time; i += (int)(Time.fixedDeltaTime * 1000)) {
				yield return new WaitForFixedUpdate();
			}
			
			yield return new WaitForFixedUpdate();
		}
	}
	
	#if UNITY_EDITOR
	void OnDrawGizmosSelected() {
		if (!Wander)
			return;
		
		Gizmos.color = new Color(1f,1f,1f,0.25f);
		Gizmos.DrawSphere(InitialPosition, Range);
	}
	#endif
}
