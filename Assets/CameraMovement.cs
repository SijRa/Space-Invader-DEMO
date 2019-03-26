using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Vector2 TL, TR, BL, BR;

	// Use this for initialization
	void Start () {
        BL = new Vector2(-9, -11);
        BR = new Vector2(-9, 10);
        TR = new Vector2(7, 10);
        TL = new Vector2(7,-11);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
