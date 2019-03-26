using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerClickHandler {

    LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Down");
        lineRenderer.SetPosition(0, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Up");
        lineRenderer.SetPosition(1, eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }
}
