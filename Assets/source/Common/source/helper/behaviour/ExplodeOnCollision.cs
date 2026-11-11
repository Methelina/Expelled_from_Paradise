using UnityEngine;
using System.Collections;

public class ExplodeOnCollision : MonoBehaviour {
	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * 0.5f;
		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-80, 80);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (Explosion && (collision.gameObject.layer & (1 << LayerMask.NameToLayer("Environment"))) == 0) {
			SceneController.Spawn(Explosion,transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
