using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderControl : MonoBehaviour {

    public List<Transform> InvaderList;

    public GameObject InvaderRed;
    public GameObject InvaderBlue;
    public GameObject InvaderGreen;
    public GameObject InvaderYellow;
    public GameObject InvaderWhite;

    // Use this for initialization
    void Start () {
        InvaderList = new List<Transform>();
    }

    public void SpawnInvaders(GameObject attackingPlanet, GameObject defendingPlanet)
    {
        World attackingWorld = attackingPlanet.GetComponent<World>();
        Vector3 spawnCentre = attackingPlanet.transform.position;
        int attackingPopulation = attackingWorld.WorldPopulation;
        for(int i = 0; i < attackingPopulation; i++)
        {
            Vector3 pos = RandomCircle(spawnCentre, 3.5f * 0.5f);
            GameObject Invader;
            switch (attackingPlanet.tag)
            {
                case "Red":
                    Invader = InvaderRed;
                    break;
                case "Blue":
                    Invader = InvaderBlue;
                    break;
                case "Yellow":
                    Invader = InvaderYellow;
                    break;
                case "Green":
                    Invader = InvaderGreen;
                    break;
                case "White":
                    Invader = InvaderWhite;
                    break;
                default:
                    Debug.Log("UNKNOWN TAG");
                    Invader = null;
                    break;

            }

            GameObject invader = Instantiate(Invader, pos, Quaternion.identity);
            attackingWorld.WorldPopulation--;
            InvaderList.Add(invader.transform);
        }
    }

    public void Attack(GameObject attackingPlanet, GameObject defendingPlanet)
    {
        SpawnInvaders(attackingPlanet, defendingPlanet);//Spawn Invaders
        foreach(Transform transform in InvaderList)
        {
            InvaderMovement invaderScript = transform.GetComponent<InvaderMovement>();
            invaderScript.TargetWorld = defendingPlanet;
            invaderScript.HomeWorld = attackingPlanet;
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
