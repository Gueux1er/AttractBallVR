﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{


    [Header("Spawner")]
    public GameObject prefabToSpawn;
    public Transform prefabContainer;
    public float forceOnSpawn;
    public float delayBetweenSpawn;

    private bool spawn = false;
    private float tmpDelay = 0f;

    [Header("Pool")]
    public int poolMax = 550;
    public Transform poolContainer;

    private List<GameObject> poolList = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < prefabContainer.transform.childCount; i++)
        {
            poolList.Add(prefabContainer.transform.GetChild(i).gameObject);
        }

        int ch = prefabContainer.transform.childCount;
        for (int i = 0; i < poolMax - ch; i++)
        {
            GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, poolContainer);
            go.SetActive(false);
            poolList.Add(go);
        }
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 1 && spawn != false)
        {
            spawn = true;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0 && spawn == true)
        {
            spawn = false;
            tmpDelay = 0;
        }


        if (spawn || Input.GetKey(KeyCode.P))
        {
            tmpDelay += Time.deltaTime;
            if (tmpDelay >= delayBetweenSpawn)
            {
                tmpDelay = 0f;
                SpawnPrefab();
            }
        }   
    }

    private void SpawnPrefab()
    {
        if (poolContainer.transform.childCount <= 0)
            return;
        GameObject go = poolContainer.transform.GetChild(0).gameObject;
        go.SetActive(true);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        go.transform.SetParent(prefabContainer);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(forceOnSpawn-50,forceOnSpawn+50));

    }
}
