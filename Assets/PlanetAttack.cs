using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttack : MonoBehaviour {

    public static List<Transform> InvaderList;

    public static GameObject InvaderRed;
    public static GameObject InvaderBlue;
    public static GameObject InvaderGreen;
    public static GameObject InvaderYellow;

    // Use this for initialization
    void Start () {
        InvaderRed = GameObject.Find("InvaderRed");
        InvaderBlue = GameObject.Find("InvaderBlue");
        InvaderGreen = GameObject.Find("InvaderGreen");
        InvaderYellow = GameObject.Find("InvaderYellow");
        InvaderList = new List<Transform>();
    }

    public static void SpawnInvaders(GameObject attackingPlanet, GameObject defendingPlanet)
    {
        World attackingWorld = attackingPlanet.GetComponent<World>();
        Vector3 spawnCentre = attackingPlanet.transform.position;
        int attackingPopulation = attackingWorld.WorldPopulation;
        for(int i = 0; i < attackingPopulation; i++)
        {
            Vector3 pos = RandomCircle(spawnCentre, 3.5f * 0.5f);
            GameObject invader = Instantiate(InvaderRed, pos, Quaternion.identity);
            attackingWorld.WorldPopulation--;
            InvaderList.Add(invader.transform);
        }
    }

    public static void Attack(GameObject defendingPlanet)
    {
        foreach(Transform transform in InvaderList)
        {
            InvaderMovement invaderScript = transform.GetComponent<InvaderMovement>();
            invaderScript.TargetWorld = defendingPlanet;
            invaderScript.HomeWorld = PlanetSelection.SelectedPlanets[0];
            invaderScript.Attack = true;
        }
        InvaderList.Clear();//Clear List
    }

    static Vector3 RandomCircle(Vector3 spawnCentre, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = spawnCentre.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = spawnCentre.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = 0;
        return pos;
    }
}
