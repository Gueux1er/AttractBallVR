using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public static HapticManager Instance;

    OVRHapticsClip littleBuzz;
    OVRHapticsClip bigBuzz;
    public AudioClip littleAudioFile;
    public AudioClip bigAudioFile;
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
    void Start()
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        littleBuzz = new OVRHapticsClip(littleAudioFile);
        bigBuzz = new OVRHapticsClip(bigAudioFile);
    }

    void Update()
    {

    }

    public void LittleHaptic(string hand)
    {
        if (hand == "right")
        {
            OVRHaptics.RightChannel.Preempt(littleBuzz);
        }
        else if (hand == "left")
        {
            OVRHaptics.LeftChannel.Preempt(littleBuzz);
        }
    }

    public void BigHaptic(string hand)
    {
        if (hand == "right")
        {
            OVRHaptics.RightChannel.Mix(bigBuzz);
        }
        else if (hand == "left")
        {
            OVRHaptics.LeftChannel.Mix(bigBuzz);
        }
    }

    public void SuccessHaptic(string hand)
    {
        if (hand == "right")
        {
            OVRHaptics.RightChannel.Preempt(littleBuzz);
            OVRHaptics.RightChannel.Mix(bigBuzz);
        }
        else if (hand == "left")
        {
            OVRHaptics.LeftChannel.Preempt(littleBuzz);
            OVRHaptics.LeftChannel.Mix(bigBuzz);
        }
    }
}
