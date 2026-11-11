using UnityEngine;
using System.Collections;

public class EnableOnEvent : GameEventReciever {
	public GameObject target;

	void Start () {
		target.SetActive(false);
	}
	
	override public void RecieveEvent(EnumGameEvent e) {
		target.SetActive(true);
	}
	
	void Update () {
	
	}
}
