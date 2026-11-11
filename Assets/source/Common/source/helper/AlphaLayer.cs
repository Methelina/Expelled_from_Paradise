using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AlphaLayer : MonoBehaviour {
	[Range(0, 1)]
	public float Alpha = 1;
	
	[Range(0, 1)]
	public float Randomize;

	void Start () {
		ApplyAlphaTo(transform);
	}
	
	void Update () {
	}
	
	void ApplyAlphaTo(Transform target) {
		var renderer2d = target.gameObject.GetComponent<SpriteRenderer>();
		if (renderer2d) {
			var c = renderer2d.color;
			c.a = Alpha + Random.Range(-Randomize, Randomize);
			renderer2d.color = c;
		}
		
		for (var i = 0; i < target.childCount; i++)
			ApplyAlphaTo(target.GetChild(i));
	}
}
