using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class World : MonoBehaviour {

    public int WorldPopulation = 0;

    public int GrowthFactor = 1000;
    public int PopulationLimit;

    TextMeshPro PopulationCounter;

    // Use this for initialization
    void Start () {
        float populationRate = gameObject.transform.localScale.x;//Can also use y axis
        PopulationCounter = transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StartPopulating(populationRate));
	}

    IEnumerator StartPopulating(float populationRate)
    {
        PopulationLimit = (int)Mathf.Round(transform.localScale.x)*GrowthFactor;
        while (true)
        {
            if(WorldPopulation < PopulationLimit)
            {
                WorldPopulation += (int)populationRate;
                PopulationCounter.text = WorldPopulation.ToString();
            }
            yield return new WaitForSeconds(1);       
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
