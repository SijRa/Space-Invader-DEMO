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
    }

    void Update()
    {
        PopulationCounter.text = WorldPopulation.ToString();
    }

    void ChangeAllegiance(string Tag)
    {
        gameObject.tag = Tag;
        Color temp = transform.GetComponent<SpriteRenderer>().color;
        temp = new Color(255,0,0,100);
        temp.a = 100;
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

    public void DecrementPopulation(string Tag)
    {
        this.Tag = Tag;
        if(transform.tag != Tag)
        {
            WorldPopulation--;
            if(WorldPopulation < 0)
            {
                ChangeAllegiance(Tag);
            }
        }
        else
        {
            WorldPopulation++;
        }
    }
}
