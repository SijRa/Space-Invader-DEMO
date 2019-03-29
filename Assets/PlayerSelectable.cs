using UnityEngine;

public class PlayerSelectable : MonoBehaviour
{
    public GameObject GlowObject;

    public bool Selected = false;

    // Use this for initialization
    void Start()
    {
        GlowObject = transform.Find("GlowRed").gameObject;
        GlowObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!Selected)
        {
            if(Selection.AddToSelection(gameObject))
            {
                GlowObject.SetActive(true);
                Selected = true;
            }
        }
        else if(Selected)
        {
            if(Selection.Contains(gameObject))
            {
                GlowObject.SetActive(false);
                Selected = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        if(!Selected)
        {
            GlowObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if(!Selected)
        {
            GlowObject.SetActive(false);
        }
    }
}
