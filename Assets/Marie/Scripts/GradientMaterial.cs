using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientMaterial : MonoBehaviour
{
    public Gradient gradient;
    public float speedMin, speedMax;
    private Material mat;
    private float speed;
    private float index;
    private bool up = true;

    void Start()
    {
        speed = Random.Range(speedMin, speedMax);
        mat = this.GetComponent<MeshRenderer>().material;
        index = Random.Range(0.0f, 1.0f);
        mat.color = gradient.Evaluate(index);
    }

    void Update()
    {
        if (up)
        {
            index += Time.deltaTime / speed;
            if (index > 1)
            {
                index = 1;
                up = false;
            }
        }
        else
        {
            index -= Time.deltaTime / speed;
            if (index < 0)
            {
                index = 0;
                up = true;
            }
        }

        mat.color = gradient.Evaluate(index);
    }
}
