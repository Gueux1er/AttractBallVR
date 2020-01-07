using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedArea : MonoBehaviour
{
    [Header("Linked")]
    public List<Area> linkedArea = new List<Area>();

    public GameObject linkedAreaLink;

    private int countArea = 0;
    void Start()
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

    void Update()
    {
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
}
