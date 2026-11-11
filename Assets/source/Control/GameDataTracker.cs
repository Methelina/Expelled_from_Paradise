using UnityEngine;
using System.Collections;

public class GameDataTracker : MonoBehaviour {
	private static GameDataTracker instance;
	
	public int livingEnemies;
	
	public bool ready = false;
	
	void Awake() {
		instance = this;
		ready = false;
	}
	
	public void SendEvent(EnumGameEvent evt, bool all = false) {
		if(evt == EnumGameEvent.NONE || evt == EnumGameEvent.ALL) return;
		
		var listeners = all ? GameEventReciever.all_recievers_ : GameEventReciever.active_recievers_;
		
		for(var i = 0; i < listeners.Count; i++) {
			if(listeners[i].targetEvent == EnumGameEvent.NONE) continue;
				
			if(listeners[i].targetEvent == EnumGameEvent.ALL || listeners[i].targetEvent == evt)
				listeners[i].RecieveEvent(evt);
		}
	}
	
	void Start () {
		//  gameObject.hideFlags = HideFlags.HideAndDontSave;
	}
	
	public static void Retrack () {
		if(instance != null) instance.ready = false;
	}
	
	void Track () {
		livingEnemies = Enemy.list_.Count;
	
		ready = true;
	}
	
	void FixedUpdate () {
		Track();
	}
}
