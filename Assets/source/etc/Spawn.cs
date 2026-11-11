using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject[] PF;
	public int count = 5;

	void Start () {
		for(var i = 0; i < count; i++)
			SceneController.Spawn(PF[Random.Range(0, PF.Length)], transform.position, Quaternion.identity);
	}
}
