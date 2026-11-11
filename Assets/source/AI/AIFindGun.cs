using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AIFindGun : MonoBehaviour {
	private Transform gun;
	
	void Start () {
		gun = GetComponent<Senses>().findWeapon();
	}
	
	void FixedUpdate () {
		if(gun != null) {
			if(Vector3.Distance(transform.position, gun.position) < 1f) {
				var weapon = gun.gameObject.GetComponentInChildren<Weapon>();
		
				if(weapon != null) {
					BroadcastMessage("TakeWeapon", weapon);
					Destroy(gun.gameObject);
				}
			} else {
				GetComponent<AINavigator>().runTo(gun.position);
			}
		} else {
			GetComponent<Blackboard>().SendEvent("FINISHED");
		}
	}
}
