using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnumComparsionType {
	LESS,
	LESS_EQUAL,
	MORE,
	MORE_EQUAL,
	EQUAL
}

public class ResourceManager {

	private Dictionary<string, Resource> _resources = new Dictionary<string, Resource>();
	private List<Resource> _public = new List<Resource>();
	
	public ResourceManager() {
		RegisterNewResourceType("nothing", 0, 0, 0);
	}
	
	private Resource RegisterNewResourceType(
		string id, int initamt = 0, 
		int min = int.MinValue, int max = int.MaxValue, string tooltip = "", bool is_public = false, string name = "", string format = "{0} {1}") {
		
		if(name == "")
			name = id.ToString();
		
		if (_resources.ContainsKey(id)) {
			Debug.LogWarning("Resource '" + id + "' is already defined!");
			return null;
		}
		
		_resources.Add(id, new Resource(id, Mathf.Clamp(initamt, min, max), min, max, tooltip, name, format));
		
		if(is_public)
			_public.Add(_resources[id]);
		
		return _resources[id];
	}
	
	public string GetTooltip(string id) {
		return GetResource(id).tooltip;
	}
	
	public List<Resource> GetPublicResources() {
		return _public;
	}
	
	public Resource GetById(string id) {
		if(!_resources.ContainsKey(id)) {
			Debug.LogWarning("Couldn't find resource " + id);
			return null;
		}
		
		return _resources[id];
	}
	
	public int AddAmount (string id, int delta) {
		var resource = GetById(id);
		
		if(resource != null) {
			_resources[id].AddDelta(delta);
			return GetAmount(id);
		}
		
		return 0;
	}
	
	public int TakeAmount (string id, int delta) {
		return AddAmount(id, -delta);
	}
	
	public int SetAmount (string id, int amt) {
		var resource = GetById(id);
		
		if(resource != null) {
			_resources[id].SetAmount(amt);
			return GetAmount(id);
		}
		
		return 0;
	}
	
	public Resource GetResource(string id) {
		return GetById(id);
	}
	
	public int GetAmount (string id) {
		var resource = GetById(id);
		
		if(resource != null)
			return _resources[id].amount;
			
		return 0;
	}
	
	public int GetMaxAmount (string id) {
		var resource = GetById(id);
		
		if(resource != null)
			return _resources[id].max;
			
		return 0;
	}
	
	public int GetMinAmount (string id) {
		var resource = GetById(id);
		
		if(resource != null)
			return _resources[id].min;
			
		return 0;
	}
	
	public float GetCoef (string id) {
		var resource = GetById(id);
		
		if(resource != null)
			return (float)_resources[id].amount / (float)_resources[id].max;
			
		return 0;
	}
	
	public bool HasResource(string id, int amount = 1, EnumComparsionType operation = EnumComparsionType.MORE_EQUAL) {
		var resource = GetById(id);
		
		if(resource == null)
			return false;
			
		if(operation == EnumComparsionType.MORE_EQUAL)
			return resource.amount >= amount;
			
		if(operation == EnumComparsionType.MORE)
			return resource.amount > amount;
			
		if(operation == EnumComparsionType.LESS_EQUAL)
			return resource.amount <= amount;
			
		if(operation == EnumComparsionType.LESS)
			return resource.amount < amount;
			
		if(operation == EnumComparsionType.EQUAL)
			return resource.amount == amount;
		
		return false;
	}
	
	public List<Resource> GetAll() {
		var all_resources = new List<Resource>();
		
		foreach (string ResourceID in System.Enum.GetValues(typeof(string))) {
			all_resources.Add(_resources[ResourceID]);
		}
		
		return all_resources;
	}
	
	public int day {
		get { return GetAmount("day_number"); }
	}
	
}
