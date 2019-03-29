using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour {
    public Transform[] HostileWorldList;
    public List<Transform> FriendlyWorldList;
    public GameObject Worlds;

    int WaitMinimum = 1;
    int WaitMaximum = 10;
    
    // Use this for initialization
    void Start () {
        if (Worlds == null)
        {
            Worlds = transform.parent.gameObject;
        }
        GameObject LevelManager = GameObject.Find("LevelManager");
        HostileWorldList = LevelManager.GetComponentsInChildren<Transform>();
        CleanHostileList();
        CheckIfNPC();//destory ai script if non-npc
        StartCoroutine(StartPlaying());//PLAY
    }

    void CleanHostileList()
    {
        List<string> PlayableWorlds = new List<string>(new string[] {});
        List<int> SameTagList = new List<int>();
        int counter = 0;
        foreach (var world in HostileWorldList)
        {
            if (world.tag == gameObject.tag)
            {
                SameTagList.Add(counter);
            }
            if(world.tag != "Red")
            {

            }
            counter++;
        }
        foreach (int index in SameTagList)
        {
            HostileWorldList[index] = null;
        }
    }

    void CheckIfNPC()
    {
        if (transform.tag == "Red")
        {
            Destroy(gameObject.GetComponent<ArtificialIntelligence>());
        }
    }

    public void UpdateWorldLists()
    {
        //foreach (Transform world in Planets.PlanetList)
        //{
        //    if (gameObject.tag == world.tag)
        //    {
        //        FriendlyWorldList.Add(world.transform);
        //    }
        //    else
        //    {
        //        HostileWorldList.Add(world.transform);
        //    }
        //}
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator StartPlaying()
    {
        Debug.Log(transform.name + " Started Playing");
        while (true)
        {
            int waitTime = Random.Range(WaitMinimum,WaitMaximum);
            //Debug.Log(transform.name + " Wait-Time: " + waitTime);
            int randomNum = Random.Range(1, 3);
            switch(randomNum)
            {
                case 1:
                    //Attack
                    Debug.Log(transform.name + " attacking");
                    StartCoroutine(Attack(HostileWorldList));
                    break;
                case 2:
                    //reinforce
                    Debug.Log(transform.name + " defending");

                    switch (gameObject.tag)
                    {
                        case "Red":
                            FriendlyWorldList = Planets.RedPlanets;
                            break;
                        case "Blue":
                            FriendlyWorldList = Planets.BluePlanets;
                            break;
                        case "Yellow":
                            FriendlyWorldList = Planets.YellowPlanets;
                            break;
                        case "Green":
                            FriendlyWorldList = Planets.GreenPlanets;
                            break;
                    }
                    if (FriendlyWorldList.Count > 1)
                    {
                        StartCoroutine(Attack(FriendlyWorldList.ToArray()));
                    }
                    break;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator Attack(Transform[] _targetList)
    {
        Transform World = ClosestWorld(_targetList);
        World WorldScript = World.GetComponent<World>();
        if (WorldScript.WorldPopulation < GetComponent<World>().WorldPopulation)
        {
            Debug.Log(transform.name + " Moving to " + World.name);
            //attack
            InvaderControl invaderScript = transform.GetComponent<InvaderControl>();
            invaderScript.Attack(gameObject,World.gameObject);
        }
        Debug.Log(transform.name + " cannot attack");
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
