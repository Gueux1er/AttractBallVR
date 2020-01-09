using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bambou_Set : MonoBehaviour
{
 
    void Awake()
    {
        transform.rotation = new Quaternion(transform.rotation.x, Random.Range(0.0f, 359.0f), transform.rotation.z, Random.Range(0.0f, 359.0f));
        transform.localScale = new Vector3(Random.Range(0.8f, 1.3f), Random.Range(0.8f, 1.3f), Random.Range(0.8f, 1.3f));
    }
    void OnEnable()
    {
        transform.rotation = new Quaternion(transform.rotation.x, Random.Range(0.0f, 359.0f), transform.rotation.z, Random.Range(0.0f, 359.0f));
        transform.localScale = new Vector3(Random.Range(0.8f, 1.3f), Random.Range(0.8f, 1.3f), Random.Range(0.8f, 1.3f));
    }

}
