using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRim : MonoBehaviour
{
    public float maxSpeed = 3.5f;
    public float minRimWidthValue = 2.4f;
    public float maxRimWidthValue = 0.5f;
    Renderer rend;
    Rigidbody rb;

    float tmpValue;
    float value;
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float m = Mathf.Clamp(rb.velocity.magnitude, 0, maxSpeed);
        value = minRimWidthValue - ((minRimWidthValue - maxRimWidthValue) / maxSpeed) * m;
        if (rend != null && EnableChangeRend())
        {
            tmpValue = value;
            rend.material.SetFloat("_RimWidth", maxSpeed - (((minRimWidthValue - maxRimWidthValue) /maxSpeed) * rb.velocity.magnitude));
        }
    }
    private bool EnableChangeRend()
    {
        if (value < tmpValue - 0.1f || value > tmpValue + 0.1f)
            return true;
        return false;
    }
}
