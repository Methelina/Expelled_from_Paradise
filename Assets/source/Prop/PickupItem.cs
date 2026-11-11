using UnityEngine;
using System.Collections;

public class PickupItem : GameEventReciever {
	void Start () {
		GetComponent<Collider2D>().enabled = false;
		if(gameObject.GetComponent<PointArrow>() != null)
			gameObject.GetComponent<PointArrow>().enabled = false;
	}
	
	override public void RecieveEvent(EnumGameEvent e) {
		GetComponent<Collider2D>().enabled = true;
		if(gameObject.GetComponent<PointArrow>() != null)
			gameObject.GetComponent<PointArrow>().enabled = true;
	}
	
	void Update () {
		var shadow = GetComponent<EZCastShadow>();
		
		if(shadow != null) {
			shadow.offset = Mathf.PingPong(Time.time / 5f, 0.2f);
		}
	}
}
