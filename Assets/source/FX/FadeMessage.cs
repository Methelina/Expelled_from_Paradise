using UnityEngine;
using System.Collections;

public class FadeMessage : MonoBehaviour {
	public float time_in = 1f;
	public float time_stay = 1f;
	public float time_out = 1f;
	
	void Start () {
		StartCoroutine(Fade());
	}
	
	IEnumerator Fade() {
		var cg = GetComponent<CanvasGroup>();
		if(cg == null)
			Debug.LogError("No canvas group found");
		
		for(float t = 0f; t < time_in; t += Time.fixedDeltaTime) {
			cg.alpha = t / time_in;
			yield return new WaitForFixedUpdate();
		}
		
		cg.alpha = 1f;
		yield return new WaitForSeconds(time_stay);
		
		for(float t = 0f; t < time_out; t += Time.fixedDeltaTime) {
			cg.alpha = 1f - t / time_out;
			yield return new WaitForFixedUpdate();
		}
		
		Destroy(gameObject);
	}
	
	void Update () {
	}
}
