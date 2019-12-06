using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractRepulse : MonoBehaviour
{


    public enum ToolType
    {
        Attract, Repulse
    }

    [Header("Input parameters")]

    public Transform leftHand;
    public ToolType leftTool = ToolType.Attract;
    public Transform rightHand;
    public ToolType rightTool = ToolType.Repulse;


    private ParameterManager.ActionType forceType;
    private float handRadius = 8f;

    private float attractionForce = -10f;
    private float attractionSpeed = 1.0f;

    private float repulsionForce = 8f;
    private float repulsionSpeed = 1.0f;


    private void Start()
    {
        forceType = ParameterManager.Instance.forceType;
        handRadius = ParameterManager.Instance.handRadius;

        attractionForce = ParameterManager.Instance.attractionForce;
        attractionSpeed = ParameterManager.Instance.attractionSpeed;

        repulsionForce = ParameterManager.Instance.repulsionForce;
        repulsionSpeed = ParameterManager.Instance.repulsionSpeed;
    }

    private void Update()
    {
        


        if (OVRInput.GetDown(OVRInput.Button.One)) //A
        {
            if (rightTool == ToolType.Attract)
                rightTool = ToolType.Repulse;
            else if (rightTool == ToolType.Repulse)
                rightTool = ToolType.Attract;
        }

        if (OVRInput.GetDown(OVRInput.Button.Three)) //X
        {
            if(leftTool == ToolType.Attract)
                leftTool = ToolType.Repulse;
            else if (leftTool == ToolType.Repulse)
                leftTool = ToolType.Attract;
        }
        

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.1f) //left
        {
            CheckType(leftTool, leftHand, OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.1f) //right
        {
            CheckType(rightTool, rightHand, OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
        }
    }

    private void CheckType(ToolType tool, Transform hand, float axis)
    {
        switch (tool)
        {
            case ToolType.Attract:
                ActiveAttract(hand, axis);
                break;

            case ToolType.Repulse:
                ActiveRepulse(hand, axis);
                break;
        }
    }

    private void ReverseAttract()
    {
        attractionForce = -attractionForce;
    }

    private void ReverseRepulse()
    {
        repulsionForce = -repulsionForce;
    }

    private void ActiveAttract (Transform tr, float axis)
    {
        Rigidbody[] rbs;

        if (GetRigidbodiesInArea(tr.position, handRadius, out rbs))
        {
            switch (forceType)
            {
                case ParameterManager.ActionType.Translation:
                    AddTranslation(rbs, attractionSpeed, handRadius, tr.position);
                    break;


                case ParameterManager.ActionType.Force:
                    AddExplosionForce(rbs, attractionForce * axis, handRadius, tr.position); 
                    break;
            }
        }
    }

    private void ActiveRepulse (Transform tr, float axis)
    {

        Rigidbody[] rbs;

        if (GetRigidbodiesInArea(tr.position, handRadius, out rbs))
        {
            switch (forceType)
            {
                case ParameterManager.ActionType.Translation:
                    AddTranslation(rbs, repulsionSpeed, handRadius, tr.position);
                    break;


                case ParameterManager.ActionType.Force:
                    AddExplosionForce(rbs, repulsionForce * axis, handRadius, tr.position);
                    break;
            }
        }

        
    }

    private void AddExplosionForce(Rigidbody[] input, float value, float radius, Vector3 center)
    {
        if (input.Length == 0.0f)
            return;
        foreach (Rigidbody rb in input)
        {
            rb.AddExplosionForce(value, center, radius);
        }
    }

    private void AddTranslation(Rigidbody[] input, float value, float radius, Vector3 center)
    {
        if (input.Length == 0.0f)
            return;
        foreach(Rigidbody rb in input)
        {
            Vector3 dir = rb.transform.position - center;
            rb.transform.Translate(dir.normalized * value * Time.deltaTime); 
        }
    }

    private bool GetRigidbodiesInArea(Vector3 position, float radius, out Rigidbody[] result)
    {
        int layerId = LayerMask.NameToLayer("Movable");
        int layerMask = 1 << layerId;
        Collider[] cols = Physics.OverlapSphere(position, radius, layerMask);
        result = new Rigidbody[0];
        if (cols.Length == 0.0f)
            return false;
        result = new Rigidbody[cols.Length];
        for (int i = 0; i < cols.Length; ++i)
        {
            result[i] = cols[i].attachedRigidbody;
        }
        return true;
    }
}
