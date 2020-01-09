using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientMaterialFixed : MonoBehaviour
{
    public Gradient gradient;
    private Material mat;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().materials[index];
        mat.color = gradient.Evaluate(Random.Range(0.0f, 1.0f));
    }

}