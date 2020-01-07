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

    [HideInInspector] public float handRadius = 0.8f;
    [HideInInspector] public float distanceCenterAttraction = 0.8f;
    [HideInInspector] public float attractionForce = -14f;
    [HideInInspector] public float repulsionForce = 40f;

    private void Awake()
    {
        handRadius = ParameterManager.Instance.handRadius;
        distanceCenterAttraction = ParameterManager.Instance.distanceCenterAttraction;
        attractionForce = ParameterManager.Instance.attractionForce;
        repulsionForce = ParameterManager.Instance.repulsionForce;    
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
            if (leftTool == ToolType.Attract)
                leftTool = ToolType.Repulse;
            else if (leftTool == ToolType.Repulse)
                leftTool = ToolType.Attract;
        }


        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.1f) //left
        {
            if (leftTool == ToolType.Attract)
            {
                HandPoseManager.Instance.LeftAttract(true);
                ActiveAttract(leftHand, OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
            }
            if (leftTool == ToolType.Repulse)
            {
                HandPoseManager.Instance.LeftRepulse(true);
                ActiveRepulse(leftHand, OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
            }
        }
        else
        {
            HandPoseManager.Instance.LeftAttract(false);
            HandPoseManager.Instance.LeftRepulse(false);
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.1f) //right
        {
            if (rightTool == ToolType.Attract)
            {
                HandPoseManager.Instance.RightAttract(true);
                ActiveAttract(rightHand, OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
            }
            if (rightTool == ToolType.Repulse)
            {
                HandPoseManager.Instance.RightRepulse(true);
                ActiveRepulse(rightHand, OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
            }
        }
        else
        {
            HandPoseManager.Instance.RightAttract(false);
            HandPoseManager.Instance.RightRepulse(false);
        }


        if (rightTool == ToolType.Attract)
        {
            rightHand.gameObject.GetComponentInChildren<CenterTrailParticle>().speed = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * 2000;
        }
        if (leftTool == ToolType.Attract)
        {
            leftHand.gameObject.GetComponentInChildren<CenterTrailParticle>().speed = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) * 2000;
        }

    }

    private void ActiveAttract (Transform tr, float axis)
    {
        Rigidbody[] rbs;
       
        Vector3 center = tr.position + (tr.forward * distanceCenterAttraction);
        if (GetRigidbodiesInArea(center, handRadius, out rbs))
        {
            AddExplosionForce(rbs, attractionForce * axis, handRadius, center); 
        }
    }

    private void ActiveRepulse (Transform tr, float axis)
    {

        Rigidbody[] rbs;

        Vector3 center = tr.position + (tr.forward * distanceCenterAttraction);
        if (GetRigidbodiesInArea(center, handRadius, out rbs))
        {
            AddExplosionForce(rbs, repulsionForce * axis, handRadius, center);
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
