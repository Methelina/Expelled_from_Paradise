using UnityEngine;
using System.Collections;

public class Gibs : MonoBehaviour {
	public float speed = 8f;
	public float speedVariation = 5f;
	
	public float lifetime = 0.5f;
	public float lifetimeVariation = 0.5f;
	
	public Sprite[] initial;
	public Sprite[] final;
	
	private Vector2 direction_ = Vector2.zero;
	private float velocity_ = 0f;
	private SpriteRenderer srenderer_;
	
	void Start () {
		srenderer_ = GetComponent<SpriteRenderer>();
		
		if(srenderer_ == null) 
			srenderer_ = gameObject.AddComponent<SpriteRenderer>();
		
		srenderer_.sortingLayerID = SRSortingLayers.Decal.id;
		
		gameObject.name = "[gibs]";
		
		direction_ = Random.insideUnitCircle;
		velocity_ = speed + Random.Range(-speedVariation, speedVariation);
		srenderer_.sprite = initial[Random.Range(0, initial.Length)];
		
		transform.rotation = Quaternion.AngleAxis(90f * Random.Range(0,3), Vector3.forward);
		
		Invoke("Die", lifetime);
	}
	
	void OnCollision2D(Collision2D coll) {
		Die();
	}
	
	void Die() {
		srenderer_.sprite = final[Random.Range(0, final.Length)];
		Destroy(gameObject.GetComponent<Rigidbody2D>());
		Destroy(gameObject.GetComponent<Collider2D>());
		Destroy(this);
	}
	
	void FixedUpdate () {
		transform.position = transform.position + (Vector3)direction_ * velocity_ * Time.fixedDeltaTime;
	}
}
