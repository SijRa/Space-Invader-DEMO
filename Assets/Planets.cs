using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planets : MonoBehaviour {


    public static List<Transform> RedPlanets;
    public static List<Transform> BluePlanets;
    public static List<Transform> GreenPlanets;
    public static List<Transform> YellowPlanets;

    public static List<Transform> PlanetList;

    // Use this for initialization
    void Start () {
    }

    void Awake()
    {
        PlanetList = new List<Transform>();
        RedPlanets = new List<Transform>();
        BluePlanets = new List<Transform>();
        GreenPlanets = new List<Transform>();
        YellowPlanets = new List<Transform>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
