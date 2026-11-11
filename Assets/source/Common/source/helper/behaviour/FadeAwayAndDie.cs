using UnityEngine;
using System.Collections;

public class FadeAwayAndDie : MonoBehaviour {
	[Range(0, 10)]
	public float T = 0.25f;

	void Start () {
		StartCoroutine(FadeAway());
	}
	
	IEnumerator FadeAway () {
		for (float t = T; t > 0; t -= Time.fixedDeltaTime) {			
			var color = GetComponent<Renderer>().material.color;
			color.a = Mathf.Clamp01(t / T);
			GetComponent<Renderer>().material.color = color;
			
			yield return new WaitForFixedUpdate();
		}
		
		Destroy(gameObject);
	}
	
	void Update () {
	
	}
}
