using UnityEngine;
using System.Collections;

public class AutoDestroyableParticleSystem : MonoBehaviour {
	public bool DestroyOnLastPaticle;

	// Use this for initialization
	void Start () {
		if(GetComponent<ParticleSystem>() != null && !DestroyOnLastPaticle)
			Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}
	
	void Update () {
		if(DestroyOnLastPaticle && GetComponent<ParticleSystem>().particleCount == 0)
			Destroy(gameObject);
	}
}
