using UnityEngine;
using System.Collections;

public class FloorExit : GameEventReciever {
	public int index;
	public bool wait = true;
	public bool once = true;
	
	void Start () {
		if(GetComponent<PointArrow>()) {
			GetComponent<PointArrow>().enabled = false;
		}
	}
	
	void Update () {
	
	}
	
	override public void RecieveEvent(EnumGameEvent evt) {
		if(evt == targetEvent) {
			wait = false;
			if(GetComponent<PointArrow>()) {
				GetComponent<PointArrow>().enabled = true;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if(wait == true) return;
		
		if(collider.tag == SRTags.Player) {
			FindObjectOfType<SceneController>().GoToFloor(index);
			
			if(once) {
				Destroy(gameObject.GetComponent<PointArrow>());
				Destroy(this);
			}
		}
	}
	
	void OnDrawGizmos () {
		var bc2d = GetComponent<BoxCollider2D>();
		if(bc2d != null) {
			var color = Color.blue;
			color.a = 0.5f;
			Gizmos.color = color;
			Gizmos.DrawCube(transform.position + (Vector3)bc2d.offset, (Vector3)bc2d.size + new Vector3(0f,0f,0.1f));
		}
	}
}
