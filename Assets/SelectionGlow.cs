using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionGlow : MonoBehaviour
{
    public GameObject GlowObject;

    private void Start()
    {
        GlowObject = Instantiate(GlowObject);

        Transform parentTransform = GetComponentInParent<Transform>();
        GlowObject.transform.localPosition = parentTransform.localPosition;


        float GlowScaleFactor = parentTransform.localScale.x;
        Vector2 newScale = new Vector2(parentTransform.localScale.x - GlowScaleFactor, parentTransform.localScale.y - GlowScaleFactor);

        GlowObject.transform.localScale = newScale;
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
