using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasableMovable : MonoBehaviour
{
    //public bool UnstaseOnCollision = true;

    //private Rigidbody rb;

    public void Stase()
    {
        //rb.velocity = Vector3.zero;
        //rb.isKinematic = true;
    }

    public void Unstase()
    {
        //rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (UnstaseOnCollision)
        //    Unstase();
    }
}
