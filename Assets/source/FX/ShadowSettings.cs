using UnityEngine;
using System.Collections;

public class ShadowSettings : MonoBehaviour {

	
	[Range(0f, 360f)]
	public float angle = 0f;

	[Range(0f, 1f)]
	public float intencity = 0.5f;
	[Range(0f, 2f)]
	public float offset = 0.25f;
	
	public void Start() {
		
	}
	
	public void Apply(EZCastShadow shadow) {
		shadow.angle = angle;
		shadow.intencity = intencity;
		shadow.offset = offset;
	}
	
}
