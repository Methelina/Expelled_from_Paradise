using UnityEngine;
using System.Collections.Generic;

public static class Common {
	public const float UNIT = 100f;
	
	public static PlayerController GetPlayer() {
		return GameObject.FindObjectOfType<PlayerController>();
	}
	
	public static int PixelsInUnit() {
		return 16;
	}
		
	public static Bounds OrthographicBounds(this Camera camera) {
		var pos = camera.transform.position;
		pos.z = 0;
		
		var aspect = (float)camera.pixelWidth / (float)camera.pixelHeight;
		var vExt = camera.orthographicSize * 2;
		var hExt = vExt * aspect;
		
		Bounds bounds = new Bounds(pos, new Vector3(hExt, vExt));
		
		return bounds;
	}
	
	public static void RemoveChildren(this Transform t) {
		for (int i = 0; i < t.childCount; i++) {
			SafeDestroy(t.GetChild(i).gameObject);
		}
	}
	
	public static Vector3 Direction(float angle) {
		return new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
	}
	
	public static bool IsAlive(this GameObject gobj) {
		return gobj != null && gobj.GetComponent<Destroyable>() != null && gobj.GetComponent<Destroyable>().health > 0;
	}
	
	public static bool IsStunned(this GameObject gobj) {
		return gobj != null && gobj.GetComponent<AIStunned>() != null && gobj.GetComponent<AIStunned>().enabled;
	}
	
	public static void SafeDestroy(GameObject gobj) {
		if(gobj != null)
			GameObject.Destroy(gobj);
	}
	
	public static float ClampAngle(
		float currentValue,
		float minAngle,
		float maxAngle,
		float clampAroundAngle = 0
	) {
		float angle = currentValue - (clampAroundAngle + 180);
	
		while (angle < 0) {
			angle += 360;
		}
	
		angle = Mathf.Repeat(angle, 360);
	
		return Mathf.Clamp(
			angle - 180,
			minAngle,
			maxAngle
		) + 360 + clampAroundAngle;
	}

	public static void DestroyBunch(GameObject[] objects) {
		for (int i = 0; i < objects.Length; i++) 
			SafeDestroy(objects[i]);
	} 

	public static float GetUnitHeight(GameObject gameObject) {
		if (!gameObject.GetComponent<SpriteRenderer>()) {
			return 0;
		}
		
		Sprite sp = gameObject.GetComponent<SpriteRenderer>().sprite;
		
		float d = Mathf.Abs(sp.bounds.max.y - sp.bounds.min.y);
		d *= gameObject.transform.localScale.y;
		
		return d;
	}

	public static void FaceTo(GameObject whom, GameObject who, bool Inverse = false) {
		FaceToV3(whom, who.transform.position, Inverse);
	}
	
	public static void FaceToV3(GameObject whom, Vector3 pos, bool Inverse = false, bool Y = false) {
		float d = Mathf.Sign(whom.transform.position.x - pos.x);
		
		if (Inverse)
			d = -d;
		
		var ls = whom.transform.localScale;
		
		if(Y) {
			if (d > 0 && ls.y > 0) {
				ls.y = -ls.y;
			} else if (d < 0 && ls.y < 0) {
				ls.y = -ls.y;
			}
		} else {
			if (d > 0 && ls.x > 0) {
				ls.x = -ls.x;
			} else if (d < 0 && ls.x < 0) {
				ls.x = -ls.x;
			}
		}
				
		whom.transform.localScale = ls;	
	}

	public static int ProbabilityUnit(float chance) {
		return Random.Range(0, 100) / 100f <= chance ? 1 : 0;
	}

	public static Vector3 GetRandomPosition (Vector3 from, float range = 1f) {
		return from + (Vector3)Random.insideUnitCircle * range;
	}
	
	public static Quaternion GetRotationTowardsMouse(Transform from, float angle_offset = 0) {
		Vector2 mousePos = GetMousePosition();
		Vector2 delta = mousePos - (Vector2)from.position;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Quaternion GetRotationTowards(Transform from, Vector2 position, float angle_offset = 0) {
		Vector2 delta = position - (Vector2)from.position;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Quaternion RotateTowards(Transform who, Transform to, float angle_offset = 0) {
		Vector2 mousePos = to.transform.position;
		Vector2 delta = mousePos - (Vector2)who.position;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Quaternion RotateTowardsV3(Transform who, Vector3 to, float angle_offset = 0) {
		Vector2 delta = to - (Vector3)who.position;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Quaternion Rotation(Vector3 from, Vector3 to, float angle_offset = 0f) {
		Vector2 delta = to - from;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Quaternion Rotation(Vector3 from, Vector3 to, Vector3 axis) {
		Vector2 delta = to - from;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle, axis);
	}

	public static Quaternion RotateToV3(Transform who, Vector3 to, float angle_offset = 0) {
		Vector2 delta = to;
		
		float rAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(rAngle + angle_offset, Vector3.forward);
	}
	
	public static Vector3 GetMousePosition () {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		var point = ray.origin + (ray.direction * 1);   

		point.z = 0;

		return point;
	}
}
