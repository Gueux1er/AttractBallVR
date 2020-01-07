using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasArea : MonoBehaviour
{
    public GameObject canvas;
    public GameObject cameraRig;

    public TMP_Text minValueText;
    public TMP_Text maxValueText;
    public TMP_Text currentValueText;
    void Start()
    {
        
    }

    void Update()
    {
        canvas.transform.LookAt(2 * transform.position - cameraRig.transform.position);        
    }
}
