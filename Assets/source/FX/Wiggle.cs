using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour {
	[Range(0f, 2f)]
	public float amplitude = 1f;
	public float size = 10f;
	private float lerp = 0.1f;
	
	private Quaternion initial;
	public Quaternion targetrotation;
	
	void Start () {
		initial = transform.rotation;
	}
	
	void Update () {
		float a;
		Vector3 AA;
		initial.ToAngleAxis(out a, out AA);
		
		targetrotation = Quaternion.AngleAxis(a + (Mathf.PingPong(Time.time * amplitude, 2f) - 1f) * size, AA);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, lerp);
	}
}
