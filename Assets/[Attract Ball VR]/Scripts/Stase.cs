using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Stase : MonoBehaviour
{

    [Header("Local")]
    public Transform[] referencePoints;
    public float radiusOfEffect;

    private GameObject[] stasables;

    private void Start()
    {
        stasables = GameObject.FindGameObjectsWithTag("Movable");
    }

    public void ActivateStase(StaseRange range)
    {
        switch (range)
        {
            case StaseRange.Global:
                foreach(GameObject s in stasables)
                {
                    s.GetComponent<StasableMovable>().Stase();
                }
                break;

            case StaseRange.Local:
                foreach(Transform tr in referencePoints)
                {
                    int layerId = LayerMask.NameToLayer("Movable");
                    int layerMask = 1 << layerId;
                    Collider[] cols = Physics.OverlapSphere(tr.position, radiusOfEffect, layerMask);
                    foreach(Collider c in cols)
                    {
                        c.GetComponent<StasableMovable>().Stase();
                    }
                }
                break;
        }
    }



    public enum StaseRange
    {
        Local, Global
    }
}
