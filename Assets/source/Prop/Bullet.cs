using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 10f;
	
	public int damage = 0;
	
	public Vector3 direction;
	
	public GameObject OnHitFX;
	public GameObject HitMark;
	
	void Start () {
		transform.rotation = Quaternion.AngleAxis(
			Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f, 
			Vector3.forward
		);
	}
	
	void FixedUpdate () {
		transform.position = transform.position + direction * speed * Time.fixedDeltaTime;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		coll.gameObject.BroadcastMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		coll.gameObject.BroadcastMessage("ApplyDamagePos", (Vector2)coll.transform.position, SendMessageOptions.DontRequireReceiver);
		
		if (OnHitFX != null && coll.transform.gameObject.layer == SRLayers.Environment) {
			var angle = Mathf.Atan2(coll.contacts[0].normal.y, coll.contacts[0].normal.x) * Mathf.Rad2Deg;
			
			SceneController.Spawn(OnHitFX, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
		}
		
		if (HitMark != null && coll.transform.gameObject.layer != SRLayers.Environment && coll.transform.gameObject.layer != SRLayers.Doors)
			SceneController.Spawn(HitMark, coll.contacts[0].point, Quaternion.identity);
		
		Destroy (gameObject);
	}
}
