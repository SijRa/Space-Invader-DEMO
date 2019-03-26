using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    List<Vector2> OccupiedSpaces;

    public List<GameObject> Worlds;
    //Reference for world prefab
    public GameObject World;

    Vector2 PreviousA1;
    Vector2 PreviousA2;
    Vector2 PreviousB1;
    Vector2 PreviousB2;

    float xBoundary = 7.5f;
    float yBoundary = 7.5f;

    int MinimumWorlds = 10;
    int MaxiumumWorlds = 20;

    public float[] WorldSizes;
    float MeanSize = 1.0f;
    float SizeDeviation = 0.3f;

    // Use this for initialization
    void Start() {
        Worlds = new List<GameObject>();
        OccupiedSpaces = new List<Vector2>();
        WorldSizes = NormalDistSample(MeanSize, SizeDeviation, Random.Range(MinimumWorlds,MaxiumumWorlds)); //LEVEL BEHAVIOUR
        //GenerateWorlds(); //start level
    }


    /// <summary>
    /// Create normal distribution for world sizes
    /// </summary>
    /// <param name="mean">Mean size</param>
    /// <param name="standartDevitation">Size deviation</param>
    /// <param name="sampleLength">Number of worlds</param>
    /// <returns></returns>
    public float[] NormalDistSample(float mean, float standartDevitation, int sampleLength)
    {
        float[] sample = new float[sampleLength];
        for (int i = 0; i < sample.Length; i++)
        {
            float u1 = Random.Range(0f, 1f);
            float u2 = Random.Range(0f, 1f);
            sample[i] = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Sin(Mathf.PI * 2 * u2) * Mathf.Sqrt(standartDevitation) + mean;
        }
        return sample;
    }

    // Update is called once per frame
    void Update() {
		
	}

    //void GenerateWorlds()
    //{
    //    int id = 0;
    //    foreach(float size in WorldSizes)
    //    {
    //        World = Instantiate(World);
    //        World.name = "World" + id;
    //        World.transform.localScale = new Vector2(size, size);
    //        World.transform.position = new Vector2(Random.Range(-xBoundary,xBoundary),Random.Range(-yBoundary,yBoundary));
    //        id++;
    //        //Instantiate(World);
    //    }
    //}

    //void GenerateWorlds()
    //{
    //    int sectionCounter = 0;
    //    for (int i = 0; i < WorldSizes.Length; i++)
    //    {
    //        sectionCounter++;
    //        GenerateWorld(xBoundary, yBoundary, sectionCounter, i);
    //        if (sectionCounter == 4)
    //        {
    //            sectionCounter = 1;
    //        }
    //    }
    //}

    //void GenerateWorld(float xBoundary, float yBoundary, int section, int worldCount)
    //{
    //    Vector2 worldPosition;
    //    Debug.Log("Spawning world..");
    //    switch (section)
    //    {
    //        case 1:
    //            worldPosition = new Vector2(Random.Range(-xBoundary, yBoundary), Random.Range(xBoundary, -yBoundary));
    //            PreviousA1 = worldPosition;
    //            if (transform.GetComponent<)
    //            {
    //                // Target is within range.
    //                Debug.Log("Spawn World!");
    //                GameObject currentWorld = Instantiate(World, worldPosition, Quaternion.identity);
    //                currentWorld.transform.localScale = new Vector3(WorldSizes[worldCount], WorldSizes[worldCount], WorldSizes[worldCount]);
    //                OccupiedSpaces.Add(worldPosition);
    //            }
    //            break;
    //        case 2:
    //            worldPosition = new Vector2(Random.Range(-xBoundary, yBoundary), Random.Range(xBoundary, -yBoundary));
    //            PreviousA2 = worldPosition;
    //            if (!Physics2D.OverlapArea(worldPosition, PreviousA2))
    //            {
    //                // Target is within range.
    //                Debug.Log("Spawn World!");
    //                GameObject currentWorld = Instantiate(World, worldPosition, Quaternion.identity);
    //                currentWorld.transform.localScale = new Vector3(WorldSizes[worldCount], WorldSizes[worldCount], WorldSizes[worldCount]);
    //                OccupiedSpaces.Add(worldPosition);
    //            }
    //            break;
    //        case 3:
    //            worldPosition = new Vector2(Random.Range(-xBoundary, yBoundary), Random.Range(xBoundary, -yBoundary));
    //            PreviousB1 = worldPosition;
    //            if (!Physics2D.OverlapArea(worldPosition, PreviousB1))
    //            {
    //                // Target is within range.
    //                Debug.Log("Spawn World!");
    //                GameObject currentWorld = Instantiate(World, worldPosition, Quaternion.identity);
    //                currentWorld.transform.localScale = new Vector3(WorldSizes[worldCount], WorldSizes[worldCount], WorldSizes[worldCount]);
    //                OccupiedSpaces.Add(worldPosition);
    //            }
    //            break;
    //        case 4:
    //            worldPosition = new Vector2(Random.Range(-xBoundary, yBoundary), Random.Range(xBoundary, -yBoundary));
    //            PreviousB2 = worldPosition;
    //            if (!Physics2D.OverlapArea(worldPosition, PreviousB2))
    //            {
    //                // Target is within range.
    //                Debug.Log("Spawn World!");
    //                GameObject currentWorld = Instantiate(World, worldPosition, Quaternion.identity);
    //                currentWorld.transform.localScale = new Vector3(WorldSizes[worldCount], WorldSizes[worldCount], WorldSizes[worldCount]);
    //                OccupiedSpaces.Add(worldPosition);
    //            }
    //            break;
    //    }
    //}
}
