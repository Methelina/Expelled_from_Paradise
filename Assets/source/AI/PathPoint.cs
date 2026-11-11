using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
#endif
public class PathPoint : MonoBehaviour {
	public bool snap;
	
	[Range(0f, 10f)]
	public float arriveDistance = 0.5f;
	public PathPoint connect;
	public PathPoint parent;
	public Color color = Color.blue;
	
	void Start () {
	
	}
	
	void Update () {
		if(snap)
			transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
	}
	
	void NPCArrive(GameObject npc) {
		//  print(npc);
	}
	
	public Vector3 position {
		get { return transform.position; }
	}
	
	void OnDrawGizmos() {
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, 0.25f);
		
		if(connect != null) {
			float totalFOV = 25f;
			float rayRange = 0.5f;
			float halfFOV = totalFOV / 2.0f;
			Vector3 direction = connect.transform.position - transform.position;
			
			Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
			Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);
			
			Vector3 leftRayDirection = leftRayRotation * -direction.normalized;
			Vector3 rightRayDirection = rightRayRotation * -direction.normalized;
			
			Gizmos.DrawRay(connect.transform.position - direction.normalized / 2f, leftRayDirection * rayRange);
			Gizmos.DrawRay(connect.transform.position - direction.normalized / 2f, rightRayDirection * rayRange);
			
			Gizmos.DrawRay(connect.transform.position - direction + direction.normalized, leftRayDirection * rayRange);
			Gizmos.DrawRay(connect.transform.position - direction + direction.normalized, rightRayDirection * rayRange);
			
			if(parent != null) {
				var direction2 = parent.transform.position - transform.position;
				
				Vector3 leftRayDirection2 = leftRayRotation * -direction2.normalized;
				Vector3 rightRayDirection2 = rightRayRotation * -direction2.normalized;
				
				Gizmos.DrawRay(parent.transform.position - direction2.normalized * (direction2.magnitude / 2f - 0.25f), leftRayDirection2 * rayRange);
				Gizmos.DrawRay(parent.transform.position - direction2.normalized * (direction2.magnitude / 2f - 0.25f), rightRayDirection2 * rayRange);
			}
				
			Gizmos.DrawLine(transform.position, connect.transform.position);
		}
		
		Gizmos.DrawWireSphere(transform.position, arriveDistance);
	}
}
