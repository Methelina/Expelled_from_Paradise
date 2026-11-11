using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject playerPrefab;
	
	public int lastFloor_ = -1;
	
	private static Game instance_;
	private static Game instance {
		get {
			if(instance_ == null) {
				var game_object = new GameObject();
				game_object.name = "GAME";
				return game_object.AddComponent<Game>();
			}
			
			return instance_;
		}
	}
	
	void Awake () {
		if(instance_ != null) {
			Destroy(this);
			return;
		}
		
		DontDestroyOnLoad(this);
		instance_ = this;
	}
	
	void Update () {
		if(Input.GetKeyDown("r") && Common.GetPlayer() == null) {
			SceneController.Restart();
		}

		if(Input.GetKeyDown("x")) {
			for(var i = Enemy.list_.Count - 1; i >= 0; i--) {
				Enemy.list_[i].BroadcastMessage("ApplyDamage", 10000f);
			}
			
		}
		
		if(Input.GetKeyDown("z")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	public static GameObject GetPlayer() {
		return instance.playerPrefab != null ? instance.playerPrefab : SRResources.player.GoatMan;
	}
	
	public static int lastFloor {
		get { return instance.lastFloor_; }
	}
	
	public static void RememberFloor(int floor) {
		instance.lastFloor_ = floor;
	}
	
}
