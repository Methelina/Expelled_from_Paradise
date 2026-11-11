using UnityEngine;
using System.Collections;

public class MouseSpawner : MonoBehaviour {
	public GameObject pf;
	public float interval;
	
	private float next_;
	
	void Start () {
	
	}
	
	void Update () {
		if(Time.time > next_) {
			SceneController.Spawn(pf, Common.GetMousePosition(), Quaternion.identity);
			next_ = Time.time + interval;
		}
	}
}
