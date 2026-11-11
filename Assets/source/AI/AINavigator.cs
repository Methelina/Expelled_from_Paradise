using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class AINavigator : MonoBehaviour {
	public bool drawDebug = true;
	
	[Space(10)]
	
	public float speed = 4f;
	public float size = 0.5f;
	public float pathCooldown = 0.5f;
	
	public Vector3 targetPosition;
	
	public LayerMask obstacles;
	
	private float currentSpeed = 0f;
	private float speedLerp = 0.1f;
	private float stopDistance = 1f;
	
	private Seeker seeker_;
	private Vector3 goPos;
	private float nextPath = 0f;
	private float maxDistance = 0.1f;
	private float reactDistance = 0.1f;
	private Vector3 rawTargetPosition;
	private bool invalidatePath;
	private bool stunned = false;
	
	void Start() {
		seeker_ = GetComponent<Seeker>();
	}
	
	void Stunned() {
		stunned = true;
	}
	
	void FixedUpdate() {
		if(stunned)
			return;
		
		FollowTrail();
		
		if(targetPosition == Vector3.zero) {
			currentSpeed = 0f;
			return;
		}
		
		if(goPos != Vector3.zero) {
			var distance = Vector3.Distance(transform.position, targetPosition);
			var isStopDistance = (distance - maxDistance) <= stopDistance;
			
			if(isStopDistance)
				currentSpeed = Mathf.Lerp(currentSpeed, 0f, currentSpeed * Time.fixedDeltaTime);
			else
				currentSpeed = Mathf.Lerp(currentSpeed, speed, speedLerp);
			
			if(distance <= maxDistance) {
				BroadcastMessage("UpdateMovement", Vector3.zero);
				transform.rotation = Quaternion.Lerp(transform.rotation, Common.RotateTowardsV3(transform, rawTargetPosition, 90f), 0.1f);
			} else {
				var delta = goPos - transform.position;
				transform.position = Vector3.MoveTowards(transform.position, goPos, currentSpeed * Time.fixedDeltaTime);
				
				BroadcastMessage("Run", delta);
				transform.rotation = Quaternion.Lerp(transform.rotation, Common.RotateTowardsV3(transform, goPos, 90f), 0.1f);
			}
		}
		
		if(invalidatePath) {
			var raydelta = targetPosition - transform.position;
			var rayBlock = Physics2D.CircleCast(transform.position, size, raydelta, raydelta.magnitude, obstacles);
			
			if(goPos == Vector3.zero || rayBlock.transform != null) {
				
				if(Time.time > nextPath) {
					nextPath = Time.time + pathCooldown;
					invalidatePath = false;
					seeker_.StartPath(transform.position, targetPosition, onPathRecieved);
				}
			}
		}
	}
	
	Path p;
	int i = 0;
	void FollowTrail() {
		if(p == null)
			return;
		
		if(i < p.vectorPath.Count) {
			goPos = p.vectorPath[i];
			
			var raydelta = goPos - transform.position;
			var rayBlock = Physics2D.CircleCast(transform.position, size, raydelta, raydelta.magnitude, obstacles);
			
			if(rayBlock.transform != null && Vector3.Distance(transform.position, goPos) > 0.1f)
				return;
			
			i++;
		} else {
			goPos = rawTargetPosition;
		}
	}
	
	public void runTo(Vector3 pos, float distance = 0.1f, float distanceReact = 0.1f) {
		stunned = false;
		
		maxDistance = distance;
		reactDistance = distanceReact;
		rawTargetPosition = pos;
		
		if(targetPosition != Vector3.zero && Vector3.Distance(transform.position, rawTargetPosition) < reactDistance)
			return;
		if(pos == targetPosition)
			return;
		
		targetPosition = pos;
		invalidatePath = true;
		
		var delta = targetPosition - transform.position;
		var rayBlock = Physics2D.CircleCast(transform.position, size, delta, delta.magnitude, obstacles);
		
		if(rayBlock.transform == null) {
			goPos = targetPosition;
			p = null;
		} else {
			goPos = p == null ? Vector3.zero : goPos;
		}
	}
	
	public void stop() {
		targetPosition = Vector3.zero;
		goPos = Vector3.zero;
		rawTargetPosition = Vector3.zero;
		BroadcastMessage("UpdateMovement", Vector3.zero);
	}
	
	void onPathRecieved(Path path) {
		p = path;
		i = 2;
	}
	
	public void OnDrawGizmos() {
		if(!drawDebug)
			return;
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, size / 2f);
		
		var color = Color.red;
		color.a = 1f;
		Gizmos.color = color;
		
		if(rawTargetPosition != Vector3.zero) {
			Gizmos.DrawLine(transform.position, goPos);
			Gizmos.DrawSphere(goPos, 0.1f);
			
			color = Color.red;
			color.a = 0.15f;
			Gizmos.color = color;
			Gizmos.DrawWireSphere(rawTargetPosition, maxDistance);
			
			color = Color.black;
			color.a = 0.15f;
			Gizmos.color = color;
			Gizmos.DrawWireSphere(rawTargetPosition, reactDistance);
		}
		
		if(p != null) {
			Gizmos.color = new Color(1f, 0.5f, 0f);
			for(var i = 1; i < p.vectorPath.Count; i++) {
				Gizmos.DrawLine(p.vectorPath[i-1], p.vectorPath[i]);
			}
		}
	}
}
