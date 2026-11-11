using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimSequence {
	public Sprite[] sprites;
	public string sequence;
	public float fps;
	public int bias;
	
	public bool loop = true;
	
	public int priority = 0;
	
	public string sortingLayer = "";
	
	private int[] raw_sequence_;
	public int[] id_sequence {
		get { 
			if(raw_sequence_ == null && sequence.Length > 0) {
				var splt = sequence.Split(',');
				var ints = new int[splt.Length];
				
				for(var i = 0; i < splt.Length; i++)
					ints[i] = int.Parse(splt[i]);
				
				raw_sequence_ = ints;
			}
			
			return raw_sequence_ != null ? raw_sequence_ : new int[0];
		 }
	}
	
	public float playtime {
		get {
			return id_sequence.Length == 0 ? 0 : id_sequence.Length * dt_frame;
		}
	}
	
	public float dt_frame {
		get {
			return 1f / fps;
		}
	}
	
}
