using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectCamera : MonoBehaviour {
	[Range(1, 10)]
	public uint pixelSize = 1;

	void Start () {
	}
	
	void Update () {
		float UnitsPerPixel = 1f / 100f;
		GetComponent<Camera>().orthographicSize = Screen.height / 2f * UnitsPerPixel / (float)pixelSize;
	}
}
