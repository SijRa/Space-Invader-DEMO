using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		/*First Example 
		We have 10 lootboxes, when user open one, he can win: 
		10 coins : chance - 50%
		25 coins : chance - 25%
		50 coins : chance - 15%
		75 coins : chance - 10%
		*/
		float[] chances = new float[4]{.5f,.25f,.15f,.1f};
		float[] values = new float[4]{10,25,50,75};
		int amountOfLootboxes = 10;
		float[] lootboxWin = DistributionManager.PolinomialSample(chances,values,amountOfLootboxes);
		for(int i = 0; i < lootboxWin.Length; i++)
		{
			//CoinManager.AddCoins(lootbosWin[i]);
			Debug.Log(lootboxWin[i] + " Win of " + (i+1).ToString() + " lootbox");
		}
		//End of first Example

		/* Example #2
		Again lootboxes (my poor imagination) 
		You have values & chances, and need to know how much user will win at average
		*/
		//I will use same chances and values, as in first example
		int amountOfSimulations = 10000;
		float[] lootboxWins = DistributionManager.PolinomialSample(chances,values,amountOfSimulations);
		float averageWin = DistributionManager.SampleMean(lootboxWin);
		Debug.Log(averageWin + " Average Win");

		//End of Example #2
	}
	

}
