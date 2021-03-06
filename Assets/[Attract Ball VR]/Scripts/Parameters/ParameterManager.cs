﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParameterManager : MonoBehaviour
{
    public static ParameterManager Instance;

    public float handRadius = 1.5f;
    public float distanceCenterAttraction = 0.4f;
    public float attractionForce = -1000f;
    public float repulsionForce = 1500f;

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


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        

        //if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || Input.GetKeyDown(KeyCode.S)) //joystick droit
        //{            
        //    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //    Debug.Log(SceneManager.sceneCount);
        //    if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        //    {
        //        SceneManager.LoadScene(nextSceneIndex);
        //    }
        //}
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) //joystick gauche
        //{
        //    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        //    if (nextSceneIndex >= 0)
        //    {
        //        SceneManager.LoadScene(nextSceneIndex);
        //    }
        //}
    }
}
