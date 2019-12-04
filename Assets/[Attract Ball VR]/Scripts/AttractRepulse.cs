using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractRepulse : MonoBehaviour
{
    public enum ActionType
    {
        Translation, Force
    }

    public enum ToolType
    {
        Attract, Repulse
    }

    [Header("Gameplay parameters")]
    public ActionType forceType = ActionType.Translation;
    public float attractRadius = 5.0f;
    public float attractionForce = 5.0f;
    public float attractionSpeed = 1.0f;
    public float repulsionRadius = 5.0f;
    public float repulsionForce = 5.0f;
    public float repulsionSpeed = 1.0f;

    [Header("Input parameters")]

    public Transform leftHand;
    public ToolType leftTool = ToolType.Attract;
    public Transform rightHand;
    public ToolType rightTool = ToolType.Repulse;


    private void Start()
    {

    }

    private void Update()
    {
        
        //if (OVRInput.GetDown(OVRInput.Button.One)) //A
        //{
        //    ReverseAttract();
        //    ReverseRepulse();
        //}
        //if (OVRInput.GetDown(OVRInput.Button.Three)) //X
        //{
        //    ReverseAttract();
        //    ReverseRepulse();
        //}
        

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) != 0) //left
        {
            CheckType(leftTool, leftHand, OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) != 0) //right
        {
            CheckType(rightTool, rightHand, OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
        }
    }

    private void CheckType(ToolType tool, Transform hand, float axis)
    {
        switch (tool)
        {
            case ToolType.Attract:

                break;

            case ToolType.Repulse:
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

        if (GetRigidbodiesInArea(tr.position, attractRadius, out rbs))
        {
            switch (forceType)
            {
                case ActionType.Translation:
                    AddTranslation(rbs, attractionSpeed, attractRadius, tr.position);
                    break;


                case ActionType.Force:
                    AddExplosionForce(rbs, attractionForce * axis, attractRadius, tr.position); 
                    break;
            }
        }
    }

    private void ActiveRepulse (Transform tr, float axis)
    {

        Rigidbody[] rbs;

        if (GetRigidbodiesInArea(tr.position, attractRadius, out rbs))
        {
            switch (forceType)
            {
                case ActionType.Translation:
                    AddTranslation(rbs, repulsionSpeed, repulsionRadius, tr.position);
                    break;


                case ActionType.Force:
                    AddExplosionForce(rbs, repulsionForce * axis, repulsionRadius, tr.position);
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
