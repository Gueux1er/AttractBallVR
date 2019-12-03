using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Stase : MonoBehaviour
{
    public bool active = true;
    public StaseRange range = StaseRange.Local;


    public SteamVR_Action_Single input;
    public SteamVR_Input_Sources inputSource;

    [Header("Local")]
    public Transform referencePoint;
    public float radiusOfEffect;

    private void Start()
    {
        input.AddOnAxisListener(ActiveStase, inputSource);
    }

    private void ActiveStase(SteamVR_Action_Single fromInput, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        switch (range)
        {
            case StaseRange.Global:
                break;

            case StaseRange.Local:
                break;
        }

    }

    public enum StaseRange
    {
        Local, Global
    }
}
