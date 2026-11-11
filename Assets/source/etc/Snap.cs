using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Snap : MonoBehaviour {
	public float snapX = 0.25f;
	public float snapY = 0.25f;
	
	void Start () {
	
	}
	
	void Update () {		
		#if UNITY_EDITOR
			transform.position = new Vector2(Mathf.Round(transform.position.x / snapX) * snapX, Mathf.Round(transform.position.y / snapY) * snapY);
		#endif
	}
}
