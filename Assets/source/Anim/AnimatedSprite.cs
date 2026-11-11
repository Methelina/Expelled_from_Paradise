using UnityEngine;
using System.Collections;

public delegate void OnAnimationComplete(AnimSequence anim);
public delegate void OnAnimationFrame(AnimSequence anim, int frame);

public class AnimatedSprite : MonoBehaviour {
	public AnimSequence[] sequences;
	public int initial_index;
	public bool isPlaying = false;
	public bool destroy = false;
	
	private AnimSequence currentSequence_;
	private float playtime_;
	private int currentIndex_ = -1;
	
	private OnAnimationComplete callback_;
	private int fixedFrame_ = -1; 
	
	public SpriteRenderer spriteRenderer;
	
	void Start () {
		if(spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
		
		if(initial_index >= 0 && sequences.Length > 0)
			PlayIndex(initial_index);
	}
	
	void FixedUpdate() {
		if(currentSequence_ == null) return;
		if(currentSequence_.playtime == 0) return;
		
		var sequence = currentSequence_;
		playtime_ += Time.fixedDeltaTime;
		
		if(!sequence.loop && playtime_ >= sequence.playtime) {
			playtime_ = 0f;
			if(callback_ != null) {
				callback_(sequence);
				callback_ = null;
			}
			currentSequence_ = null;
			isPlaying = false;
			if(destroy)
				Destroy(gameObject);
		}
		
		var index = (int)(playtime_/sequence.dt_frame) % sequence.id_sequence.Length;
		index = sequence.id_sequence[index] + sequence.bias;
		
		if(sequence.sortingLayer != "")
			spriteRenderer.sortingLayerName = sequence.sortingLayer;
			
		if(fixedFrame_ >= 0)
			index = fixedFrame_;
		
		spriteRenderer.sprite = sequence.sprites[index];
	}
	
	public void PlayFrame(AnimSequence sequence, int frame) {
		Play(sequence, true);
		fixedFrame_ = frame;
	}
	
	public void PlayIndex(int index, bool restart = false) {
		isPlaying = true;
		
		if(currentIndex_ == index && !restart)
			return;
			
		Play(sequences[index], restart);
		currentIndex_= index;
	}
	
	public void Play(AnimSequence sequence, bool restart = false, OnAnimationComplete callback = null) {
		isPlaying = true;

		if(sequence == currentSequence_ && !restart)
			return;
			
		if(callback_ != null)
			callback_(currentSequence_);
		
		currentSequence_ = sequence;
		playtime_ = restart ? 0f : playtime_;
		currentIndex_= -1;
		
		callback_ = callback;
		
		fixedFrame_ = -1;
	}
	
	public void Stop() {
		isPlaying = false;
	}
}
