using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour {
    public Transform[] HostileWorldList;
    public List<Transform> FriendlyWorldList;
    public GameObject Worlds;

    int WaitMinimum = 1;
    int WaitMaximum = 10;

    float TargetChangeProbability = 0.4f;

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
        List<int> unknownTags = new List<int>();
        int counter = 0;
        foreach (var world in HostileWorldList)
        {
            if(world.tag == "Untagged")//Non-playing objects
            {
                unknownTags.Add(counter);
            }
            else if(world.tag == gameObject.tag)//Exclude self
            {
                unknownTags.Add(counter);
            }
            counter++;
        }
        foreach (int index in unknownTags)
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
        if (transform.tag != "Red")
        {
            while (true)
            {
                int waitTime = Random.Range(WaitMinimum, WaitMaximum);
                //Debug.Log(transform.name + " Wait-Time: " + waitTime);
                int randomNum = Random.Range(1, 3);
                switch (randomNum)
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
    }

    IEnumerator Attack(Transform[] _targetList)
    {
        Transform[] Worlds = ClosestWorld(_targetList,false);
        World WorldScript = null;
        if(Worlds.Length == 1)
        {
            WorldScript = Worlds[0].GetComponent<World>();
        }
        else
        {
            foreach (var world in Worlds)
            {
                if(GetComponent<World>().WorldPopulation > world.GetComponent<World>().WorldPopulation)
                {
                    WorldScript = world.GetComponent<World>();
                }
            }
        }
        if (WorldScript.WorldPopulation < GetComponent<World>().WorldPopulation)
        {
            Debug.Log(transform.name + " Moving to " + Worlds[0].name);
            //attack
            InvaderControl invaderScript = transform.GetComponent<InvaderControl>();
            invaderScript.Attack(gameObject,Worlds[0].gameObject);
        }
        Debug.Log(transform.name + " cannot attack");
        yield return null;
    }

    Transform[] ClosestWorld(Transform[] targetList, bool neutralWorlds)
    {
        Transform[] bestTarget = new Transform[1];
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        Transform[] secondBest = new Transform[1];
        List<Transform> PlanetList = new List<Transform>();
        foreach (Transform potentialTarget in targetList)
        {
            if(potentialTarget != null)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    secondBest[0] = potentialTarget;
                    bestTarget[0] = potentialTarget;
                    if(neutralWorlds)
                    {
                        PlanetList.Add(potentialTarget);
                    }
                }
            }
        }
        Transform[] PlanetListArray = PlanetList.ToArray();
        int randomCount = Random.Range(PlanetList.Count-5,PlanetList.Count-2);//SELECT CLOSEST LAST 3 - 5 WORLDS
        List<Transform> OtherPlanets = new List<Transform>();
        for (int i = PlanetList.Count - randomCount; i < PlanetList.Count - 2; i++)
        {
            OtherPlanets.Add(PlanetListArray[i]);
        }
        //PRODUCT OF MARAJUANA
        int randomNum = Random.Range(1,9);
        float randomFloat = 1 - (randomNum / 100);
        if (randomFloat < TargetChangeProbability)
        {
            return secondBest;
        }
        else if(randomFloat < 0.3f)
        {
            return OtherPlanets.ToArray();
        }
        //PRODUCT OF MARAJUANA

        return bestTarget;
    }
}
