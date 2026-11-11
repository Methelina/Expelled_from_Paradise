using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour {
	
	void Start () {
	
	}
	
	void Update () {
		var player = Common.GetPlayer();
		var text = GetComponent<Text>();
		
		if(player == null) {
			text.text = Mathf.Repeat(Time.time, 1f) > 0.5f ? "R TO RESTART" : "DEAD";
			return;
		}
		
		var weapon = player.GetComponentInChildren<Weapon>();
		if(weapon) {
			text.enabled = true;
			text.text = string.Format("{0} [{1}/{2}]", weapon.title, weapon.ammo, weapon.maxAmmo);
		} else {
			text.enabled = false;
		}
	}
}
