using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTweaker : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		Time.timeScale = GetComponent<Slider>().value;
	}
}
