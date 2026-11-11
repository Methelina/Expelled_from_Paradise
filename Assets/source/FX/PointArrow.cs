using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PointArrow : MonoBehaviour {
	public float offset;
	public float angle;
	
	private GameObject arrow;
	
	void OnDestroy() {
		if(arrow != null)
			DestroyArrow(arrow);
	}
	
	void OnDisable() {
		if(arrow != null)
			arrow.SetActive(false);
	}
	
	void OnEnable() {
		CheckArrow();
	}
	
	void Start() {
		CheckArrow();
	}
	
	void CheckArrow() {	
		if(arrow != null)
			arrow.SetActive(true);
		
		if(arrow == null) {
			arrow = SRResources.point_arrow.Instantiate();
			arrow.hideFlags = HideFlags.HideAndDontSave;
			UpdatePosition();
		}
	}
	
	void DestroyArrow(GameObject arrow) {
		if(!Application.isPlaying && Application.isEditor)
			DestroyImmediate(arrow);
		else
			Destroy(arrow);
		
		arrow = null;
	}
	
	void Update () {
		UpdatePosition();
	}
	
	void UpdatePosition() {
		if(arrow != null) {
			arrow.transform.position = transform.position + Common.Direction(angle * Mathf.Deg2Rad) * offset;
			arrow.transform.rotation = Common.RotateTowards(arrow.transform, transform, -90f);
		}
	}
	
}
