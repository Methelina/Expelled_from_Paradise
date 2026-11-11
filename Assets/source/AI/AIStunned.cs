using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AIStunned : MonoBehaviour {
	public float duration = 15f;

	void OnEnable () {
		Invoke("OnFinished", duration);
	}
	
	void OnDisable () {
	}
	
	void OnFinished() {
		if(gameObject.IsAlive()) {
			BroadcastMessage("UpdateMovement", Vector3.zero, SendMessageOptions.DontRequireReceiver);
			GetComponent<Blackboard>().SendEvent("FINISHED");
		}
	}
	
	void Update () {
		BroadcastMessage("Stunned", SendMessageOptions.DontRequireReceiver);
	}
}
