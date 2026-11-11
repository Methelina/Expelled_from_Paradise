using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChanceList<T>  {

	private List<ChanceItem<T>> _items = new List<ChanceItem<T>>();
	
	public void Add (T item, float weight, int amount = 1, int drop_count = 1) {
		_items.Add (new ChanceItem<T>(item, weight, amount, drop_count));
	}
	
	public bool CanRoll () {
		return _items.Count > 0;
	}
	
	public ChanceItem<T> Roll () {
		if (!CanRoll()) {
			Debug.LogError("Add some items before rolling this shit.");
			
			return default(ChanceItem<T>);
		}
	
		float total_weight = 0;
		
		for (var i = 0; i < _items.Count; i++) {
				if (_items[i].amount > 0)
					total_weight += _items[i].weight;
		}
		
		int safety_lock = 100000;
		int c = 0;
		float dice = Random.Range(0f, total_weight);
		
		while (c < safety_lock) {
			ChanceItem<T> item = _items[(int)Mathf.Repeat(c, _items.Count)];
			c++;
			
			if (item.amount <= 0)
				continue;
			
			dice -= item.weight;
			
			if (dice <= 0) {
				item.amount--;
				
				return item;
			}
		}
		
		if(c == safety_lock) {
			Debug.LogWarning("Something went horribly wrong with this shit");
			return _items[0];
		}
		
		return default(ChanceItem<T>);
	}

}

public class ChanceItem<T> {
	public int amount;
	public float weight;
	public T content;
	public int drop_count;
	
	public ChanceItem (T content_, float weight_, int amount_, int drop_count_) {
		amount = amount_;
		content = content_;
		weight = weight_;
		drop_count = drop_count_;
	}
}