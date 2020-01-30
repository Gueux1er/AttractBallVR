using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class LevelManager : MonoBehaviour
{
    public List<LevelStep> levelStepList = new List<LevelStep>();
    public static LevelManager Instance;

    [Serializable]
    public struct LevelStep
    {
        public List<Area> areas;
        [Header("Enter")]
        public UnityEvent enterEvent;

        [Header("Exit")]
        public UnityEvent exitEvent;
    }

    private LevelStep currentStep;
    private int currentStepValue;

    private bool finalActiveState = false;
    private Coroutine finalActiveStepCoroutine;

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

        currentStepValue = 0;
        currentStep = levelStepList[currentStepValue];

        StartCoroutine(StartStep(currentStepValue));
    }


    void Start()
    {
        
    }

    void Update()
    {
        int i = currentStep.areas.Count;
        int iTmp = 0;
        foreach (Area a in currentStep.areas)
        {
            if (a.activeState == Area.ActiveState.ACTIVE)
                iTmp++;
            else
            {
                finalActiveState = false;
                if (finalActiveStepCoroutine != null)
                    StopCoroutine(finalActiveStepCoroutine);
            }
        }

        if (iTmp == i && !finalActiveState || Input.GetKeyDown(KeyCode.N))
        {
            finalActiveStepCoroutine = StartCoroutine(StartFinalActiveStep(currentStepValue));            
        }
    }

    public IEnumerator StartFinalActiveStep(int step)
    {
        finalActiveState = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EndStep(currentStepValue));
    }

    public IEnumerator StartStep(int step)
    {
        yield return new WaitForSeconds(2f);

        currentStep.enterEvent.Invoke();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Step" + step);
        foreach(GameObject g in objects)
        {
            Animator a = g.GetComponent<Animator>();
            if (a != null) a.SetTrigger("Appear");
            StudioEventEmitter emitter = g.GetComponent<StudioEventEmitter>();
            if (emitter != null) emitter.Play();
        }

        Area[] areasInLevel = GameObject.FindObjectsOfType<Area>();
        foreach(Area a in currentStep.areas)
        {
            a.Appear();            
        }
    }

    private IEnumerator EndStep(int step)
    {

        currentStep.exitEvent.Invoke();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Step" + step);
        foreach (GameObject g in objects)
        {
            Animator a = g.GetComponent<Animator>();
            if (a != null) a.SetTrigger("Disappear");
        }

        Area[] areasInLevel = GameObject.FindObjectsOfType<Area>();
        foreach (Area a in currentStep.areas)
        {            
            a.Disappear();            
        }
        currentStepValue++;
        currentStep = levelStepList[currentStepValue];

        yield return new WaitForSeconds(2f);

        StartCoroutine(StartStep(currentStepValue));
    }

    public void Step0Enter()
    {
        Debug.Log("step0start");
    }


}


