using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRadius : MonoBehaviour
{
    public Transform follow;

    void Update()
    {
        float f = ParameterManager.Instance.handRadius * 2;
        transform.position = follow.position + (follow.forward * ParameterManager.Instance.handRadius);
        transform.localScale = new Vector3(f,f,f);
    }
}
