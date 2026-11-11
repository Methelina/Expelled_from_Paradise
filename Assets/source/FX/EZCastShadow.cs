using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EZCastShadow : MonoBehaviour {
	public bool useGlobal = true;
	
	[Range(0f, 360f)]
	public float angle = 0f;

	[Range(0f, 1f)]
	public float intencity = 0.5f;
	[Range(0f, 2f)]
	public float offset = 0.25f;
	
	private GameObject shadow_;
	private SpriteRenderer spriteRenderer_;
	private static ShadowSettings shadowSettings;

	void Start () {
	}
	
	void OnDestroy() {
		if(shadow_ != null)
			DestroyShadow(shadow_);
	}
	
	void OnDisable() {
		if(shadow_ != null)
			shadow_.SetActive(false);
	}
	
	void FetchSettings() {
		if(shadowSettings == null) {
			shadowSettings = FindObjectOfType<ShadowSettings>();
		}
	}
	
	void DestroyShadow(GameObject shadow) {
		if(!Application.isPlaying && Application.isEditor)
			DestroyImmediate(shadow.gameObject);
		else
			Destroy(shadow.gameObject);
		
		shadow_ = null;
	}
	
	void OnEnable() {
		if(shadow_ != null) {
			shadow_.SetActive(true);
			return;
		}
			
		for(var i = transform.childCount - 1; i >= 0; i--) { 
			var _old_shadow = transform.GetChild(i);
			
			if(_old_shadow != null && _old_shadow.name.IndexOf("_shadow") != -1)
				DestroyShadow(_old_shadow.gameObject);
		}

		shadow_ = new GameObject();
		shadow_.transform.position = transform.position;
		
		spriteRenderer_ = shadow_.AddComponent<SpriteRenderer>();
		
		shadow_.name = gameObject.name + "_shadow";
		shadow_.hideFlags = HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.HideInHierarchy | HideFlags.HideInInspector;
		
		spriteRenderer_.sortingLayerID = SRSortingLayers.Background;
		spriteRenderer_.sortingOrder = 10;
		
		shadow_.transform.parent = transform;
		
		checkGlobal();
	}
	
	void Update () {
		if(spriteRenderer_ == null)
			return;
		
		spriteRenderer_.sprite = GetComponent<SpriteRenderer>().sprite;
		
		var c = Color.black;
		c.a = intencity;
		spriteRenderer_.color = c;
		
		shadow_.transform.position = transform.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * offset;
		shadow_.transform.localScale = transform.localScale;
		shadow_.transform.rotation = transform.rotation;
		spriteRenderer_.material = SRResources.Shadows;
		
		checkGlobal();
	}
	
	void checkGlobal() {
		if(useGlobal) {
			FetchSettings();
			
			if(shadowSettings != null)
				shadowSettings.Apply(this);
		}
	}
	
	
}
