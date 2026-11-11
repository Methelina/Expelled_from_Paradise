using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public Transform target;
	[Range(0f, 1f)]
	public float step = 0.1f;
	
	public Vector3 offset;
	public bool setOffsetOnStart = true;

	public Rect bounds;
	
	private float zValue;

	void Start () {
		zValue = transform.position.z;
		
		if (setOffsetOnStart) {
			offset.x = target.position.x - transform.position.x;
			offset.y = target.position.y - transform.position.y;
		}
	}
	
	void FixedUpdate () {
		if (target == null)
			return;
		
		var from = transform.position;
		var to = target.position - offset;
		
		from.z = zValue;
		to.z = zValue;
		
		transform.position = Vector3.Lerp(from, to, step);
	}
}
