using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Funly.SkyStudio;
using DG.Tweening;

public class SkyboxManager : MonoBehaviour
{
    public SkyProfile steppedSkyProfile;
    public SkyProfile loopedSkyProfile;

    public TimeOfDayController timeOfDayController;

    public void Start()
    {
        timeOfDayController.skyProfile = steppedSkyProfile;
        timeOfDayController.automaticTimeIncrement = false;
        timeOfDayController.skyTime = 0;
    }

    [ContextMenu("Complete step 1")]
    public void SetCompleteStepOne()
    {
        DOTween.To(() => timeOfDayController.skyTime, x => timeOfDayController.skyTime = x, 0.2f, 5f);
    }

    [ContextMenu("Complete step 2")]
    public void SetCompleteStepTwo()
    {
        DOTween.To(() => timeOfDayController.skyTime, x => timeOfDayController.skyTime = x, 0.4f, 5f);
    }

    [ContextMenu("Complete step 3")]
    public void SetCompleteStepThree()
    {
        DOTween.To(() => timeOfDayController.skyTime, x => timeOfDayController.skyTime = x, 0.6f, 5f);
    }

    [ContextMenu("Complete step 4")]
    public void SetCompleteStepFour()
    {
        DOTween.To(() => timeOfDayController.skyTime, x => timeOfDayController.skyTime = x, 0.8f, 5f);
    }

    [ContextMenu("Complete step 5")]
    public void SetCompleteStepFive()
    {
        DOTween.To(() => timeOfDayController.skyTime, x => timeOfDayController.skyTime = x, 0.99999f, 5f);
    }

    [ContextMenu("Complete step 6")]
    public void SetCompleteStepSix()
    {
        timeOfDayController.skyProfile = loopedSkyProfile;
        timeOfDayController.automaticTimeIncrement = true;
        timeOfDayController.skyTime = 0.25f;
    }
}
