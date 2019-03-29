using System.Collections;
using TMPro;
using UnityEngine;
public class World : MonoBehaviour
{
    public int WorldPopulation = 0;

    public int GrowthFactor = 1000;

    public int PopulationLimit;

    TextMeshPro PopulationCounter;

    Color WorldColour;

    //public Transform TargetWorld;

    Color Red, Blue, Green, Yellow;

    string Tag;

    // Use this for initialization
    void Start()
    {
        WorldColour = GetComponent<SpriteRenderer>().color;
        float populationRate = gameObject.transform.localScale.x;//Can also use y axis
        PopulationCounter = transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StartPopulating(populationRate));

        Red = new Color(255,0,0);
        Blue = new Color(0,0,255);
        Green = new Color(0, 255, 0);
        Yellow = new Color(255, 255, 0);

        if(transform.parent.name != "HomeWorlds")
        {
            transform.tag = "White";
        }

        World worldScript = GetComponent<World>();
        
        switch (transform.tag)
        {
            case "Red":
                Planets.RedPlanets.Add(transform);
                break;
            case "Blue":
                Planets.BluePlanets.Add(transform);
                break;
            case "Yellow":
                Planets.YellowPlanets.Add(transform);
                break;
            case "Green":
                Planets.GreenPlanets.Add(transform);
                break;
        }

        //Planets.PlanetList.Add(transform);
    }

    void Update()
    {
        PopulationCounter.text = WorldPopulation.ToString();
    }

    void ChangeAllegiance(GameObject _HomeWorld)
    {
        gameObject.tag = _HomeWorld.tag;
        Color temp = transform.GetComponent<SpriteRenderer>().color;

        switch (_HomeWorld.tag)
        {
            case "Red":
                temp = Red;
                temp.a = 100;
                Planets.RedPlanets.Add(_HomeWorld.transform);
                break;
            case "Blue":
                temp = Blue;
                temp.a = 80;
                Planets.BluePlanets.Add(_HomeWorld.transform);
                break;
            case "Yellow":
                temp = Yellow;
                temp.a = 170;
                Planets.YellowPlanets.Add(_HomeWorld.transform);
                break;
            case "Green":
                temp = Green;
                temp.a = 100;
                Planets.GreenPlanets.Add(_HomeWorld.transform);
                break;
        }
        var aiScript =_HomeWorld.GetComponent<ArtificialIntelligence>();
        if(aiScript)
        {
            aiScript.UpdateWorldLists();
        }
        transform.GetComponent<SpriteRenderer>().color = temp;

        //Color tmp = Thugger.GetComponent<SpriteRenderer>().color;
        //tmp.a = 0f;
        //Thugger.GetComponent<SpriteRenderer>().color = tmp;
    }

    IEnumerator StartPopulating(float populationRate)
    {
        PopulationLimit = (int)Mathf.Round(transform.localScale.x) * GrowthFactor;
        while (true)
        {
            if (WorldPopulation < PopulationLimit)
            {
                WorldPopulation += (int)populationRate;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public void DecrementPopulation(GameObject _HomeWorld)
    {
        if(transform.tag != _HomeWorld.tag)
        {
            WorldPopulation--;
            if(WorldPopulation < 0)
            {
                ChangeAllegiance(_HomeWorld);
                //give ai powers
                if(Tag != "Red")
                {
                    var aiScript = gameObject.AddComponent<ArtificialIntelligence>();
                }
            }
        }
        else
        {
            WorldPopulation++;
        }
    }
}
