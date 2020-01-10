using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SequenceRenderer : MonoBehaviour
{
    public Transform point0;
    public Transform point1;
    // Start is called before the first frame update
    void Start()
    {
        if (point0 != null && point1 != null)
            SetLineRendererPoints(point0.position, point1.position);
    }

    public void SetLineRendererPoints(Vector3 point0, Vector3 point1)
    {
        var rend = GetComponent<LineRenderer>();

        rend.positionCount = 2;
        rend.SetPosition(0, point0);
        rend.SetPosition(1, point1);
    }
}
