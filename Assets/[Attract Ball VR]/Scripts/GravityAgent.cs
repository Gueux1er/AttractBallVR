using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAgent : MonoBehaviour
{
    private Rigidbody rb;
    private ConstantForce cf;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.AddComponent<ConstantForce>();
        cf = GetComponent<ConstantForce>();
    }

    public void ReverseGravity(bool gravityReversed)
    {
        if (gravityReversed)
        {
            rb.useGravity = false;
            
            cf.force = -Physics.gravity;
        }
        else
        {
            rb.useGravity = true;

            cf.force = Vector3.zero;
        }
    }
}
