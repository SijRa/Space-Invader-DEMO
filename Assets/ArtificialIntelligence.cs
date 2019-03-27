using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour {

    List<World> FriendlyWorldList;
    List<World> HostileWorldList;
    public GameObject Worlds;
    
    // Use this for initialization
    void Start () {
        HostileWorldList = new List<World>();
        foreach (World world in Worlds.GetComponentsInChildren<World>())
        {
            if (world.transform.name != transform.name)
            {
                HostileWorldList.Add(world);
            }
        }
        foreach (World world in transform.parent.GetComponentsInChildren<World>())
        {
            if(world.transform.name != transform.name)
            {
                HostileWorldList.Add(world);
            }
        }
        FriendlyWorldList.Add(GetComponent<World>());
    }

    // Update is called once per frame
    void Update () {
		
	}
    IEnumerator StartPlaying()
    {
        while (true)
        {
            //Wait
            //Attack
            //Reinforce
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }

    IEnumerator Attack()
    {
        //Transform World = ClosestWorld(HostileWorldList.ToArray());
        //World targetWorldScript = World.GetComponent<World>();
        //if (targetWorldScript.WorldPopulation < transform.GetComponent<World>().WorldPopulation)
        //{

        //}
        yield return null;
    }

    IEnumerator Reinforce()
    {
        yield return null;
    }

    Transform ClosestWorld(Transform[] targetList)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in targetList)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
