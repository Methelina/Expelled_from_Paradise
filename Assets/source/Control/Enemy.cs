using UnityEngine;
using System.Collections.Generic;

public class Enemy : Destroyable {
	public static List<Enemy> list_ = new List<Enemy>();

	void OnEnable () {
		list_.Add(this);
	}
	
	void OnDisable () {
		list_.Remove(this);
	}
	
	void OnKilled () {
		list_.Remove(this);
	}
	
	void Update () {
	
	}
}
