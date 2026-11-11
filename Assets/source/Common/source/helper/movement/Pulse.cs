using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {
	[Range(0,100)]
	public float amplitude = 5;
	[Range(0,50)]
	public float speed = 4;
	
	private Vector3 initialScale;
	
	void Start () {
		initialScale = transform.localScale;
	}
	
	void Update () {
		transform.localScale = initialScale - Vector3.one *  Mathf.Sin(Time.time * speed) * (amplitude / 20);
	}
}
