using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class SceneController : MonoBehaviour {
	public GameObject[] floors;
	public int startAt = 0;
	
	private GameObject currentFloor;
	private static SceneController instance;
	
	private static List<GameObject> reparent_ = new List<GameObject>();
	public Dictionary<int, GameObject> played_floors_ = new Dictionary<int, GameObject>();
	
	public static GameObject NewGameObject(string name = "Game Object") {
		var gameObject = new GameObject();
		
		if(dynamicContainer != null)
			gameObject.transform.parent = dynamicContainer;
		else
			reparent_.Add(gameObject);
		
		gameObject.name = name;
		
		return gameObject;
	}
	
	public static GameObject Spawn(Object original, Vector3 position, Quaternion rotation) {
		var i = GameObject.Instantiate(original, position, rotation) as GameObject;
		
		if(dynamicContainer != null) {
			i.transform.parent = dynamicContainer;
		}
		
		return i;
	}
	public static GameObject Spawn(Object original) { return Spawn(original, Vector3.zero, Quaternion.identity); }
	public static GameObject Spawn(Object original, Vector3 position) { return Spawn(original, position, Quaternion.identity); }
	
	public static Transform dynamicContainer {
		get {
			if(instance == null)
				return null;
			
			var d = instance.currentFloor.transform.FindChild("[dynamic]");
			
			if(d == null) {
				d = new GameObject().transform;
				d.name = "[dynamic]";
				d.parent = instance.currentFloor.transform;
			}
			
			return d;
		}
	}
	
	void Awake () {
		instance = this;
		
		for(var i = 0; i < floors.Length; i++) {
			floors[i].SetActive(false);
		}
		
		var floor = Game.lastFloor > 0 ? Game.lastFloor : startAt;
		GoToFloor(floor, floor != startAt);
	}
	
	void Update () {
		if(dynamicContainer != null) {
			for(var i = 0; i < reparent_.Count; i++) {
				reparent_[i].transform.parent = dynamicContainer;
			}
			
			reparent_.Clear();
		}
	}
	
	private Vector3 baseline;
	private Vector3 step = new Vector3(0f, 1000f, 0f);
	private int currentIndex;
	
	public void GoToFloor(int index, bool rescanPaths = true) {
		var old_floor = currentFloor;
		
		currentFloor = played_floors_.ContainsKey(index) ? played_floors_[index] : Instantiate(floors[index]);
		currentFloor.transform.parent = transform;
		currentFloor.transform.position = floors[index].transform.position;
		currentFloor.SetActive(true);
		
		var respawn = (GameObject)GameObject.FindWithTag(SRTags.RespawnPoint);
		if(respawn == null)
			Debug.LogError("No respawn point found!");
		
		var respawnpoint = respawn.transform.position;
		var player = Common.GetPlayer();
		if(player == null) {
			player = Spawn(Game.GetPlayer(), respawnpoint).GetComponent<PlayerController>();
		} else {
			player.transform.parent = currentFloor.transform;
		}
		
		if(old_floor != null) {
			played_floors_[currentIndex] = old_floor;
			old_floor.SetActive(false);
		}
		
		if (rescanPaths) {
			AstarPath.active.Scan();
		}
		
		Camera.main.transform.position = CameraController.targetPosition;
		
		currentIndex = index;
		Game.RememberFloor(index);
		GameDataTracker.Retrack();
	}
	
	public static void Restart() {
		if(instance.played_floors_.ContainsKey(instance.currentIndex)) {
			var floor = instance.played_floors_[instance.currentIndex];
			instance.played_floors_.Remove(instance.currentIndex);
			Destroy(floor);
		}
		
		instance.GoToFloor(instance.currentIndex, false);
	}
	
	
}
