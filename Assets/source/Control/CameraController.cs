using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
	[Range(0f, 1f)]
	public float lerp = 0.75f;	
	
	[Range(0f, 15f)]
	public float shakiness = 0.5f;
	
	public float sightRange = 1f;
	public float aimRange = 9.5f;
	public float aimLerp = 0.2f;
	
	private float shake_i_ = 0f;
	private float shake_t_ = 0f;	
	
	private Camera cam_;
	private static CameraController instance_;
	
	private Vector3 camOffset = Vector3.zero;
	private Vector3 shakeBias = Vector3.zero;
	
	void Start () {
		instance_ = this;
		
		cam_ = GetComponent<Camera>();
	}
	
	public static Vector3 targetPosition {
		get { 
			if(instance_ != null) 
				return instance_.cameraTargetPos;
			else 
				return Camera.main.transform.position;
		}
	}
	
	public static void Shake(float i, float t, Vector3 bias = new Vector3()) {
		if(instance_ != null) {
			instance_.shake_i_ = i;
			instance_.shake_t_ = t;
			instance_.shakeBias = bias;
		}
	}
	
	public Vector3 cameraTargetPos {
		get { 
			var plr = Common.GetPlayer();
			if(plr == null)
				return transform.position;
			
			var player = plr.transform;
			
			var offset = Input.mousePosition;
			offset.x = offset.x / Screen.width;
			offset.y = offset.y / Screen.height;
			
			var range = sightRange;
			if (Input.GetKey(KeyCode.LeftShift))
				range += aimRange;
			
			offset = (offset - Vector3.one / 2f) * range;
			
			offset.z = 0;
			
			camOffset = Vector3.Lerp(camOffset, offset, aimLerp);
			var camPos = new Vector3(player.position.x, player.position.y, transform.position.z) + camOffset;
			
			var shake_k = shake_i_ * shake_t_ * shakiness;
			var shake =  (shakeBias * shake_k + (Vector3)(Random.insideUnitCircle) * shake_k);
			return camPos + shake; 
		} 
		set {}
	}
	
	void FixedUpdate () {
		shake_t_ = Mathf.Clamp(shake_t_ - Time.fixedDeltaTime, 0f, float.MaxValue);
		cam_.transform.position = Vector3.Lerp(cam_.transform.position, cameraTargetPos, lerp);
	}
	
	void OnDrawGizmos() {
		var color = Color.yellow;
		color.a = 0.15f;
		
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, sightRange);
		
		color = Color.green;
		color.a = 0.15f;
		
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, aimRange);
	}
}
