using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ConfMultiplier {
	public float amount;
	public int min;
	public int max;
	
	public ConfMultiplier(float amount, int min, int max) {
		this.amount = amount;
		this.min = min;
		this.max = max;
	}
}

public class ResourceModifier {
	private Dictionary<string, int> _items = new Dictionary<string, int>();
	private Dictionary<string, List<ConfMultiplier>> _multipliers = new Dictionary<string, List<ConfMultiplier>>();
	private List<ConfMultiplier> _all_multipliers = new List<ConfMultiplier>();
	
	public int getAmount (string id) {
		if(_items.ContainsKey(id))
			return _items[id];
		else
			return 0;
	}
	
	public ResourceModifier take(string id, int amount = 1) {
		give(id, -amount);
		return this;
	}
	
	public ResourceModifier takeAll(string id) {
		return take(id, getAmount(id));
	}
	
	public int give(string id, int amount = 1) {
		if(!_items.ContainsKey(id))
			_items[id] = 0;
		
		var v = _items[id];
		_items[id] += amount;
		
		return getValue(id) - v;
	}
	
	public bool multiply(float amount, int min = 1, int max = int.MaxValue) {
		_all_multipliers.Add(new ConfMultiplier(amount, min, max));
		
		return _items.Count > 0;
	}
	
	public bool multiply(string id, float amount, int min = 1, int max = int.MaxValue) {
		if(!_multipliers.ContainsKey(id))
			_multipliers.Add(id, new List<ConfMultiplier>());
		
		var conf = new ConfMultiplier(amount, min, max);
		_multipliers[id].Add(conf);
		
		return do_multiply((float)_items[id], conf) != (float)_items[id];
	}
	
	private float do_multiply(float value, ConfMultiplier mul) {
		var sign = (int)Mathf.Sign(mul.min);
		var absvalue = (int)Mathf.Abs(value);
		
		if(value == 0)
			return value;
		
		if(absvalue < mul.min)
			return value;
		
		if(sign > 0)
			return Mathf.Clamp(Mathf.Floor(value * mul.amount), mul.min, mul.max);
		else
			return Mathf.Clamp(Mathf.Floor(value * mul.amount), mul.min, mul.max);
	}
	
	public bool canTake(ResourceManager resouces) {
		foreach (KeyValuePair<string, int> entry in _items) {
			if (resouces.GetAmount(entry.Key) < Mathf.Abs(entry.Value) && entry.Value < 0)
				return false;
		}
		
		return true;
	}
	
	public int getValue(string id) {
		var value = (float)_items[id];
		var list = new List<ConfMultiplier>();
		
		if(_multipliers.ContainsKey(id))
			list = _multipliers[id];
		
		for (var i = 0; i < list.Count; i++) 
			value = do_multiply(value, list[i]);
		
		for (var i = 0; i < _all_multipliers.Count; i++)
			value = do_multiply(value, _all_multipliers[i]);
		
		return (int)value;
	}
	
	override public string ToString() {
		var split = ", ";
		var take = "";
		var give = "";
		
		foreach (KeyValuePair<string, int> entry in _items) {
			if(entry.Value > 0)
				give += "+" + UIFormatter.FormatResourceCost(entry.Key, entry.Value) + split;
			else if(entry.Value < 0)
				take += UIFormatter.FormatResourceCost(entry.Key, entry.Value) + split;
		}

		var str = give + " " + take;
		
		return str.Length > split.Length ? str.Substring(0, str.Length - split.Length) : "";
	}

}
