using UnityEngine;
using System.Collections;

public class ChanceUtil {

	public static bool Roll(float chance) {
		if (chance <  0 || chance > 1)
			Debug.LogWarning("weird chance value " + chance);
	
		var random = Random.Range(0f, 1f);
		
		return random < chance;
	}
	
	public static int Dice(int sides, int throws) {
		int result = 0;
		
		for(var i = 0; i < throws; i++)
			result += Random.Range(1, sides + 1);
			
		return result;
	}
	
	public static bool DiceChance(int sides, int throws, int check) {
		return Dice(sides, throws) >= check;
	}

}
