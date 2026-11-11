using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject Target;

	void Start () {
		transform.position = TargetPos;
	}
	
	public Vector3 TargetPos {
		get { return new Vector3(Target.transform.position.x, Target.transform.position.y, Camera.main.transform.position.z); } 
		set {}
	}
	
	void FixedUpdate () {
		transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);
	}
}
