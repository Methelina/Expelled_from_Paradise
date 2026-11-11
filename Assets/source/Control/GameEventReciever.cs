using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventReciever : MonoBehaviour {
	public static List<GameEventReciever> active_recievers_ = new List<GameEventReciever>();
	public static List<GameEventReciever> all_recievers_ = new List<GameEventReciever>();
	
	public EnumGameEvent targetEvent = EnumGameEvent.ALL;
	
	void Awake() {
	}
	
	virtual public void RecieveEvent(EnumGameEvent evt) { /*@virtual*/ }
	
	void OnDestroy() {
		if(all_recievers_.Contains(this))
			all_recievers_.Remove(this);
	}
	
	void OnEnable () {
		if(!all_recievers_.Contains(this))
			all_recievers_.Add(this);
		
		active_recievers_.Add(this);
	}
	
	void OnDisable () {
		active_recievers_.Remove(this);
	}
	
	void Update () {
		
	}
}
