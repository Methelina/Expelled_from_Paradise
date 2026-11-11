using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AIWander : MonoBehaviour {
	public float wanderTime = 30f;
	
	Vector3 wanderPoint = Vector3.zero;
	int orientation = 0;
	int failtures = 0;
	
	int increment = 0;
	
	private bool isLookAround = false;
	private Vector3 lookAroundDirection;
	
	void Start () {
		orientation = Random.Range(0, 4);
	}
	
	void OnEnable () {
		increment = ChanceUtil.Roll(0.5f) ? -1 : 1;
		changeWanderPoint();
		
		Invoke("Finish", wanderTime);
	}
	
	void Finish() {
		GetComponent<Blackboard>().SendEvent("FINISHED");
	}
	
	void Update () {
		var senses_ = GetComponent<Senses>();
		if(senses_.seeTarget != Vector3.zero || senses_.hearTarget != Vector3.zero) {
			GetComponent<Blackboard>().SendEvent("EnemyInSight");
		}
		
		if(isLookAround) {
			GetComponent<AINavigator>().stop();
			transform.rotation = Quaternion.Lerp(transform.rotation, Common.RotateTowardsV3(transform, transform.position + lookAroundDirection, 90f), 0.15f);
		} else { 
			GetComponent<AINavigator>().runTo(wanderPoint, 0f, 0f);
			
			if(Vector3.Distance(transform.position, wanderPoint) <= 0.6f)
				changeWanderPoint();	
		}
	}
	
	void changeWanderPoint () {
		if(isLookAround)
			return;
		
		Transform ray_hit = null;
		
		while(ray_hit == null) {
			orientation += increment;
			
			var orientangle = orientation * Mathf.PI / 2f;
			var direction = new Vector3(Mathf.Sin(orientangle), Mathf.Cos(orientangle));
			var ray = Physics2D.Raycast(transform.position, direction, 100f, 1 << LayerMask.NameToLayer("Environment"));
			ray_hit = ray.transform;
			
			if(ray_hit == null) {
				failtures++;
			} else {
				failtures = 0;
				wanderPoint = (Vector3)ray.point - direction / 2f;
				break;
			}
			
			if(failtures > 0) {
				direction = direction + (Vector3)Random.insideUnitCircle * 0.25f;
				if(Physics2D.Linecast(transform.position, transform.position + direction, 1 << LayerMask.NameToLayer("Environment")).transform == null)
					wanderPoint = transform.position + direction * 5f;
				
				StartCoroutine(LookAround());
				break;
			}
		}
	}
	
	IEnumerator LookAround() {
		isLookAround = true;
		
		var c = Random.Range(1, 5);
		while(c > 0) {
			orientation++;
			c--;
			
			var orientangle = orientation * Mathf.PI / 2f;
			lookAroundDirection = new Vector3(Mathf.Sin(orientangle), Mathf.Cos(orientangle));
			
			yield return new WaitForSeconds(0.7f);
		}
		
		isLookAround = false;
		wanderPoint = transform.position + lookAroundDirection * 5f + (Vector3)Random.insideUnitCircle * 0.25f;
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(wanderPoint, 0.15f);
		
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + lookAroundDirection * 10f);
	}
	
}
