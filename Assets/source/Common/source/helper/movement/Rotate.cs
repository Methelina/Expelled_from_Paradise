using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	[Range(-360,360)]
	public float speed = 1f;
	
	void Start () {
	
	}
	
	void Update () {
		transform.Rotate(-speed * Time.fixedDeltaTime * Vector3.forward);
	}
}
