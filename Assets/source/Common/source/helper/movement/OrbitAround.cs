using UnityEngine;
using System.Collections;

public class OrbitAround : MonoBehaviour {
	public Transform Target;
	public float Radius;
	public float Speed = 10f;
	
	private float StartAngle = 0;

	void Start () {
		if (Radius == 0) {
			Radius = Vector3.Distance(Target.position, transform.position);
		}
		
		var d = Target.position - transform.position;
		StartAngle = -Mathf.Atan2(d.y, d.x);
	}
	
	void FixedUpdate () {
		var orbit = Target.transform.position + new Vector3(
			Mathf.Sin(StartAngle + Time.time * Speed / 10f) * Radius,
			Mathf.Cos(StartAngle + Time.time * Speed / 10f) * Radius
		);
		
		transform.position = orbit;
	}
}
