using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseManager : MonoBehaviour
{
    public static HandPoseManager Instance;
    [SerializeField] private Transform LeftAttractHandPose;
    [SerializeField] public Transform LeftRepulseHandPose;
    [SerializeField] public Transform RightAttractHandPose;
    [SerializeField] public Transform RightRepulseHandPose;

    public OvrAvatar ovrAvatar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LeftAttract(bool enable)
    {
        ovrAvatar.LeftHandCustomPose = enable ? LeftAttractHandPose : null;
    }

    public void LeftRepulse(bool enable)
    {
        ovrAvatar.LeftHandCustomPose = enable ? LeftRepulseHandPose : null;
    }

    public void RightAttract(bool enable)
    {
        ovrAvatar.RightHandCustomPose = enable ? RightAttractHandPose : null;
    }

    public void RightRepulse(bool enable)
    {
        ovrAvatar.RightHandCustomPose = enable ? RightRepulseHandPose : null;
    }

    public void ResetPose()
    {
        ovrAvatar.RightHandCustomPose = null;
        ovrAvatar.LeftHandCustomPose = null;
    }

}
