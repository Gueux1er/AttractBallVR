using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedArea : MonoBehaviour
{
    [Header("Linked")]
    public List<Area> linkedArea = new List<Area>();

    public GameObject linkedAreaLink;

    private int countArea = 0;
    private bool instantiateLink = false;

    private bool isActivable = false;

    void Update()
    {
        if (!linkedArea[0].isActiveAndEnabled)
            return;

        if (!instantiateLink)
        {
            instantiateLink = true;
            StartLate();
        }

        if (isActivable == true && !IsActivable())
        {
            foreach (Area lArea in linkedArea)
            {
                StartCoroutine(lArea.SustainUnableActive(Area.ActiveState.ACTIVABLE_UNDER));
            }
        }

        isActivable = IsActivable();
    }

    public bool IsActivable()
    {
        int i = 0;
        foreach (Area lArea in linkedArea)
        {
            if (lArea.isActivable)
                i++;
        }
        return (i==countArea) ? true : false;
    }

    public void PingAreas()
    {
        foreach (Area lArea in linkedArea)
        {
            lArea.movableCount = lArea.movableCount;
        }
    }

    void StartLate()
    {
        foreach (Area lArea in linkedArea)
        {
            lArea.isLinked = true;
            lArea.linkedArea = this;
            countArea++;

            foreach (Area lAreaBis in linkedArea)
            {
                if (lArea != lAreaBis)
                {
                    GameObject line = Instantiate(linkedAreaLink);
                    line.GetComponent<LineLink>().a = lArea.gameObject;
                    line.GetComponent<LineLink>().b = lAreaBis.gameObject;
                }
            }
        }
    }
}
