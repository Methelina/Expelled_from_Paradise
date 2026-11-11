using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFormatter {
	private const string AP_PriceFormat = "{0} action points";
	
	static UIFormatter() {
	}
	
	public static string AP_Price(int value) {
		return string.Format(AP_PriceFormat, value);
	}
	
	public static string FormatResourceCost(string id, int value) {
		return string.Format("{0} {1}", value, id);
	}

}
