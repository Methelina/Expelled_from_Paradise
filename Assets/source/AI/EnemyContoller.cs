using UnityEngine;
using System.Collections;

public class EnemyContoller : MonoBehaviour {
	public float moveSpeed = 10f;
	public float sightRange = 10f;
	
	private bool playerLocked = true;
	
	void Start () {
	
	}
	
	void FixedUpdate () {
		var player = Common.GetPlayer();
		
		if (player == null)
			return;
		
		var distance = Vector3.Distance(player.transform.position, transform.position);
		
		if (distance > 2f && IsPlayerInSight()) {
			var tv = player.transform.position - transform.position;
			
			transform.position = transform.position + tv.normalized * moveSpeed * Time.fixedDeltaTime;
			playerLocked = true;
		} else {
			playerLocked = false;
		}
		
		if (playerLocked) {
			transform.rotation = Common.RotateTowards(transform, player.transform);
			
			BroadcastMessage("FireAt", player.transform.position - transform.position, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	bool IsPlayerInSight() {
		var player = Common.GetPlayer();
		var direction = player.transform.position - transform.position;
		
		var rayhit = Physics2D.Raycast(transform.position, direction, sightRange, ~(1 << LayerMask.NameToLayer("Enemy")));
		
		/*var sightDirection = new Vector3(
			Mathf.Cos (transform.rotation.z),
			Mathf.Sin (transform.rotation.z)
		);*/
		
		if (rayhit.collider != null)
			return rayhit.collider.CompareTag("Player");
		
		return false;
	}
	
	void OnDrawGizmosSelected () {
		Color color;
		
		if (IsPlayerInSight())
			color = Color.red;
		else
			color = Color.green;
		
		color.a = 0.15f;
		Gizmos.color = color;
		
		Gizmos.DrawSphere(transform.position, sightRange);
	}
	
	public Weapon Weapon {
		get { return GetComponentInChildren<Weapon>(); }
	}
	
	void OnKilled() {
		if (Weapon != null) {
			var drop = SceneController.Spawn(Weapon.DropPrefab, transform.position, Quaternion.identity);
			
			drop.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * 25f;
			drop.GetComponent<Rigidbody2D>().angularVelocity = 260 * Random.Range(-1f, 1f);
			
			var weapon = Weapon;
			
			weapon.transform.parent = drop.transform;
			weapon.transform.localPosition = Vector3.zero;
		} 
	}
}
