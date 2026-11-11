using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == SRTags.Player) {
			Application.LoadLevel(0);
		}
	}
	
	void Update () {
	
	}
}
