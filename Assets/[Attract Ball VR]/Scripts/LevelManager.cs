using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public GameObject accessor;
    public LevelStep step0;
    public LevelStep step1;
    public static LevelManager Instance;

    [Serializable]
    public struct LevelStep
    {
        [Header("Enter")]
        public UnityEvent enterEvent;

        [Header("Exit")]
        public UnityEvent exitEvent;
    }

    private LevelStep currentStep;
    private int currentStepValue;

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
        currentStep = step0;
        currentStepValue = 0;
        StartStep(currentStepValue);
    }

    void Update()
    {
        
    }

    public void StartStep(int step)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Step" + step);
        foreach(GameObject g in objects)
        {
            Animator a = g.GetComponent<Animator>();
            if (a != null) a.SetTrigger("Appear");
        }
    }

    private void EndStep(int step)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Step" + step);
        foreach (GameObject g in objects)
        {
            Animator a = g.GetComponent<Animator>();
            if (a != null) a.SetTrigger("Disappear");
        }
    }
}


