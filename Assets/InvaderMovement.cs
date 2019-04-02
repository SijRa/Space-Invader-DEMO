using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMovement : MonoBehaviour {

    public GameObject TargetWorld;
    public GameObject HomeWorld;
    public bool Attack;

    float Speed;
    float SpeedMinimum = 2f;
    float SpeedMaximum = 5f;

    void Start()
    {
        Speed = Random.Range(SpeedMinimum, SpeedMaximum);
    }
	
	// Update is called once per frame
	void Update ()
    {
        float step = Speed * Time.deltaTime; // calculate distance to move
        if(Attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetWorld.transform.position, step);
        }

        if(TargetWorld != null)
        {
            if (transform.position == TargetWorld.transform.position)
            {
                string tagCopy = HomeWorld.tag;
                TargetWorld.GetComponent<World>().DecrementPopulation(HomeWorld,tagCopy);

                Destroy(gameObject);
            }
        }
    }
}
