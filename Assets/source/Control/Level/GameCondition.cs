using UnityEngine;
using System.Collections;
using BehaviourMachine;

public enum EnumGameEvent {
	NONE = 0,
	ALL = 1,
	FLOOR_CLEAR = 2,
	LEVEL_COMPLETE = 3,
}

public class GameCondition : MonoBehaviour {
	public GameObject target;
	
	[Space(10)]
	[Header("Self")]
	public bool destroyed;
	public bool pickup;
	
	[Space(10)]
	[Header("State")]
	
	public int enemies = -1;
	
	public float delay = 0f;
		
	[Header("Send")]
	[Space(10)]
	
	public bool toAll = false;
	public bool once = true;
	public EnumGameEvent gameEvent = EnumGameEvent.NONE;
	
	private static GameDataTracker game_data_;
	
	private Blackboard blackboard_;
	private PlayMakerFSM playmaker_;
	
	void Start () {
		if(game_data_ == null) {
			var game_data = new GameObject();
			game_data.name = "game_data_tracker";
			
			game_data_ = game_data.AddComponent<GameDataTracker>();
		}
		
		if(target == null)
			target = gameObject;
		
		playmaker_ = target.GetComponent<PlayMakerFSM>();
		blackboard_ = target.GetComponent<Blackboard>();
		
		//  Invoke("Complete", time);
	}
	
	void Complete () {
		if(playmaker_ != null) {
			playmaker_.SendEvent("Complete");
		}
		
		if(blackboard_ != null) {
			blackboard_.SendEvent("FINISHED");
		}
		
		target.SendMessage("Trigger");
		
		game_data_.SendEvent(gameEvent, toAll);
		
		if(once)
			enabled = false; 
	}
	
	void PickUp() {
		if(pickup)
			Fire();	
	}
	
	void OnDestroyed() {
		if(destroyed) 
			Fire();
	}
	
	void Trigger () {
	}
	
	void Fire () {
		fired = true;
		Debug.Log(name + " fired");
		Invoke("Complete", delay);
	}
	
	bool fired = false;
	void Update () {
		if(fired || !game_data_.ready)
			return;
		
		if(enemies == game_data_.livingEnemies)
			Fire();
	}
}
