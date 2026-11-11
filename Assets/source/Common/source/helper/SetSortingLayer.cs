using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour {
	public string SortingLayerName = "Default";
	public int SortingOrder = 0;

	// Use this for initialization
	void Start () {
		// Set the sorting layer of the particle system.
		GetComponent<Renderer>().sortingLayerName = SortingLayerName;
		GetComponent<Renderer>().sortingOrder = SortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
