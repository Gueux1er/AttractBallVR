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

    void Update()
    {
        if (!linkedArea[0].isActiveAndEnabled)
            return;

        if (!instantiateLink)
        {
            instantiateLink = true;
            StartLate();
        }

        if (!IsLocked())
        {
            foreach (Area lArea in linkedArea)
            {
                lArea.isLinked = false;
                lArea.movableCount = lArea.movableCount;
            }
        }
        else
        {
            foreach (Area lArea in linkedArea)
            {
                lArea.isLinked = true;
                lArea.movableCount = lArea.movableCount;
            }
        }
    }

    public bool IsLocked()
    {
        int i = 0;
        foreach (Area lArea in linkedArea)
        {
            if (lArea.activeState == Area.ActiveState.ACTIVE)
                i++;
        }
        return (i==countArea) ? false : true;
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
