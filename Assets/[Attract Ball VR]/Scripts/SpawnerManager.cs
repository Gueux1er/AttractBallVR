using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpawnerManager : MonoBehaviour
{
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean spawnButton;

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
        spawnButton.AddOnStateDownListener(Spawn, hand);
        spawnButton.AddOnStateUpListener(Unspawn, hand);

        for (int i = 0; i < prefabContainer.transform.childCount; i++)
        {
            poolList.Add(prefabContainer.transform.GetChild(i).gameObject);
        }

        int ch = prefabContainer.transform.childCount;
        for (int i = 0; i < poolMax - ch; i++)
        {
            Debug.Log(i);
            GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, poolContainer);
            go.SetActive(false);
            poolList.Add(go);
        }
    }

    void Update()
    {
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

    private void Spawn(SteamVR_Action_Boolean fromInput, SteamVR_Input_Sources fromSource)
    {
        spawn = true;
    }

    private void Unspawn(SteamVR_Action_Boolean fromInput, SteamVR_Input_Sources fromSource)
    {
        spawn = false;
        tmpDelay = 0;
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
