using UnityEngine;
using System.Collections;

public class UIMessages : GameEventReciever {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	override public void RecieveEvent(EnumGameEvent evt) {
		switch(evt) {
			case EnumGameEvent.FLOOR_CLEAR: 
				FloorClear();
			break;
			case EnumGameEvent.LEVEL_COMPLETE: 
				LevelClear();
			break;
		}
	}
	
	public void FloorClear () {
		SRResources.UI.stage_clear.Instantiate().transform.SetParent(((Canvas)(GameObject.FindObjectOfType(typeof(Canvas)))).transform, false);
		SoundManager.PlaySound(SoundManager.sounds.StageClear, true, 0.5f);
	}
	
	public void LevelClear () {
		SRResources.UI.level_clear.Instantiate().transform.SetParent(((Canvas)(GameObject.FindObjectOfType(typeof(Canvas)))).transform, false);
		SoundManager.PlaySound(SoundManager.sounds.StageClear, true, 0.5f);
	}
}
