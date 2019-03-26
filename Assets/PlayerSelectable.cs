using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectable : MonoBehaviour {

    public GameObject GlowObject;

	// Use this for initialization
	void Start () {
        GlowObject = transform.Find("GlowRed").gameObject;
        //Debug.Log(GlowObject.name);
        GlowObject.SetActive(false);
	}

    private void OnMouseEnter()
    {
        GlowObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        GlowObject.SetActive(false);
    }
}
