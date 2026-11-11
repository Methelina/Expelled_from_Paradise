using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Condition {
	private List<Condition> _all;
	private List<Condition> _any;
	
	private string _resourceId;
	private int _resourceValue;
	private EnumComparsionType _comparsion;
	
	public Condition() {
		_all = new List<Condition>();
		_any = new List<Condition>();
		
		_resourceId = "nothing";
		_resourceValue = 0;
		_comparsion = EnumComparsionType.EQUAL;
	}
	
	public Condition AndHasResource(string id, int value = 1, EnumComparsionType comparsion = EnumComparsionType.MORE_EQUAL) {
		_all.Add(MakeResourceCondition(id, value, comparsion));
		
		return this;
	}
	
	public Condition OrHasResource(string id, int value = 1, EnumComparsionType comparsion = EnumComparsionType.MORE_EQUAL) {
		_any.Add(MakeResourceCondition(id, value, comparsion));
		
		return this;
	}
	
	public bool IsPassed(ResourceManager resources) {
		var and_passed = resources.HasResource(_resourceId, _resourceValue, _comparsion);
		
		for (var i = 0; i < _all.Count; i++) {
			var and_condition = _all[i];
			
			if (!and_condition.IsPassed(resources))
				return false;
		}
		
		var or_passed = _any.Count == 0;
		for (var i = 0; i < _any.Count; i++) {
			var or_condition = _any[i];
			
			if (or_condition.IsPassed(resources) && and_passed)
				or_passed = true;
		}
		
		return and_passed && or_passed;
	}
	
	public static Condition MakeResourceCondition(string id, int value = 0, EnumComparsionType comparsion = EnumComparsionType.MORE_EQUAL) {
		var subcondition = new Condition();
		subcondition._resourceId = id;
		subcondition._resourceValue = value;
		subcondition._comparsion = comparsion;
		
		return subcondition;
	}
	
}
