using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLink : MonoBehaviour
{
    [HideInInspector] public GameObject a;
    [HideInInspector] public GameObject b;

    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPositions(new Vector3[] {a.transform.position, b.transform.position});
    }
}
