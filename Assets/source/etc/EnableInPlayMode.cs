using UnityEngine;
using System.Collections;

public class EnableInPlayMode : MonoBehaviour {
	public GameObject target;
	
	void Start () {
		target.SetActive(true);
	}
	
	void Update () {
	
	}
}
