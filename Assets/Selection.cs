using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    public static List<GameObject> SelectedPlanets;

    public static int MaxSelected = 2;

    static List<string> PlayerTags;

    // Use this for initialization
    void Start()
    {
        SelectedPlanets = new List<GameObject>();
        PlayerTags = new List<string>();
        PlayerTags.Add("Red");
    }

    public static bool AddToSelection(GameObject selectedWorld)
    {
        //MAX SELCTION == 2
        if (SelectedPlanets.Count == 0 && PlayerTags.Contains(selectedWorld.tag))
        {
            SelectedPlanets.Add(selectedWorld);
            return true;
        }

        if (SelectedPlanets.Count == MaxSelected - 1)
        {
            //<attack function>
            InvaderControl invaderScript = selectedWorld.GetComponent<InvaderControl>();
            invaderScript.Attack(SelectedPlanets[0],selectedWorld);
            DeselectAll();
            return false;
        }

        return false;
    }

    public static bool Contains(GameObject selectedWorld)
    {
        if (SelectedPlanets.Contains(selectedWorld))
        {
            SelectedPlanets.Remove(selectedWorld);
            return true;
        }
        return false;
    }

    public static void DeselectAll()
    {
        foreach (GameObject gameObject in SelectedPlanets)
        {
            gameObject.GetComponent<PlayerSelectable>().Selected = false;
            if (!PlayerTags.Contains(gameObject.tag))
            {
                gameObject.tag = "White";
            }
            gameObject.transform.Find("GlowRed").gameObject.SetActive(false);
        }
        SelectedPlanets.Clear();
    }
}
