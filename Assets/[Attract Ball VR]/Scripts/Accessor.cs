using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessor : MonoBehaviour
{
    private bool right = false;
    private bool left = false;
    private float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(right || left)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftControllerAnchor")
        {
            left = true;
        }
        if (other.gameObject.name == "RightControllerAnchor")
        {
            right = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LeftControllerAnchor")
        {
            left = false;
        }
        if (other.gameObject.name == "RightControllerAnchor")
        {
            right = false;
        }
    }
}
