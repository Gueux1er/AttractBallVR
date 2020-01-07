using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParameter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) // increase attraction force   A
        {
            ParameterManager.Instance.attractionForce += 0.5f;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three)) //reduce attraction force    X
        {
            ParameterManager.Instance.attractionForce -= 0.5f;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two)) //increase replusion force B
        {
            ParameterManager.Instance.repulsionForce += 0.5f;
        }

        if (OVRInput.GetDown(OVRInput.Button.Four)) //reduce attraction force    Y
        {
            ParameterManager.Instance.repulsionForce -= 0.5f;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.1f) //reduce handRadius
        {
            ParameterManager.Instance.handRadius -= Time.deltaTime;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.1f) //increase handRadius
        {
            ParameterManager.Instance.handRadius += Time.deltaTime;
        }
    }
}
