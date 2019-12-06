using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParameter : MonoBehaviour
{
    public TMPro.TMP_Text attForce; 
    public TMPro.TMP_Text repForce; 
    public TMPro.TMP_Text handRadius; 

    void Start()
    {
        
    }

    void Update()
    {
        attForce.text = ParameterManager.Instance.attractionForce.ToString();
        repForce.text = ParameterManager.Instance.repulsionForce.ToString();
        handRadius.text = ParameterManager.Instance.handRadius.ToString();
    }
}
