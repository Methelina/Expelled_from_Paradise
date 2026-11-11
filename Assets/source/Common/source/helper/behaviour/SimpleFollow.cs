using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {
	public GameObject Target;

	public bool Rotate = true;
	public float AngleOffset = 90f;
	private float StartDistance;

	void Start () {
		StartDistance = (Target.transform.position - transform.position).magnitude;
	}
	
	void Update () {
		if (!Target)
			return;
		
		var d = Target.transform.position - transform.position;
		var n = d.normalized;
		
		transform.position = Vector3.Lerp(transform.position, Target.transform.position - n * StartDistance, 0.25f);
		
		if (!Rotate)
			return;
			
		transform.rotation = Common.RotateTowards(transform, Target.transform, AngleOffset);
	}
}
