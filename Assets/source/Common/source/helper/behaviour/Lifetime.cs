using UnityEngine;

public class Lifetime : MonoBehaviour {
	[Range(0, 600)]
	public float Time = 0;
	public GameObject target;
	
	void Start () {
		Invoke("Die", Time);
	}
	
	void Die () {
		if (target != null)
			Destroy(target);
		else
			Destroy(gameObject);
	}
	
}
