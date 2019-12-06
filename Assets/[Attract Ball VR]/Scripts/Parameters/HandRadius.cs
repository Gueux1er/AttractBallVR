using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRadius : MonoBehaviour
{
    public Transform follow;

    void Update()
    {
        transform.position = follow.position;
        float f = ParameterManager.Instance.handRadius * 2;
        transform.localScale = new Vector3(f,f,f);
    }
}
