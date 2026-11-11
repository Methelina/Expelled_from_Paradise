using UnityEngine;
using System.Collections;
using TMPro;

public class FadingText : GameEventReciever {
	TextMeshPro textMesh;
	TextMeshProUGUI textMeshUGUI;
	SpriteRenderer spriteRenderer;
	
	[Range(0.01f, 0.5f)]
	public float lerp = 0.01f;
	[Range(0.1f, 10f)]
	public float interval = 1f;
	public Color colorA;
	public Color colorB;
	
	private bool state;
	private float time = 0f;
	private float dying = 1f;
	
	private bool dead = false;
	
	void Start () {
		dying = 1f;
		dead = false;
		textMesh = GetComponent<TextMeshPro>();
		textMeshUGUI = GetComponent<TextMeshProUGUI>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		if(targetEvent != EnumGameEvent.ALL && GetComponent<MeshRenderer>())
			GetComponent<MeshRenderer>().enabled = false;
	}
	
	override public void RecieveEvent(EnumGameEvent e) {
		if(e == targetEvent)
			GetComponent<MeshRenderer>().enabled = true;
	}
	
	private Color targetColor;
	
	void Update () {
		if(dead) dying -= Time.deltaTime;
		
		targetColor = state ? colorA : colorB;
		color = Color.Lerp(color, targetColor, lerp);
		
		
		var c = color;
		c.a = dying;
		color = c; 
		
		if(time > interval) {
			state = !state;
			time = 0f;
		}
		
		time += Time.deltaTime;
	}
	
	Color color {
		get { 
			if(textMesh != null)
				return textMesh.color;
			if(textMeshUGUI != null)
				return textMeshUGUI.color;
			if(spriteRenderer != null)
				return spriteRenderer.color;
			
			return Color.black;
		}
		set {
			if(textMesh != null)
				textMesh.color = value;
			if(textMeshUGUI != null)
				textMeshUGUI.color = value;
			if(spriteRenderer != null)
				spriteRenderer.color = value;
		}
	}
	
	void OnTriggerEnter2D(Collider2D entry) {
		if(entry.tag == SRTags.Player) {
			dead = true;
			Destroy(GetComponent<Collider2D>());
		}
	}
	
}
