using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	[Range(0,100)]
	public float amplitude = 5;
	[Range(0,50)]
	public float speed = 4;
	
	[Range(0, 10)]
	public int amp_offset = 1;
	
	private Vector3 initialPos;

	void Start () {
		initialPos = transform.localPosition;
	}
	
	void Update () {
		transform.localPosition = initialPos + transform.up *  Mathf.Sin((amp_offset * Mathf.PI) + Time.time * speed) * (amplitude / 20f);
	}
}
