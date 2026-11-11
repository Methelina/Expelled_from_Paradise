using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {
	public float health = 100f;
	
	public GameObject OnHitFX;
	
	public GameObject corpsePrefab;
	
	public GameObject root;
	public bool killRoot;
	
	private Vector2 sourcePosition;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void ApplyDamagePos(Vector2 position) {
		sourcePosition = position;
		if (OnHitFX != null)
			SceneController.Spawn(OnHitFX, position, Quaternion.identity);
	}
	
	void ApplyDamage(float damage) {
		health -= damage;
		
		if (health <= 0) {
			health = 0;
			
			BroadcastMessage("OnKilledFrom", sourcePosition, SendMessageOptions.DontRequireReceiver);
			BroadcastMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
			
			if (corpsePrefab != null) {
				SceneController.Spawn(corpsePrefab, transform.position, transform.rotation);
			}
			
			if(killRoot)
				root.SetActive(false);
			else
				gameObject.SetActive(false);
		}
	}
}
