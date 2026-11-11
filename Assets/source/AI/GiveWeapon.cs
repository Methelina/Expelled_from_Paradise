using UnityEngine;
using System.Collections;

public class GiveWeapon : MonoBehaviour {
	public GameObject Weapon;
	
	void Start () {
		var weap = SceneController.Spawn(Weapon);
		BroadcastMessage("TakeWeapon", weap.GetComponent<Weapon>());
	}
	
	void Update () {
		
	}
}
