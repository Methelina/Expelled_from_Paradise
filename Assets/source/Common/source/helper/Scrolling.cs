using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scrolling : MonoBehaviour {
	[Range(-4, 4)]
	public float Speed;
	
	[Range(0, 30)]
	public float Width = 1f;
	[Range(0, 30)]
	public float Height = 1f;

	[HideInInspector]
	public bool Active = true;
	
	private Vector3 Pivot;

	void Start () {
		Pivot = transform.position;
		
		StartCoroutine(ScrollCoroutine());
	}
	
	float BoundsLeft {
		get { return transform.position.x - Width / 2; }
		set { }
	}
	
	float BoundsRight {
		get { return transform.position.x + Width / 2; }
		set { }
	}
	
	void OnDrawGizmosSelected () {
		Gizmos.color = new Color(1, 0, 0, 0.25f);
		Gizmos.DrawCube(transform.position, new Vector3(Width, Height));
	}
	
	IEnumerator ScrollCoroutine () {
		GameObject second = null;
		
		if (Active) {
			Active = false;
			second = SceneController.Spawn(
				gameObject, 
				Pivot - new Vector3(Width * Mathf.Sign (Speed), 0), 
				Quaternion.identity
				);
			
			second.GetComponent<Scrolling>().Active = false;
			
			if (second != null)
				second.GetComponent<Scrolling>().Speed = Speed;
		}
		
		for ( var d  = 0f; 
				  d  < Width; 
		     	  d += Mathf.Abs(Speed * Time.deltaTime) ) {

			transform.position = Pivot + new Vector3(d * Mathf.Sign(Speed), 0f);
			
			if (second != null)
				second.GetComponent<Scrolling>().Speed = Speed;
			
			yield return new WaitForEndOfFrame();
		}
		
		transform.position = Pivot;
		
		StartCoroutine(ScrollCoroutine());
	}
	
}
