using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {
	public Color[] colors;
	public float interval = 1f;
	public float random = 1f;
	
	private float time = 0f;
	private float runTime = 0f;
	private int index = 0;
	
	void Start () {
		 runTime = interval;
	}
	
	void Update () {
		if(time > runTime / 2f) {
			index++;
			runTime = Random.Range(interval, interval + random);
			time = 0f;
		}
		
		time += Time.deltaTime;
		Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, colors[index % colors.Length], time / runTime);	
	}
}
