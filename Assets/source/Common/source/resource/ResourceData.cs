using UnityEngine;
using System.Collections;

public class Resource {
	private string _ID;
	private int _amount;
	
	private int _min;
	private int _max;
	
	private string _tooltip;
	
	private string _name;
	private string _format;
	
	public Resource (string id, int amount, int min, int max, string tooltip, string name, string format) {
		_ID = id;
		_amount = amount;
		_min = min;
		_max = max;
		_tooltip = tooltip;
		_name = name;
		_format = format;
	}
	
	public string id {
		get { return _ID; }
	}
	
	public string formattedValue {
		get { return string.Format(_format, _name, _amount); }
	}
	
	public string name {
		get { return _name; }
	}
	
	public string tooltip {
		get { return _tooltip; }
	}
	
	public string ID {
		get { return _ID; }
	}
	
	public int min {
		get { return _min; }
	}
	
	public int max {
		get { return _max; }
	}
	
	public int amount {
		get { return _amount; }
	}
	
	public void AddDelta (int delta) {
		_amount += delta;
		_amount = Mathf.Clamp(_amount, _min, _max);
	}
	
	public void take(int delta) {
		AddDelta(-delta);
	}
	
	public void give(int delta) {
		AddDelta(delta);
	}
	
	public void add(int delta) {
		AddDelta(delta);
	}

	public void SetAmount (int amount) {
		_amount = amount;
		_amount = Mathf.Clamp(_amount, _min, _max);
	}
}
